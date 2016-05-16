namespace WithMartin.GitCommandBuilder
{
    public class GitCommandBuilder
    {
        public string WorkingDirectory { get; }

        public GitCommandBuilder(string workingDirectory)
        {
            WorkingDirectory = workingDirectory;
        }
    }
}
