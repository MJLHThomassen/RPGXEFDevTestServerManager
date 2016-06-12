using WithMartin.GitCommandBuilder.Attributes;

namespace WithMartin.GitCommandBuilder.FluentApi.Commands.Remote.SubCommands
{
    public enum GitRemoteUpdateArgs
    {
        [GitArgument(Argument = "-p")]
        Prune,

        [GitArgument]
        GroupOrRemote
    }

    public abstract class GitRemoteUpdateCommandBase : GitExecutableCommandBase<GitRemoteUpdateArgs>
    {
        protected override string Command => "update";
    }

    public interface IGitRemoteUpdatableCommand : IGitCommand<GitRemoteUpdateArgs>
    {
    }

    public class GitRemoteUpdateCommand : GitRemoteUpdateCommandBase, IGitRemoteUpdatableCommand
    {
    }
}
