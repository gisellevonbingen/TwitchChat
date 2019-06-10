using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giselle.Commons;

namespace TwitchChat.Commands
{
    public class Emote
    {
        public const string EmotesSeparator = "/";
        public const string EmoteIdSeparator = ":";
        public const string IndicesSeparator = ",";

        public static Emote Parse(string text)
        {
            if (text == null)
            {
                return null;
            }

            var splites = text.Split(EmoteIdSeparator);

            if (splites.Length != 2)
            {
                return null;
            }

            var emote = new Emote();
            emote.EmoteId = splites[0];

            var indicesToString = splites[1];
            var indicesSplits = indicesToString.Split(IndicesSeparator);

            foreach (var indexToString in indicesSplits)
            {
                if (EmoteIndex.TryParse(indexToString, out var index) == true)
                {
                    emote.Indices.Add(index);
                }

            }

            return emote;
        }

        public string EmoteId { get; set; }
        public List<EmoteIndex> Indices { get; }

        public Emote()
        {
            this.EmoteId = null;
            this.Indices = new List<EmoteIndex>();
        }

        public override string ToString()
        {
            return $"{this.EmoteId}{EmoteIdSeparator}{string.Join(IndicesSeparator, this.Indices)}";
        }

    }

}
