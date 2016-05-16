namespace WithMartin.GitCommandBuilder.FluentApi.Commands.RevParse
{
    public interface IGitRevParseArgableCommand : IGitCommand<GitRevParseArgs>
    {      
    }

    public class GitRevParseArgsCommand : GitRevParseCommandBase, IGitExecutableCommand
    {
    }
}
