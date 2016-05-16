namespace WithMartin.GitCommandBuilder.FluentApi.Commands.Branch
{
    public interface IGitBranchLocatableCommand : IGitCommand<GitBranchArgs>
    {
    }

    public class GitBranchRemotesCommand : GitBranchCommandBase, IGitBranchFilterableCommand
    {
    }

    public class GitBranchAllCommand : GitBranchCommandBase, IGitBranchFilterableCommand
    {
    }
}
