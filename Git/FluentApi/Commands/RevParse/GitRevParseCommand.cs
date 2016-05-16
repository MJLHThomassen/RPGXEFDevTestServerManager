using WithMartin.GitCommandBuilder.Attributes;

namespace WithMartin.GitCommandBuilder.FluentApi.Commands.RevParse
{
    public enum GitRevParseArgs
    {
        [GitArgument(Argument = "--short", ArgumentValueConnector = "=")]
        Short,

        [GitArgument]
        Args
    }

    public abstract class GitRevParseCommandBase : GitCommandBase<GitRevParseArgs>
    {
        protected override string Command => "rev-parse";
    }

    public interface IGitRevParsableCommand : IGitCommand<GitRevParseArgs>
    { 
    }

    public class GitRevParseCommand : GitRevParseCommandBase, IGitRevParsableCommand, IGitRevParseArgableCommand
    {  
    }
}
