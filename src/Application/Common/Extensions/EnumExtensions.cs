using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Extensions
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string position) => (T)Enum.Parse(typeof(T), position, false);
    }
}
