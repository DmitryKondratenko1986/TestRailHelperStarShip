using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace TestRailHelperStarShip.DataProviders
{
    public class JsonDataProvider : IDataProvider
    {
        private readonly string dataContent;
        private JObject JsonObject => JsonConvert.DeserializeObject<JObject>(dataContent);
        public JsonDataProvider(string dataContent)
        {
            this.dataContent = dataContent;
        }
        public T GetValue<T>(string path)
        {
            var node = GetJsonNode(path);

            return node.ToObject<T>();
        }
        public IReadOnlyDictionary<string, T> GetValueDictionary<T>(string path)
        {
            var dict = new Dictionary<string, T>();
            var node = GetJsonNode(path);
            foreach (var child in node.Children<JProperty>())
            {
                dict.Add(child.Name, GetValue<T>($".{child.Path}"));
            }

            return dict;
        }
        public IReadOnlyList<T> GetValueList<T>(string path)
        {
            var node = GetJsonNode(path);
            return node.ToObject<IReadOnlyList<T>>();
        }
        public bool IsValuePresent(string path)
        {
            return JsonObject.SelectToken(path) != null;
        }
        private JToken GetJsonNode(string jsonPath)
        {
            var node = JsonObject.SelectToken(jsonPath);
            if (node == null)
            {
                throw new ArgumentException($"There are no values found by path '{jsonPath}' in JSON content");
            }
            return node;
        }
    }
}
