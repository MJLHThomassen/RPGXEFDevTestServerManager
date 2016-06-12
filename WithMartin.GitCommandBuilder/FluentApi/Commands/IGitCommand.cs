namespace WithMartin.GitCommandBuilder.FluentApi.Commands
{
    /// <summary>
    /// Defines an interface for a GitCommand
    /// </summary>
    public interface IGitCommand
    {
    }

    /// <summary>
    /// Defines an interface for a GitCommand with Arguments
    /// </summary>
    public interface IGitCommand<TArgs> : IGitCommand
    {
        TOutCmd AddFlag<TOutCmd>(TArgs arg, string argValue = null) where TOutCmd : GitCommandBase<TArgs>;
    }
}
