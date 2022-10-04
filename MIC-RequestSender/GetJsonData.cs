using System.Text.Json.Nodes;

namespace MIC_RequestSender;

public class GetJsonData
{
    public static Dictionary<string,object> ReadJsonDataFromString(JsonNode body, List<string> jsonKeys)
    {
        foreach (var key in jsonKeys)
        {
            
        }

        return new Dictionary<string, object>();
    }
}