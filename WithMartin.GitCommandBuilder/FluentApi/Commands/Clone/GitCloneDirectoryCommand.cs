using WithMartin.GitCommandBuilder.FluentApi.Commands.Clean;

namespace WithMartin.GitCommandBuilder.FluentApi.Commands.Clone
{
    public interface IGitCloneDirableCommand : IGitCommand<GitCloneArgs>
    {      
    }

    public class GitCloneDirectoryCommand : GitCloneCommandBase, IGitExecutableCommand
    {
    }
}
