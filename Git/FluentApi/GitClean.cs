using WithMartin.GitCommandBuilder.FluentApi.Commands;
using WithMartin.GitCommandBuilder.FluentApi.Commands.Clean;

namespace WithMartin.GitCommandBuilder.FluentApi
{
    public static class GitClean
    {
        public static GitCleanCommand Clean(this GitCommandBuilder gitCommandBuilder)
        {
            return new GitCommand
            {
                GitCommandBuilder = gitCommandBuilder
            }.AppendCommand(new GitCleanCommand());
        }

        #region IGitCleanableCommands
        public static GitCleanCommand Dirs(this IGitCleanableCommand cmd)
        {
            return cmd.AddFlag<GitCleanCommand>(GitCleanArgs.Dirs);
        }

        public static GitCleanCommand Force(this IGitCleanableCommand cmd)
        {
            return cmd.AddFlag<GitCleanCommand>(GitCleanArgs.Force);
        }

        public static GitCleanCommand Interactive(this IGitCleanableCommand cmd)
        {
            return cmd.AddFlag<GitCleanCommand>(GitCleanArgs.Interactive);
        }

        public static GitCleanCommand DryRun(this IGitCleanableCommand cmd)
        {
            return cmd.AddFlag<GitCleanCommand>(GitCleanArgs.DryRun);
        }

        public static GitCleanCommand Quiet(this IGitCleanableCommand cmd)
        {
            return cmd.AddFlag<GitCleanCommand>(GitCleanArgs.Quiet);
        }

        public static GitCleanCommand Exclude(this IGitCleanableCommand cmd, string pattern)
        {
            return cmd.AddFlag<GitCleanCommand>(GitCleanArgs.Exclude, pattern);
        }
        #endregion

        #region IGitCleanIgnorableCommands
        public static GitCleanAllCommand All(this IGitCleanIgnorableCommand cmd)
        {
            return cmd.AddFlag<GitCleanAllCommand>(GitCleanArgs.All);
        }

        public static GitCleanIgnoredOnlyCommand IgnoredOnly(this IGitCleanIgnorableCommand cmd)
        {
            return cmd.AddFlag<GitCleanIgnoredOnlyCommand>(GitCleanArgs.IgnoredOnly);
        }
        #endregion

        #region IGitCleanDashableCommands
        public static GitCleanDashDashCommand DashDash(this IGitCleanDashableCommand cmd)
        {
            return cmd.AddFlag<GitCleanDashDashCommand>(GitCleanArgs.DashDash);
        }
        #endregion

        #region IGitCleanPathableCommands
        public static GitCleanPathsCommand Path(this IGitCleanPathableCommand cmd, string paths)
        {
            return cmd.AddFlag<GitCleanPathsCommand>(GitCleanArgs.Paths, paths);
        }
        #endregion
    }
}
