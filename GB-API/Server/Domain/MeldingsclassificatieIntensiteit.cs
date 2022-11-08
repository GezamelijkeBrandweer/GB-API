namespace GB_API.Server.Domain;

public class MeldingsclassificatieIntensiteit
{
    public long Id { get; set; }
    public string Punten { get; set; }
    
    public Dienst Dienst { get; set; }
    
    public MeldingsclassificatieIntensiteit()
    {
    }
    
    public MeldingsclassificatieIntensiteit(long id, string punten, Dienst dienst)
    {
        Id = id;
        Punten = punten;
        Dienst = dienst;
    }
}