namespace WithMartin.GitCommandBuilder.FluentApi.Commands.ShowRef
{
    public interface IGitShowRefArgableCommand : IGitCommand<GitShowRefArgs>
    {
    }

    public class GitShowRefArgsCommand : GitShowRefCommandBase, IGitShowRefArgableCommand, IGitExecutableCommand
    {
    }
}
