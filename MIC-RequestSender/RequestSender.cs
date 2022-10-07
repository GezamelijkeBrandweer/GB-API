using System.Text;
using System.Text.Json.Nodes;
using MIC_RequestSender.Domain;

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
    public static async Task<JsonNode> SendRequest(HttpClient client, RequestMethod method, string optionalUri = "", Dictionary<string, string>? optionalQueryParams = null, HttpContent? optionalBody = null)
    {
        if (optionalQueryParams != null && optionalUri != "")
        {
            optionalUri = FormatQueryParams(optionalQueryParams, optionalUri);
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

    private static string FormatQueryParams(Dictionary<string, string> queryParams, string uri)
    {
        var stringBuilder = new StringBuilder(uri);
        foreach (var queryParam in queryParams)
        {
            stringBuilder.Append($"{queryParam.Key}={queryParam.Value}&");
        }
        // remove last &
        stringBuilder.Remove(-1, 1);
        return stringBuilder.ToString();
    }
}