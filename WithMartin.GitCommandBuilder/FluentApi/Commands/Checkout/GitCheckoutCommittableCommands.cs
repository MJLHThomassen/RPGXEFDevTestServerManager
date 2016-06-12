namespace WithMartin.GitCommandBuilder.FluentApi.Commands.Checkout
{
    public interface IGitCheckoutCommittableCommand : IGitCommand<GitCheckoutArgs>
    {
    }

    public class GitCheckoutCommitCommand : GitCheckoutCommandBase
    {
    }
}
