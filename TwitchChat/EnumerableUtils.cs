using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat
{
    public static class EnumerableUtils
    {
        public static T[] ToArray<T>(this IEnumerable<T> enumerable, Func<T, bool> func)
        {
            return enumerable.Where(func).ToArray();
        }

    }

}
