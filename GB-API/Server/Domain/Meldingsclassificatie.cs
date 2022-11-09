namespace GB_API.Server.Domain;

public class Meldingsclassificatie
{
    public long Id { get; set; }
    public string Naam { get; set; }
    
    // deze private list is voor de many-to-many voor incident.
    private ICollection<Incident> _incidents;
    
    public Meldingsclassificatie(){ }
    
    public Meldingsclassificatie(long id, string naam)
    {
        Id = id;
        Naam = naam;
    }
}