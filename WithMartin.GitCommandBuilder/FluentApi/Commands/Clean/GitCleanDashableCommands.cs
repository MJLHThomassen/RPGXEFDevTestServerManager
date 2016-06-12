namespace WithMartin.GitCommandBuilder.FluentApi.Commands.Clean
{
    public interface IGitCleanDashableCommand : IGitCommand<GitCleanArgs>
    {        
    }

    public class GitCleanDashDashCommand : GitCleanCommandBase, IGitCleanPathableCommand
    {
    }
}
