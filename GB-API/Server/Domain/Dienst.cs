namespace GB_API.Server.Domain;
public class Dienst
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    [System.Text.Json.Serialization.JsonIgnore]
    public List<KarakteristiekIntensiteit> KarakteristiekIntensiteiten { get; set; } = new();
    [System.Text.Json.Serialization.JsonIgnore]
    public List<MeldingsclassificatieIntensiteit> MeldingsclassificatieIntensiteiten { get; set; } = new();

    public Dienst(){}

    public Dienst(string name)
    {
        Name = name;
    }
}