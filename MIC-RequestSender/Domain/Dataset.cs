namespace MIC_RequestSender.Domain;

public class Dataset
{
    public string Name { get; set; }
    public bool KeyInHeader { get; set; }
    public string BaseUrl { get; set; }
    public List<Endpoint> Endpoints { get; } = new();

    public Dataset(string name, bool keyInHeader, string baseUrl)
    {
        Name = name;
        KeyInHeader = keyInHeader;
        BaseUrl = baseUrl;
    }

    public void AddEndpoint(RequestMethod method, string url, StringContent requestBody, List<string> requestVariables)
    {
        this.Endpoints.Add(new Endpoint(method, url, requestBody, requestVariables));
    }
}