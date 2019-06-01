using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public abstract class Tags
    {
        public Tags()
        {

        }

        public virtual void Read(TagsSerializer serializer)
        {

        }

        public virtual void Write(TagsSerializer serializer)
        {

        }

    }

}
