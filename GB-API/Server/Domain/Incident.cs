using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GB_API.Server.Domain.VerkeersIncident;

namespace GB_API.Server.Domain;

public class Incident
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Name { get; set; }
    public Locatie Locatie { get; set; }
    public List<TrafficIncident> VerkeersIncidenten { get; set; } = new();

    public Incident(long id, string name, Locatie locatie)
    {
        Id = id;
        Name = name;
        Locatie = locatie;
    }

    // Lege constructor voor testing
    public Incident(long id)
    {
        this.Id = id;
    }

    public void AddVerkeersIncident(TrafficIncident verkeersIncident)
    {
        this.VerkeersIncidenten.Add(verkeersIncident);
    }
}