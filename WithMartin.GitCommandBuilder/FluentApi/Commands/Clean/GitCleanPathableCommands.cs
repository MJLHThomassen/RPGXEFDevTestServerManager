namespace WithMartin.GitCommandBuilder.FluentApi.Commands.Clean
{
    public interface IGitCleanPathableCommand : IGitCommand<GitCleanArgs>
    {        
    }

    public class GitCleanPathsCommand : GitCleanCommandBase
    {
    }
}
