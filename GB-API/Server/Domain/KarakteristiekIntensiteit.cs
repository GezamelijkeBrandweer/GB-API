namespace GB_API.Server.Domain;

public class KarakteristiekIntensiteit
{
    public long Id { get; set; }
    public int Punten { get; set; }
    
    public Dienst Dienst { get; set; }

    public KarakteristiekIntensiteit() { }
    
    public KarakteristiekIntensiteit(int punten, Dienst dienst)
    {
        this.Punten = punten;
        this.Dienst = dienst;
    }

}