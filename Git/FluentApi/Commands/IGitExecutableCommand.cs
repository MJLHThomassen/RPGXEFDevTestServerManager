namespace WithMartin.GitCommandBuilder.FluentApi.Commands
{
    /// <summary>
    /// Defines an interface for a GitCommand that is Executable
    /// </summary>
    public interface IGitExecutableCommand
    {
        GitCommandBuilder GitCommandBuilder { get; }
        string ToString();
    }
}
