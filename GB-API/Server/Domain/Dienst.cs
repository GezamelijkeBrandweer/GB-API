namespace GB_API.Server.Domain;

public class Dienst
{
    public long Id { get; set; }
    public string Name { get; set; }

    public List<KarakteristiekIntensiteit> KarakteristiekIntensiteiten { get; set; } = new();
    public List<MeldingsclassificatieIntensiteit> MeldingsclassificatieIntensiteiten { get; set; } = new();

    public Dienst(){}

    public Dienst(string name)
    {
        Name = name;
    }

    //TODO (niet hier) een check doen bij het opslaan dat een dienst niet meerdere keer dezelfde karakteristiek kan opgeven
    public void AddKarakteristiekIntensiteit(int punten, Karakteristiek karakteristiek)
    {
        KarakteristiekIntensiteiten.Add(new KarakteristiekIntensiteit(punten, this, karakteristiek));
    }
    
    //TODO (niet hier) een check doen bij het opslaan dat een dienst niet meerdere keer dezelfde MeldingClassificatie kan opgeven
    public void AddMeldingsclassificatieIntensiteit(int punten, Meldingsclassificatie mClassificatie)
    {
        MeldingsclassificatieIntensiteiten.Add(new MeldingsclassificatieIntensiteit(punten, this, mClassificatie));
    }
}