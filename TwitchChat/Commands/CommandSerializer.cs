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
        public Dictionary<string, string> Tags { get; set; }
        public IRCPrefix Sender { get; set; }
        public string Command { get; set; }
        public int ParamsIndex { get; private set; }
        public List<string> Params { get; }

        public CommandSerializer()
        {
            this.Tags = null;
            this.Sender = null;
            this.Command = null;
            this.ParamsIndex = 0;
            this.Params = new List<string>();
        }

        public T ReadTags<T>(Func<T> constructor) where T : Tags
        {
            var tags = this.Tags;

            if (tags != null)
            {
                var tagSerializer = new TagsSerializer(tags);

                var tagsObject = constructor();
                tagsObject.Read(tagSerializer);

                return tagsObject;
            }

            return null;
        }

        public void WriteTags<T>(T tagsObject) where T : Tags
        {
            if (tagsObject != null)
            {
                if (this.Tags == null)
                {
                    this.Tags = new Dictionary<string, string>();
                }

                var tagSerializer = new TagsSerializer(this.Tags);
                tagsObject.Write(tagSerializer);
            }

        }

        public void FromMessage(IRCMessage message)
        {
            this.Tags = this.GetTags(message);
            this.Sender = message.Prefix;
            this.Command = message.Command;
            var param = this.Params;
            param.Clear();
            param.AddRange(message.Params.Values);
            this.ParamsIndex = 0;
        }

        private Dictionary<string, string> GetTags(IRCMessage message)
        {
            var mTags = message.Tags;

            if (mTags != null)
            {
                var tags = new Dictionary<string, string>();

                foreach (var pair in mTags.Values)
                {
                    tags[pair.Key.Name] = pair.Value;
                }

                return tags;
            }

            return null;
        }

        public void ToMessage(IRCMessage message)
        {
            message.Tags = this.SetTags(this.Tags);
            message.Prefix = this.Sender;
            message.Command = this.Command;
            var param = message.Params.Values;
            param.Clear();
            param.AddRange(this.Params);
        }

        private IRCTags SetTags(Dictionary<string, string> tags)
        {
            if (tags == null || tags.Count == 0)
            {
                return null;
            }

            var mTags = new IRCTags();

            foreach (var pair in tags)
            {
                var ircKey = new IRCTagKey();
                ircKey.Name = pair.Key;
                mTags.Values[ircKey] = pair.Value;
            }

            return mTags;

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
