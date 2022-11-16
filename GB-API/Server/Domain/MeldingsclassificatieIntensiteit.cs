using Newtonsoft.Json;

namespace GB_API.Server.Domain;

public class MeldingsclassificatieIntensiteit
{
    public long Id { get; set; }
    public int Punten { get; set; }
    
    [System.Text.Json.Serialization.JsonIgnore]
    public Dienst Dienst { get; set; }
    
    public Meldingsclassificatie Meldingsclassificatie { get; set; }
    
    public MeldingsclassificatieIntensiteit()
    {
    }
    
    public MeldingsclassificatieIntensiteit(int punten, Dienst dienst, Meldingsclassificatie meldingsclassificatie)
    {
        Punten = punten;
        Dienst = dienst;
        Meldingsclassificatie = meldingsclassificatie;
    }
}