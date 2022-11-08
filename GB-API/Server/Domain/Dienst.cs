namespace GB_API.Server.Domain;

public class Dienst
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<KarakteristiekIntensiteit> KarakteristiekIntensiteiten { get; set; }
    public ICollection<MeldingsclassificatieIntensiteit> MeldingsclassificatieIntensiteiten { get; set; }

    public Dienst(){}

    public Dienst(string name)
    {
        Name = name;
    }
}