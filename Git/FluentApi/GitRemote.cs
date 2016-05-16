using WithMartin.GitCommandBuilder.FluentApi.Commands;
using WithMartin.GitCommandBuilder.FluentApi.Commands.Remote;
using WithMartin.GitCommandBuilder.FluentApi.Commands.Remote.SubCommands;

namespace WithMartin.GitCommandBuilder.FluentApi
{
    public static class GitRemote
    {
        public static GitRemoteCommand Remote(this GitCommandBuilder gitCommandBuilder)
        {
            return new GitCommand
            {
                GitCommandBuilder = gitCommandBuilder
            }.AppendCommand(new GitRemoteCommand());
        }

        #region IGitRemotableCommands
        public static GitRemoteVerboseCommand Verbose(this IGitRemoteVerbosableCommand cmd)
        {
            return cmd.AddFlag<GitRemoteVerboseCommand>(GitRemoteArgs.Verbose);
        }
        #endregion

        #region GitRemoteCommands
        public static GitRemoteUpdateCommand Update(this GitRemoteCommand cmd)
        {
            return cmd.AppendCommand(new GitRemoteUpdateCommand());
        }

        public static GitRemoteUpdateCommand Update(this GitRemoteVerboseCommand cmd)
        {
            return cmd.AppendCommand(new GitRemoteUpdateCommand());
        }

        #endregion

        #region IGitRemoteUpdatableCommands
        public static GitRemoteUpdateCommand Prune(this IGitRemoteUpdatableCommand cmd)
        {
            return cmd.AddFlag<GitRemoteUpdateCommand>(GitRemoteUpdateArgs.Prune);
        }

        public static GitRemoteUpdateCommand Remote(this IGitRemoteUpdatableCommand cmd, string groupOrRemote)
        {
            return cmd.AddFlag<GitRemoteUpdateCommand>(GitRemoteUpdateArgs.GroupOrRemote, groupOrRemote);
        }
        #endregion
    }
}
