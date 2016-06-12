namespace WithMartin.GitCommandBuilder.FluentApi.Commands.Branch
{
    public interface IGitBranchColorableCommand : IGitCommand<GitBranchArgs>
    {
    }

    public class GitBranchColorCommand : GitBranchCommandBase, IGitBranchLocatableCommand, IGitBranchFilterableCommand
    {
    }

    public class GitBranchNoColorCommand : GitBranchCommandBase, IGitBranchLocatableCommand, IGitBranchFilterableCommand
    {
    }
}
