using WithMartin.GitCommandBuilder.Attributes;
using WithMartin.GitCommandBuilder.FluentApi.Commands.Clone;

namespace WithMartin.GitCommandBuilder.FluentApi.Commands.Clean
{
    public enum GitCloneArgs
    {
        [GitArgument]
        Repository,

        [GitArgument]
        Directory
    }

    public abstract class GitCloneCommandBase : GitCommandBase<GitCloneArgs>
    {
        protected override string Command => "clone";
    }

    public interface IGitClonableCommand : IGitCommand<GitCloneArgs>
    {     
    }

    public class GitCloneCommand : GitCloneCommandBase, IGitClonableCommand, IGitCloneRepoableCommand
    {       
    }
}
