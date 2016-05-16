using System;

namespace RPGXEFDevTestServerManager.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumValueDescriptionAttribute : Attribute
    {
        public string Description { get; }

        public EnumValueDescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}