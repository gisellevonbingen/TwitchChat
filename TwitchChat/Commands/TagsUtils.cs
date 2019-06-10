using Giselle.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchAPIs;

namespace TwitchChat.Commands
{
    public static class TagsUtils
    {
        public const int TrueValue = 1;

        public static bool ToBool(string str)
        {
            return NumberUtils.ToInt(str) == TrueValue;
        }

    }

}
