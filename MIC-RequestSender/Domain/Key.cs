namespace MIC_RequestSender.Domain;

public class Key
{
    public string Value { get; set; }
    public KeyType KeyType { get; set; }

    public Key(string value, KeyType keyType)
    {
        Value = value;
        KeyType = keyType;
    }
}