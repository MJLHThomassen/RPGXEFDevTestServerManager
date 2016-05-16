namespace WithMartin.GitCommandBuilder.FluentApi.Commands
{
    internal class GitCommandArg
    {
        internal string ArgValue { get; }
        internal int ArgEnumValue { get; }

        internal GitCommandArg(int argEnumValue, string argValue)
        {
            ArgEnumValue = argEnumValue;
            ArgValue = argValue;
        }

        public override string ToString()
        {
            return ArgValue;
        }
    }
}