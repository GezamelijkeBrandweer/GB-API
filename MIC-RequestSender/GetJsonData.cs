using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace MIC_RequestSender;

public class GetJsonData
{
    
    public static Dictionary<string,object> ReadJsonDataFromString(JsonNode body, List<string> jsonKeys)
    {
        var dictionary = new Dictionary<string, object>();
        foreach (var key in jsonKeys)
        {
            var path = key.Split("/");
            dictionary.Add(path[^1], path.Aggregate(body, (current, p) => current[p]));
        }
        return dictionary;
    }

    private static JToken GetJsonValueFromNode(JToken body, IReadOnlyList<string> jsonKeyPath)
    {
        if (jsonKeyPath.Count == 0)
        {
            return body;
        }
        var filteredBody = body[jsonKeyPath[0]];
        if (filteredBody == null) throw new Exception("Given keyPath does not exist");
        
        return GetJsonValueFromNode(filteredBody, jsonKeyPath.Skip(1).ToArray());
    }

    public static List<Dictionary<string, JToken>> ReadJsonArrayDataFromString(JsonNode body, List<string> jsonKeys, string listInProperty = "")
    {
        List<Dictionary<string, JToken>> jsonDictionaryList = new();
        JToken jsonBody = JToken.Parse(body.ToJsonString());
        if (listInProperty != "")
        {
            jsonBody = GetJsonValueFromNode(jsonBody, new[] { listInProperty });
        }
        
        var jArray = (JArray)jsonBody;
        foreach (var jObject in jArray)
        {
            Dictionary<string, JToken> jsonDictionary = new();
            foreach (var key in jsonKeys)
            {
                var path = key.Split("/");
                jsonDictionary.Add(path[^1], GetJsonValueFromNode(jObject, path));
            }
            jsonDictionaryList.Add(jsonDictionary);
        }

        return jsonDictionaryList;
    }
}