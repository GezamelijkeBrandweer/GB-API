namespace MIC_RequestSender.Domain;

public class Dataset
{
    public string Name { get; set; }
    public bool KeyInHeader { get; set; }
    public string BaseUrl { get; set; }
}