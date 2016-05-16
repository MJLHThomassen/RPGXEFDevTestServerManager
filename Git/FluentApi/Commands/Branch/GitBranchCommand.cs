using WithMartin.GitCommandBuilder.Attributes;

namespace WithMartin.GitCommandBuilder.FluentApi.Commands.Branch
{
    public enum GitBranchArgs
    {
        [GitArgument(Argument = "--color", ArgumentValueConnector = "=")]
        Color,

        [GitArgument(Argument = "--no-color")]
        NoColor,

        [GitArgument(Argument = "-r")]
        Remotes,

        [GitArgument(Argument = "-a")]
        All,

        [GitArgument(Argument = "--merged")]
        Merged,

        [GitArgument(Argument = "--no-merged")]
        NoMerged,

        [GitArgument(Argument = "--contains")]
        Contains,

        [GitArgument]
        Commit
    }

    public abstract class GitBranchCommandBase : GitExecutableCommandBase<GitBranchArgs>
    {
        protected override string Command => "branch";
    }

    public class GitBranchCommand : GitBranchCommandBase, IGitBranchColorableCommand, IGitBranchLocatableCommand, IGitBranchFilterableCommand
    {
    }
}
