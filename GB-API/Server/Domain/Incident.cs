using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GB_API.Server.Domain.Traffic;

namespace GB_API.Server.Domain;

public class Incident
{
    public long Id { get; set; }
    public string Name { get; set; }
    public Locatie Locatie { get; set; }

    public int IntensiteitPunten { get; set; }
    public ICollection<Karakteristiek> KarakteristiekList { get; set; } 
    public MeldingsClassificaties MeldingsClassificaties { get; set; } = new();

    public List<TrafficIncident> VerkeersIncidenten { get; set; } = new();

    public Incident(){}
    
    public Incident(string name, Locatie locatie)
    {
        Name = name;
        Locatie = locatie;
    }
    
    public Incident(string name, MeldingsClassificaties meldingsClassificaties ,Locatie locatie)
    {
        Name = name;
        Locatie = locatie;
        MeldingsClassificaties = meldingsClassificaties;
        UpdateIntensiteitPunten();
    }
    
    public void AddVerkeersIncident(TrafficIncident verkeersIncident)
    {
        this.VerkeersIncidenten.Add(verkeersIncident);
    }
    public void AddKarkteristieken(Karakteristiek karakteristiek)
    {
        KarakteristiekList.Add(karakteristiek);
        UpdateIntensiteitPunten();
    }

    private void UpdateIntensiteitPunten()
    {
        int tempPunten = KarakteristiekList.Sum(karakteristiek => karakteristiek.IntensiteitPunten);
        tempPunten += MeldingsClassificaties.IntensiteitPunten;
        IntensiteitPunten = tempPunten;

    }
    
}