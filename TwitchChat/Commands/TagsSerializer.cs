using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class TagsSerializer
    {
        public Dictionary<string, List<string>> Raw { get; }

        public TagsSerializer(Dictionary<string, List<string>> tags)
        {
            this.Raw = tags;
        }

        public void PutSingle(string key, string value)
        {
            this.PutList(key, new List<string>() { value });
        }

        public void PutList(string key, List<string> values)
        {
            this.Raw[key] = values;
        }

        public string GetSingle(string key)
        {
            return this.GetList(key)?.FirstOrDefault();
        }

        public List<string> GetList(string key)
        {
            return this.Raw.TryGetValue(key, out var list) ? list : null;
        }

    }

}
