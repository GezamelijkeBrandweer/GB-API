using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB_API.Server.Domain;

public class Incident
{
    public long Id { get; set; }
    public string Name { get; set; }
    public Locatie Locatie { get; set; }

    public Incident(){}
    
    public Incident(string name, Locatie locatie)
    {
        Name = name;
        Locatie = locatie;
    }
    
    
}