using IRCProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class CommandSerializer
    {
        public IRCPrefix Sender { get; set; }
        public string Command { get; set; }
        public int ParamsIndex { get; private set; }
        public List<string> Params { get; }

        public CommandSerializer()
        {
            this.Sender = null;
            this.Command = null;
            this.ParamsIndex = 0;
            this.Params = new List<string>();
        }

        public void FromMessage(IRCMessage message)
        {
            this.Sender = message.Prefix;
            this.Command = message.Command;
            var param = this.Params;
            param.Clear();
            param.AddRange(message.Params.Values);
            this.ParamsIndex = 0;
        }

        public void ToMessage(IRCMessage message)
        {
            message.Prefix = this.Sender;
            message.Command = this.Command;
            var param = message.Params.Values;
            param.Clear();
            param.AddRange(this.Params);
        }

        public void PutParam(string param)
        {
            this.Params.Add(param);
            this.ParamsIndex = this.Params.Count;
        }

        public void PutParams(IEnumerable<string> param)
        {
            this.Params.AddRange(param);
            this.ParamsIndex = this.Params.Count;
        }

        public string GetParam()
        {
            return this.GetParam(false);
        }

        public string GetParam(bool ignoreIndex)
        {
            var index = this.ParamsIndex;

            if (index < this.Params.Count)
            {
                var param = this.Params[index];
                this.ParamsIndex = index + 1;
                return param;
            }
            else if (ignoreIndex == true)
            {
                return null;
            }
            else
            {
                throw new IndexOutOfRangeException(nameof(this.Params));
            }

        }

        public IEnumerable<string> GetParams()
        {
            var startIndex = this.ParamsIndex;
            var endIndex = this.Params.Count;
            var count = endIndex - startIndex;
            var range = this.Params.GetRange(startIndex, count);
            this.ParamsIndex = endIndex;

            return range;
        }

    }

}
