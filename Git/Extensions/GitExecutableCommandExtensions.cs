using System.Linq;
using WithMartin.GitCommandBuilder.FluentApi.Commands;

namespace WithMartin.GitCommandBuilder.Extensions
{
    public static class GitExecutableCommandExtensions
    {
        /// <summary>
        /// Executes the given Git command on the command line.
        /// </summary>
        /// <param name="cmd">The command</param>
        /// <param name="cleanup">Deos some cleanup on the output, removing leading and trailing whitespace per line and the command's trailing newline. Default: true</param>
        /// <returns>The output of the command</returns>
        public static string Execute(this IGitExecutableCommand cmd, bool cleanup = true)
        {
            var output = cmd.ToString().Run(cmd.GitCommandBuilder.WorkingDirectory);

            if (!cleanup || string.IsNullOrEmpty(output))
                return output;

            // Remove leading and trailing whitespaces per line
            var cleanedOutput = output.Split('\n').Aggregate("", (current, line) => current + line.Trim() + '\n');

            // Remove command trailing whitespace and newlines
            while (cleanedOutput.EndsWith(" ") || cleanedOutput.EndsWith("\n"))
            {
                cleanedOutput = output.Remove(output.Length - 1);
            }

            return cleanedOutput;
        }
    }
}
