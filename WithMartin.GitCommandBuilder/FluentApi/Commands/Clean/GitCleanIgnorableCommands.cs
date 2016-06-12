namespace WithMartin.GitCommandBuilder.FluentApi.Commands.Clean
{
    public interface IGitCleanIgnorableCommand : IGitCommand<GitCleanArgs>
    {      
    }

    public class GitCleanAllCommand : GitCleanCommandBase, IGitCleanDashableCommand, IGitCleanPathableCommand
    {
    }

    public class GitCleanIgnoredOnlyCommand : GitCleanCommandBase, IGitCleanDashableCommand, IGitCleanPathableCommand
    {
    }
}
