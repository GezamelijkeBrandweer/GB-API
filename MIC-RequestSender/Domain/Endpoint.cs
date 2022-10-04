namespace MIC_RequestSender.Domain;

public class Endpoint
{
    public RequestMethod HttpMethod { get; set; }
    public string Url { get; set; }
    public StringContent RequestBody { get; set; }

    public Endpoint(RequestMethod httpMethod, string url, StringContent requestBody)
    {
        HttpMethod = httpMethod;
        Url = url;
        RequestBody = requestBody;
    }
}