namespace GB_API.Server.Domain;

public class KarakteristiekIntensiteit
{
    public long Id { get; set; }
    public int Punten { get; set; }
    
    public Dienst Dienst { get; set; }
    
    public Karakteristiek Karakteristiek { get; set; }

    public KarakteristiekIntensiteit() { }
    
    public KarakteristiekIntensiteit(int punten, Dienst dienst, Karakteristiek karakteristiek)
    {
        this.Punten = punten;
        this.Dienst = dienst;
        this.Karakteristiek = karakteristiek;
    }

}