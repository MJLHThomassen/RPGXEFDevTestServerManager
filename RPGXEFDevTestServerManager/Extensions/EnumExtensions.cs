using System;
using System.Linq;
using System.Reflection;
using RPGXEFDevTestServerManager.Attributes;

namespace RPGXEFDevTestServerManager.Extensions
{
    public static class EnumExtensions
    {
        public static string Description<T>(this T status)
        {
            return typeof(T).GetMember(Enum.GetName(typeof(T), status)).First().GetCustomAttribute<EnumValueDescriptionAttribute>().Description;
        }
    }
}