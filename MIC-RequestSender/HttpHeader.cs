namespace MIC_RequestSender;

public class HttpHeader
{
    public string Name { get; set; }
    public string Value { get; set; }

    public HttpHeader(string name, string value)
    {
        Name = name;
        Value = value;
    }
}