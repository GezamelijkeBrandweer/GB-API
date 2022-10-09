namespace GB_API.Server.Domain;

public class Karakteristiek
{
    public long Id { get; set; }
    public string Naam { get; set; }
    public string Type { get; set; }
    public int VolgNr { get; set; }
    public string Waarde { get; set; }

    public Karakteristiek() { }

    public Karakteristiek(string naam, string type, int volgNr, string waarde)
    {
        Naam = naam;
        Type = type;
        VolgNr = volgNr;
        Waarde = waarde;
    }
}