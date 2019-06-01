using System;
using System.Collections.Generic;
using System.Linq;

namespace TwitchChat.Commands
{
    public class TagsSerializer
    {
        public const string TagValuesSeparator = ",";

        public Dictionary<string, string> Raw { get; }

        public TagsSerializer(Dictionary<string, string> tags)
        {
            this.Raw = tags;
        }

        public void PutSingle(string key, string value)
        {
            this.PutList(key, new List<string>() { value });
        }

        public void PutList(string key, List<string> values)
        {
            this.PutList(key, values, TagValuesSeparator);
        }

        public void PutList(string key, List<string> values, string separator)
        {
            this.Raw[key] = string.Join(separator, values);
        }

        public string GetSingle(string key)
        {
            return this.GetList(key)?.FirstOrDefault();
        }

        public List<string> GetList(string key)
        {
            return this.GetList(key, TagValuesSeparator);
        }

        public List<string> GetList(string key, string separator)
        {
            var values = new List<string>();

            if (this.Raw.TryGetValue(key, out var value) == true)
            {
                var splits = value.Split(separator);
                values.AddRange(splits);
            }

            return values;
        }

    }

}
