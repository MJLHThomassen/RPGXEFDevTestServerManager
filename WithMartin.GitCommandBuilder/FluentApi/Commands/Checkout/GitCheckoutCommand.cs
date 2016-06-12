using WithMartin.GitCommandBuilder.Attributes;

namespace WithMartin.GitCommandBuilder.FluentApi.Commands.Checkout
{
    public enum GitCheckoutArgs
    {
        [GitArgument(Argument = "-q")]
        Quiet,

        [GitArgument(Argument = "-f")]
        Force,

        [GitArgument(Argument = "-m")]
        Merge,

        [GitArgument]
        Branch,

        [GitArgument(Argument = "-detach")]
        Detach,

        [GitArgument]
        Commit
    }


    public abstract class GitCheckoutCommandBase : GitExecutableCommandBase<GitCheckoutArgs>
    {
        protected override string Command => "checkout";
    }

    public interface IGitCheckoutableCommand : IGitCommand<GitCheckoutArgs>
    {
        
    }

    public class GitCheckoutCommand : GitCheckoutCommandBase, IGitCheckoutableCommand, IGitCheckoutBranchableCommand, IGitCheckoutDetachableCommand, IGitCheckoutCommittableCommand
    {
        
    }
}
