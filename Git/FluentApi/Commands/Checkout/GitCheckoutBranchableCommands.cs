namespace WithMartin.GitCommandBuilder.FluentApi.Commands.Checkout
{
    public interface IGitCheckoutBranchableCommand : IGitCommand<GitCheckoutArgs>
    {
    }

    public class GitCheckoutBranchCommand : GitCheckoutCommandBase
    {
    }
}
