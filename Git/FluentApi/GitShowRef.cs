using WithMartin.GitCommandBuilder.FluentApi.Commands;
using WithMartin.GitCommandBuilder.FluentApi.Commands.ShowRef;

namespace WithMartin.GitCommandBuilder.FluentApi
{
    public static class GitShowRef
    {
        public static GitShowRefCommand ShowRef(this GitCommandBuilder gitCommandBuilder)
        {
            return new GitCommand
            {
                GitCommandBuilder = gitCommandBuilder
            }.AppendCommand(new GitShowRefCommand());
        }

        #region IGitShowRefArgableCommands
        /// <summary>
        /// Dereference tags into object IDs as well. They will be shown with "^{}" appended.
        /// </summary>
        /// <param name="cmd">The GitShowRefCommand to append Dereference to.</param>
        /// <returns></returns>
        public static GitShowRefArgsCommand Dereference(this IGitShowRefArgableCommand cmd)
        {
            return cmd.AddFlag<GitShowRefArgsCommand>(GitShowRefArgs.Dereference);
        }
        /// <summary>
        /// Only show the SHA-1 hash, not the reference name. 
        /// When combined with .Dereference() the dereferenced tag will still be shown after the SHA-1.
        /// </summary>
        /// <param name="cmd">The GitShowRefCommand to append Hash to.</param>
        /// <returns></returns>
        public static GitShowRefArgsCommand Hash(this IGitShowRefArgableCommand cmd)
        {
            return cmd.AddFlag<GitShowRefArgsCommand>(GitShowRefArgs.Hash);
        }

        /// <summary>
        /// Only show the SHA-1 hash, not the reference name. 
        /// When combined with .Dereference() the dereferenced tag will still be shown after the SHA-1.
        /// </summary>
        /// <param name="cmd">The GitShowRefCommand to append Hash to.</param>
        /// <param name="n">The length of the hash.</param>
        public static GitShowRefArgsCommand Hash(this IGitShowRefArgableCommand cmd, int n)
        {
            return cmd.AddFlag<GitShowRefArgsCommand>(GitShowRefArgs.Hash, n.ToString());
        }

        /// <summary>
        /// Abbreviate the object name.
        /// </summary>
        /// <param name="cmd">The GitShowRefCommand to append Abbrev to.</param>
        /// <returns></returns>
        public static GitShowRefArgsCommand Abbrev(this IGitShowRefArgableCommand cmd)
        {
            return cmd.AddFlag<GitShowRefArgsCommand>(GitShowRefArgs.Abbrev);
        }

        /// <summary>
        /// Abbreviate the object name. When using Hash(n), you do not have to say .Hash().Abbrev; .Hash(n) would do.
        /// </summary>
        /// <param name="cmd">The GitShowRefCommand to append Abbrev to.</param>
        /// <param name="n">The length of the abbreviated hash.</param>
        public static GitShowRefArgsCommand Abbrev(this IGitShowRefArgableCommand cmd, int n)
        {
            return cmd.AddFlag<GitShowRefArgsCommand>(GitShowRefArgs.Abbrev, n.ToString());
        }
        #endregion

        #region IGitShowRefExcludableCommands
        /// <summary>
        /// Make git show-ref act as a filter that reads refs from stdin of the form "^(?:<anything>\s)?<refname>(?:\^{})?$" 
        /// and performs the following actions on each:
        /// (1) strip "^{}" at the end of line if any;
        /// (2) ignore if pattern is provided and does not head-match refname; 
        /// (3) warn if refname is not a well-formed refname and skip; 
        /// (4) ignore if refname is a ref that exists in the local repository; 
        /// (5) otherwise output the line.
        /// </summary>
        /// <param name="cmd">The GitShowRefCommand to append ExcludeExisting to.</param>
        /// <param name="pattern">The pattern to use.</param>
        /// <returns></returns>
        public static GitShowRefExcludeExistingCommand ExcludeExisting(this IGitShowRefExcludableCommand cmd, string pattern = null)
        {
            return cmd.AddFlag<GitShowRefExcludeExistingCommand>(GitShowRefArgs.ExcludeExisting, pattern);
        }
        #endregion
    }
}
