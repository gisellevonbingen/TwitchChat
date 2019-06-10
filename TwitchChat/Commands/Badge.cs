using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giselle.Commons;

namespace TwitchChat.Commands
{
    public class Badge
    {
        public const string VersionSeparator = "/";

        public string Name { get; set; }
        public string Version { get; set; }

        public static Badge Parse(string text)
        {
            if (text == null)
            {
                return null;
            }

            var splits = text.Split(VersionSeparator);

            if (splits.Length == 2)
            {
                var badge = new Badge();
                badge.Name = splits[0];
                badge.Version = splits[1];

                return badge;
            }

            return null;
        }

        public Badge()
        {
            this.Name = null;
            this.Version = null;
        }

        public override string ToString()
        {
            return $"{this.Name}{VersionSeparator}{this.Version}";
        }

    }

}
