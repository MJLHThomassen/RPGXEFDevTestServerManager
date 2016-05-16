using WithMartin.GitCommandBuilder.Attributes;

namespace WithMartin.GitCommandBuilder.FluentApi.Commands.ShowRef
{
    public enum GitShowRefArgs
    {
        [GitArgument(Argument = "-q")]
        Quiet,

        [GitArgument(Argument = "--verify")]
        Verify,

        [GitArgument(Argument = "--head")]
        Head,

        [GitArgument(Argument = "-d")]
        Dereference,

        [GitArgument(Argument = "--hash", ArgumentValueConnector = "=")]
        Hash,

        [GitArgument(Argument = "--abbrev", ArgumentValueConnector = "=")]
        Abbrev,

        [GitArgument(Argument = "--tags")]
        Tags,

        [GitArgument(Argument = "--heads")]
        Heads,

        [GitArgument(Argument = "--")]
        DashDash,

        [GitArgument]
        Pattern,

        [GitArgument(Argument = "--exclude-existing", ArgumentValueConnector = "=")]
        ExcludeExisting
    }

    public abstract class GitShowRefCommandBase : GitCommandBase<GitShowRefArgs>
    {
        protected override string Command => "show-ref";
    }

    public class GitShowRefCommand : GitShowRefCommandBase, IGitShowRefArgableCommand, IGitShowRefExcludableCommand
    {       
    }
}
