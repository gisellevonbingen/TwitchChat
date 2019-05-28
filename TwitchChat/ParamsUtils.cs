using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat
{
    public static class ParamsUtils
    {
        public static string RemovePrefix(string text, string prefix)
        {
            if (text.StartsWith(prefix) == true)
            {
                return text.Substring(prefix.Length);
            }

            return text;
        }

        public static string AddPrefix(string text, string prefix)
        {
            if (text.StartsWith(prefix) == false)
            {
                return prefix + text;
            }

            return text;
        }

    }

}
