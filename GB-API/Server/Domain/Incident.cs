namespace GB_API.Server.Domain;

public class Incident
{
    public long Id { get; set; }
    public string Name { get; set; }
    public Locatie Locatie { get; set; }

    public Incident(long id, string name, Locatie locatie)
    {
        Id = id;
        Name = name;
        Locatie = locatie;
    }
}