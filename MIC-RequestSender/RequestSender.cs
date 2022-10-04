using System.Text.Json.Nodes;

namespace MIC_RequestSender;

public class RequestSender
{
    public static HttpClient HttpClientBuilder(string uri, List<HttpHeader> headers = null)
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(uri)
        };
        if (headers == null)
        {
            return httpClient;
        }
        
        foreach (var httpHeader in headers)
        {
            httpClient.DefaultRequestHeaders.Add(httpHeader.Name, httpHeader.Value);
        }
        return httpClient;
    }
    public static async Task<JsonNode> SendRequest(HttpClient client, RequestMethod method, HttpContent optionalBody = null)
    {
        using var response = method switch
        {
            RequestMethod.Get => await client.GetAsync(""),
            RequestMethod.Put => await client.PutAsync("",optionalBody),
            RequestMethod.Post => await client.PostAsync("",optionalBody),
            RequestMethod.Patch =>  await client.PatchAsync("",optionalBody),
            RequestMethod.Delete => await client.DeleteAsync(""),
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
        response.EnsureSuccessStatusCode();
        var jsonNode = await response.Content.ReadAsStringAsync();
        return JsonNode.Parse(jsonNode) ?? throw new NullReferenceException("Niks terug gekregen");
    }
}