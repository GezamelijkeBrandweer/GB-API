namespace MIC_RequestSender.Domain;

public class Dataset
{
    public string Name { get; set; }
    public bool KeyInHeader { get; set; }
    public string BaseUrl { get; set; }
    public List<Endpoint> Endpoints { get; set; }

    public Dataset(string name, bool keyInHeader, string baseUrl, List<Endpoint> endpoints)
    {
        Name = name;
        KeyInHeader = keyInHeader;
        BaseUrl = baseUrl;
        Endpoints = endpoints;
    }
}