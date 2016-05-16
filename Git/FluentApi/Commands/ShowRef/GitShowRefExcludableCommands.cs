namespace WithMartin.GitCommandBuilder.FluentApi.Commands.ShowRef
{
    public interface IGitShowRefExcludableCommand : IGitCommand<GitShowRefArgs>
    {
    }

    public class GitShowRefExcludeExistingCommand : GitShowRefCommandBase, IGitExecutableCommand
    {
    }
}
