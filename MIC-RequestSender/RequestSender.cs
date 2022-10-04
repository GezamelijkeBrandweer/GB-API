using System.Text.Json.Nodes;

namespace MIC_RequestSender;

public class RequestSender
{
    public static HttpClient HttpClientBuilder(string baseUri, List<HttpHeader> headers = null)
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUri)
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
    public static async Task<JsonNode> SendRequest(HttpClient client, RequestMethod method, string optionalUri = "", List<string> optionalQueryParams = null, HttpContent optionalBody = null)
    {
        if (optionalQueryParams == null && optionalUri != "")
        {
            string.Format(optionalUri, optionalQueryParams);
        }
        using var response = method switch
        {
            RequestMethod.Get => await client.GetAsync(optionalUri),
            RequestMethod.Put => await client.PutAsync(optionalUri,optionalBody),
            RequestMethod.Post => await client.PostAsync(optionalUri,optionalBody),
            RequestMethod.Patch =>  await client.PatchAsync(optionalUri,optionalBody),
            RequestMethod.Delete => await client.DeleteAsync(optionalUri),
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
        response.EnsureSuccessStatusCode();
        var jsonNode = await response.Content.ReadAsStringAsync();
        return JsonNode.Parse(jsonNode) ?? throw new NullReferenceException("Request heeft geen body ");
    }
}