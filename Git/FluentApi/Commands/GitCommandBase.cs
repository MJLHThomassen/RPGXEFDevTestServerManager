using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WithMartin.GitCommandBuilder.Attributes;

namespace WithMartin.GitCommandBuilder.FluentApi.Commands
{
    public abstract class GitCommandBase : IGitCommand
    {
        protected abstract string Command { get; }

        public GitCommandBuilder GitCommandBuilder { get; internal set; }
        protected GitCommandBase PrependedCommand;

        internal TCmd AppendCommand<TCmd>(TCmd cmd) where TCmd : GitCommandBase, new()
        {
            var newCommand = new TCmd
            {
                PrependedCommand = this,
                GitCommandBuilder = GitCommandBuilder
            };

            return newCommand;
        }

        public override string ToString()
        {
            if (PrependedCommand == null)
            {
                return Command;
            }

            var previousCommandsString = PrependedCommand.ToString();

            return previousCommandsString + " " + Command;
        }
    }

    public abstract class GitCommandBase<TArgs> : GitCommandBase, IGitCommand<TArgs>
    {
        private List<GitCommandArg> _args;

        protected GitCommandBase()
        {
            _args = new List<GitCommandArg>();
        }

        private TOutCmd Cast<TOutCmd>(GitCommandBase<TArgs> from) where TOutCmd : GitCommandBase<TArgs>
        {
            var castedCommand = Activator.CreateInstance<TOutCmd>();

            castedCommand.PrependedCommand = from.PrependedCommand;
            castedCommand.GitCommandBuilder = from.GitCommandBuilder;

            castedCommand._args = _args;

            return castedCommand;
        }

        /// <summary>
        /// Adds a flag to the command, potentially transforming it into another (more restrictive) command.
        /// </summary>
        /// <typeparam name="TOutCmd">The command to transform into after adding the argument.</typeparam>
        /// <param name="arg">The argurment to add.</param>
        /// <param name="argValue">The value of the argument to add. Default: null</param>
        /// <returns>The (transformed) command with the argument added.</returns>
        TOutCmd IGitCommand<TArgs>.AddFlag<TOutCmd>(TArgs arg, string argValue)
        {
            var argType = arg.GetType();
            var argEnumName = Enum.GetName(argType, arg);
            var argEnumValue = (int)Enum.Parse(typeof(TArgs), argEnumName);

            var attribute =
                (GitArgumentAttribute)
                    argType.GetMember(argEnumName).Single().GetCustomAttribute(typeof(GitArgumentAttribute));

            if (!attribute.AllowMultiple && _args.Any(flag => flag.ArgEnumValue == argEnumValue))
            {
                throw new InvalidOperationException($"The {argEnumName} argument has already been specified. It can not be specified multiple times.");
            }

            _args.Add(new GitCommandArg(argEnumValue, attribute.ArgumentString(argValue)));

            return Cast<TOutCmd>(this);
        }

        public override string ToString()
        {
            var commandString = base.ToString();

            if(_args.Any())
            {
                commandString += " " + _args.Select(flag => flag.ToString()).Aggregate((a, b) => a + " " + b);
            }

            return commandString;
        }
    }
}
