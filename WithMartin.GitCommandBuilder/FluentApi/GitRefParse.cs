using System;
using WithMartin.GitCommandBuilder.FluentApi.Commands;
using WithMartin.GitCommandBuilder.FluentApi.Commands.RevParse;

namespace WithMartin.GitCommandBuilder.FluentApi
{
    public static class GitRefParse
    {
        public static GitRevParseCommand RevParse(this GitCommandBuilder gitCommandBuilder)
        {
            return new GitCommand
            {
                GitCommandBuilder = gitCommandBuilder
            }.AppendCommand(new GitRevParseCommand());
        }

        #region IGitRevParsableCommands
        /// <summary>
        /// Instead of outputting the full SHA-1 values of object names try to abbreviate them to a shorter unique name of length 7.
        /// </summary>
        /// <param name="cmd">The GitRevParseCommand to append Short to.</param>
        public static GitRevParseCommand Short(this IGitRevParsableCommand cmd)
        {
            return cmd.AddFlag<GitRevParseCommand>(GitRevParseArgs.Short);
        }

        /// <summary>
        /// Instead of outputting the full SHA-1 values of object names try to abbreviate them to a shorter unique name of length n.
        /// </summary>
        /// <param name="cmd">The GitRevParseCommand to append Short to.</param>
        /// <param name="n">Length of the SHA-1 output. The minimum length is 4.</param>
        public static GitRevParseCommand Short(this IGitRevParsableCommand cmd, int n)
        {
            return cmd.AddFlag<GitRevParseCommand>(GitRevParseArgs.Short, n.ToString());
        }
        #endregion

        #region IGitRevParseArgableCommands
        /// <summary>
        /// Flags and parameters to be parsed.
        /// </summary>
        /// <param name="cmd">The GitRevParseCommand to append Args to.</param>
        /// <param name="args">Flags and parameters to be parsed.</param>
        public static GitRevParseArgsCommand Args(this IGitRevParseArgableCommand cmd, string args)
        {
            return cmd.AddFlag<GitRevParseArgsCommand>(GitRevParseArgs.Args, args);
        }
        #endregion
    }
}
