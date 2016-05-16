namespace WithMartin.GitCommandBuilder.FluentApi.Commands.Branch
{
    public interface IGitBranchFilterableCommand : IGitCommand<GitBranchArgs>
    {
    }

    public class GitBranchMergedCommand : GitBranchCommandBase, IGitBranchCommittableCommand
    {
    }

    public class GitBranchNoMergedCommand : GitBranchCommandBase, IGitBranchCommittableCommand
    {
    }

    public class GitBranchContainsCommand : GitBranchCommandBase, IGitBranchCommittableCommand
    {
    }
}
