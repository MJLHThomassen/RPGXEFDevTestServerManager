namespace WithMartin.GitCommandBuilder.FluentApi.Commands
{
    /// <summary>
    /// Defines a base class for executable commands
    /// </summary>
    /// <typeparam name="TArgs">The type of the arguments enum for this command.</typeparam>
    public abstract class GitExecutableCommandBase<TArgs> : GitCommandBase<TArgs>, IGitExecutableCommand
    {
    }
}
