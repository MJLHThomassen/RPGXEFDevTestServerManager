using System;

namespace WithMartin.GitCommandBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class GitArgumentAttribute : Attribute
    {
        /// <summary>
        /// The argument/flag as a string.
        /// </summary>
        public string Argument { get; set; }

        /// <summary>
        /// The string to put between the argument/flag and the argument/flag value if there is a value present.
        /// If ArgumentString() is called with a value but without this being set, it defaults to a space: " ".
        /// </summary>
        public string ArgumentValueConnector { get; set; }

        /// <summary>
        /// If set to true, allows multiple instances of this argument.
        /// Can be used to specify -f twice in git clean for example.
        /// </summary>
        public bool AllowMultiple { get; set; }

        public GitArgumentAttribute()
        {
            Argument = null;
            ArgumentValueConnector = null;
        }

        public string ArgumentString(string argumentValue = null)
        {
            if(argumentValue == null)
                return Argument;

            if (Argument == null)
                return argumentValue;

            if (ArgumentValueConnector == null)
                return Argument + " " + argumentValue;

            return Argument + ArgumentValueConnector + argumentValue;
        }
    }
}
