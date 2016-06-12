using WithMartin.GitCommandBuilder.Attributes;

namespace WithMartin.GitCommandBuilder.FluentApi.Commands.Remote
{
    public enum GitRemoteArgs
    {
        [GitArgument(Argument = "-v")]
        Verbose
    }

    public abstract class GitRemoteCommandBase : GitExecutableCommandBase<GitRemoteArgs>
    {
        protected override string Command => "remote";
    }

    public class GitRemoteCommand : GitRemoteCommandBase, IGitRemoteVerbosableCommand
    {     
    }
}
