using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat
{
    public static class StringUtils
    {
        public static string RemoveSuffx(string text, char suffix)
        {
            return RemoveSuffx(text, suffix.ToString());
        }

        public static string RemoveSuffx(string text, string suffix)
        {
            if (text == null)
            {
                return null;
            }

            if (text.EndsWith(suffix) == true)
            {
                return text.Substring(0, text.Length - suffix.Length);
            }

            return text;
        }

        public static string AddSuffx(string text, char suffix)
        {
            return AddSuffx(text, suffix.ToString());
        }

        public static string AddSuffx(string text, string suffix)
        {
            if (text == null)
            {
                return null;
            }

            if (text.EndsWith(suffix) == false)
            {
                return text + suffix;
            }

            return text;
        }

        public static string RemovePrefix(string text, char prefix)
        {
            return RemovePrefix(text, prefix.ToString());
        }

        public static string RemovePrefix(string text, string prefix)
        {
            if (text == null)
            {
                return null;
            }

            if (text.StartsWith(prefix) == true)
            {
                return text.Substring(prefix.Length);
            }

            return text;
        }

        public static string AddPrefix(string text, char prefix)
        {
            return AddPrefix(text, prefix.ToString());
        }

        public static string AddPrefix(string text, string prefix)
        {
            if (text == null)
            {
                return null;
            }

            if (text.StartsWith(prefix) == false)
            {
                return prefix + text;
            }

            return text;
        }

    }

}
