using WithMartin.GitCommandBuilder.FluentApi.Commands;
using WithMartin.GitCommandBuilder.FluentApi.Commands.Branch;

namespace WithMartin.GitCommandBuilder.FluentApi
{
    public static class GitBranch
    {
        public static GitBranchCommand Branch(this GitCommandBuilder gitCommandBuilder)
        {
            return new GitCommand
            {
                GitCommandBuilder = gitCommandBuilder
            }.AppendCommand(new GitBranchCommand());
        }

        #region IGitBranchColorableCommands
        public static GitBranchColorCommand Color(this IGitBranchColorableCommand cmd, string when = null)
        {
            return cmd.AddFlag<GitBranchColorCommand>(GitBranchArgs.Color, when);
        }

        public static GitBranchNoColorCommand NoColor(this IGitBranchColorableCommand cmd, int n)
        {
            return cmd.AddFlag<GitBranchNoColorCommand>(GitBranchArgs.NoColor);
        }
        #endregion

        #region IGitBranchLocatableCommands
        public static GitBranchRemotesCommand Remotes(this IGitBranchLocatableCommand cmd)
        {
            return cmd.AddFlag<GitBranchRemotesCommand>(GitBranchArgs.Remotes);
        }

        public static GitBranchRemotesCommand All(this IGitBranchLocatableCommand cmd)
        {
            return cmd.AddFlag<GitBranchRemotesCommand>(GitBranchArgs.All);
        }
        #endregion

        #region IGitBranchFilterableCommands
        public static GitBranchMergedCommand Merged(this IGitBranchFilterableCommand cmd)
        {
            return cmd.AddFlag<GitBranchMergedCommand>(GitBranchArgs.Merged);
        }

        public static GitBranchNoMergedCommand NoMerged(this IGitBranchFilterableCommand cmd)
        {
            return cmd.AddFlag<GitBranchNoMergedCommand>(GitBranchArgs.NoMerged);
        }

        public static GitBranchContainsCommand Contains(this IGitBranchFilterableCommand cmd)
        {
            return cmd.AddFlag<GitBranchContainsCommand>(GitBranchArgs.Contains);
        }
        #endregion

        #region IGitBranchCommittableCommands
        public static GitBranchCommitCommand Commit(this IGitBranchCommittableCommand cmd, string commit)
        {
            return cmd.AddFlag<GitBranchCommitCommand>(GitBranchArgs.Commit, commit);
        }
        #endregion
    }
}
