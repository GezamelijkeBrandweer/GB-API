using System.Text.Json.Nodes;

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
}