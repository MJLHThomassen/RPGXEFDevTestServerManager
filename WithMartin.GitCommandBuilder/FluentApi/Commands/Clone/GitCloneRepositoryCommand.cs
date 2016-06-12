using WithMartin.GitCommandBuilder.FluentApi.Commands.Clean;

namespace WithMartin.GitCommandBuilder.FluentApi.Commands.Clone
{
    public interface IGitCloneRepoableCommand : IGitCommand<GitCloneArgs>
    {      
    }

    public class GitCloneRepositoryCommand : GitCloneCommandBase, IGitExecutableCommand, IGitCloneDirableCommand
    {
    }
}
