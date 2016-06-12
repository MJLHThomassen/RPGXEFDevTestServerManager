using WithMartin.GitCommandBuilder.Attributes;

namespace WithMartin.GitCommandBuilder.FluentApi.Commands.Clean
{
    public enum GitCleanArgs
    {
        [GitArgument(Argument = "-d")]
        Dirs,

        [GitArgument(Argument = "-f")]
        Force,

        [GitArgument(Argument = "-i")]
        Interactive, // TODO: How to properly support this?

        [GitArgument(Argument = "-n")]
        DryRun,

        [GitArgument(Argument = "-q")]
        Quiet,

        [GitArgument(Argument = "-e")]
        Exclude,

        [GitArgument(Argument = "-x")]
        All,

        [GitArgument(Argument = "-X")]
        IgnoredOnly,

        [GitArgument(Argument = "--")]
        DashDash,

        [GitArgument]
        Paths
    }

    public abstract class GitCleanCommandBase : GitExecutableCommandBase<GitCleanArgs>
    {
        protected override string Command => "clean";
    }

    public interface IGitCleanableCommand : IGitCommand<GitCleanArgs>
    {     
    }

    public class GitCleanCommand : GitCleanCommandBase, IGitCleanableCommand, IGitCleanIgnorableCommand, IGitCleanDashableCommand, IGitCleanPathableCommand
    {       
    }
}
