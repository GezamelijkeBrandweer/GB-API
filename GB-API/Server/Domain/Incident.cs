namespace GB_API.Server.Domain;

public class Incident
{
    public long Id { get; set; }
    public string Name { get; set; }
    public Locatie Locatie { get; set; }
    public ICollection<Intensiteit> Intensiteiten { get; set; } 
    public ICollection<Karakteristiek> Karakteristieken { get; set; }
    public Meldingsclassificatie Meldingsclassificatie { get; set; }

    public Incident(){}

    public Incident(string name, Locatie locatie, Meldingsclassificatie meldingsclassificatie)
    {
        Name = name;
        Locatie = locatie;
        Meldingsclassificatie = meldingsclassificatie;
        Karakteristieken = new List<Karakteristiek>();
        Intensiteiten = new List<Intensiteit>();
    }

    public void AddIntensiteit(Dienst dienst)
    {
        List<long> karakteristiekenIds = Karakteristieken.Select(k => k.Id).ToList();
        Intensiteiten.Add(new Intensiteit(dienst, Meldingsclassificatie, karakteristiekenIds));
    }
}