using WithMartin.GitCommandBuilder.FluentApi.Commands;
using WithMartin.GitCommandBuilder.FluentApi.Commands.Checkout;
using WithMartin.GitCommandBuilder.FluentApi.Commands.Clean;
using WithMartin.GitCommandBuilder.FluentApi.Commands.Clone;

namespace WithMartin.GitCommandBuilder.FluentApi
{
    public static class GitClone
    {
        public static GitCloneCommand Clone(this GitCommandBuilder gitCommandBuilder)
        {
            return new GitCommand
            {
                GitCommandBuilder = gitCommandBuilder
            }.AppendCommand(new GitCloneCommand());
        }

        #region IGitCloneRepoableCommand

        public static GitCloneRepositoryCommand Repository(this IGitCloneRepoableCommand cmd, string repository)
        {
            return cmd.AddFlag<GitCloneRepositoryCommand>(GitCloneArgs.Repository, repository);
        }

        #endregion

        #region IGitCloneDirableCommand

        public static GitCloneDirectoryCommand Directory(this IGitCloneDirableCommand cmd, string directory)
        {
            return cmd.AddFlag<GitCloneDirectoryCommand>(GitCloneArgs.Directory, directory);
        }

        #endregion
    }
}
