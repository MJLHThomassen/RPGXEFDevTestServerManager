using WithMartin.GitCommandBuilder.FluentApi.Commands;
using WithMartin.GitCommandBuilder.FluentApi.Commands.Checkout;

namespace WithMartin.GitCommandBuilder.FluentApi
{
    public static class GitCheckout
    {
        public static GitCheckoutCommand Checkout(this GitCommandBuilder gitCommandBuilder)
        {
            return new GitCommand
            {
                GitCommandBuilder = gitCommandBuilder
            }.AppendCommand(new GitCheckoutCommand());
        }

        #region IGitCheckoutableCommands
        public static GitCheckoutCommand Quiet(this IGitCheckoutableCommand cmd)
        {
            return cmd.AddFlag<GitCheckoutCommand>(GitCheckoutArgs.Quiet);
        }

        public static GitCheckoutCommand Force(this IGitCheckoutableCommand cmd)
        {
            return cmd.AddFlag<GitCheckoutCommand>(GitCheckoutArgs.Force);
        }

        public static GitCheckoutCommand Merge(this IGitCheckoutableCommand cmd)
        {
            return cmd.AddFlag<GitCheckoutCommand>(GitCheckoutArgs.Merge);
        }
        #endregion

        #region IGitCheckoutBranchableCommands
        public static GitCheckoutBranchCommand Branch(this IGitCheckoutBranchableCommand cmd, string branch)
        {
            return cmd.AddFlag<GitCheckoutBranchCommand>(GitCheckoutArgs.Branch, branch);
        }
        #endregion

        #region IGitCheckoutDetachableCommands
        public static GitCheckoutDetachCommand Detach(this IGitCheckoutDetachableCommand cmd)
        {
            return cmd.AddFlag<GitCheckoutDetachCommand>(GitCheckoutArgs.Detach);
        }
        #endregion

        #region IGitCheckoutCommittableCommands
        public static GitCheckoutCommitCommand Commit(this IGitCheckoutCommittableCommand cmd, string commit)
        {
            return cmd.AddFlag<GitCheckoutCommitCommand>(GitCheckoutArgs.Branch, commit);
        }
        #endregion
    }
}
