using System;

namespace Application.Common.Extensions
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string position) => (T)Enum.Parse(typeof(T), position, false);
    }
}
