namespace GB_API.Server.Domain;

public class Karakteristiek
{
    public long Id { get; set; }
    public string Naam { get; set; }
    public string Type { get; set; }
    public int VolgNr { get; set; }
    public string Waarde { get; set; }
    
    // deze private list is voor de many-to-many voor incident.
    private ICollection<Incident> _incidents;

    public Karakteristiek() { }

    public Karakteristiek(string naam, string type, int volgNr, string waarde)
    {
        Naam = naam;
        Type = type;
        VolgNr = volgNr;
        Waarde = waarde;
    }
}