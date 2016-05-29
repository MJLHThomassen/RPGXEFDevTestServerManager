using System.Collections.Generic;
using System.Linq;
using WithMartin.GitCommandBuilder;
using WithMartin.GitCommandBuilder.Extensions;
using WithMartin.GitCommandBuilder.FluentApi;

namespace RPGXEFDevTestServerManager.ExternalHelpers
{
    public class GitHelper
    {
        private readonly GitCommandBuilder _git;

        public GitHelper(GitCommandBuilder git)
        {
            _git = git;
        }

        public void Clone(string repo, string dir)
        {
            _git.Clone().Repository(repo).Directory(dir).Execute();
        }

        public KeyValuePair<string, string> GetCurrentBranch()
        {
            var currentBranchHash = _git.RevParse().Short().Args("HEAD").Execute();

            var currentBranchNames = _git.Branch().Remotes().Contains().Commit(currentBranchHash).Execute().Split('\n');

            // Return the last matched brnach
            var currentBranchName = currentBranchNames.Last().Replace(" ", "");

            return new KeyValuePair<string, string>(currentBranchHash, currentBranchName);
        } 

        /// <summary>
        /// Returns all remote branches only.
        /// </summary>
        /// <returns>
        /// A dictory with branches where the key is the abbreviated commit hash 
        /// of that branch's current commit and the value is the branch name (without regs/remotes/origin/ prepended).
        /// </returns>
        public IEnumerable<KeyValuePair<string, string>> GetRemoteBranches()
        {
            // Update remotes and prune removed refs
            _git.Remote().Update().Prune().Execute();

            // Show all refes
            var refs = _git.ShowRef().Abbrev().Execute();

            var branches = refs
                .Split('\n')                                        // GitCommandBuilder outputs each ref on a newline
                .Where(r => r.Contains("refs/remotes/origin/"))     // Grab remote origins only
                .Where(r => !r.EndsWith("HEAD"))                    // Ignore the HEAD entry
                .Select(r =>                                        // Create a key-value pair of the hash and branch name
                {
                    r = r.Replace("refs/remotes/origin/", "");      // Remove refs/remotes/origin/ prefix, this is implied
                    var separatorLocation = r.IndexOf(' ');
                    return new KeyValuePair<string, string>(
                        r.Substring(0, separatorLocation),          // The hash
                        r.Substring(separatorLocation + 1));        // The branch name
                });

            return branches;
        }

        /// <summary>
        /// Checks out the specified commit or branch and cleans the repository.
        /// </summary>
        /// <param name="hashOrBranch">The hash or branch to check out.</param>
        public void SwitchToClean(string hashOrBranch)
        {
            // Checkout the commit
            _git.Checkout().Force().Commit(hashOrBranch).Execute();

            // Clean the repo
            _git.Clean().Force().Dirs().All().Execute();
        }
    }
}
