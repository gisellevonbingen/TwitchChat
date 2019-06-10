using Giselle.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchAPIs;

namespace TwitchChat.Commands
{
    public struct EmoteIndex
    {
        public const string IndexSeparator = "-";

        public int StartIndex { get; set; }
        public int LastIndex { get; set; }

        public EmoteIndex(int startIndex, int lastIndex)
            :this()
        {
            this.StartIndex = startIndex;
            this.LastIndex = lastIndex;
        }

        public override string ToString()
        {
            return $"{this.StartIndex}{IndexSeparator}{this.LastIndex}";
        }

        public static bool TryParse(string text, out EmoteIndex value)
        {
            value = new EmoteIndex();

            if (text == null)
            {
                return false;
            }

            var splites = text.Split(IndexSeparator);

            if (splites.Length != 2)
            {
                return false;
            }

            value.StartIndex = NumberUtils.ToInt(splites[0]);
            value.LastIndex = NumberUtils.ToInt(splites[1]);

            return true;
        }

    }

}
