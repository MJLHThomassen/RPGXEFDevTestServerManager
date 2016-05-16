namespace WithMartin.GitCommandBuilder.FluentApi.Commands.Checkout
{
    public interface IGitCheckoutDetachableCommand : IGitCommand<GitCheckoutArgs>
    {
    }

    public class GitCheckoutDetachCommand : GitCheckoutCommandBase, IGitCheckoutBranchableCommand, IGitCheckoutCommittableCommand
    {
    }
}
