namespace GB_API.Server.Domain;

public class Intensiteit
{
    public long Id { get; set; }
    public int Score { get; set; }
    public Dienst Dienst { get; set; }

    public Intensiteit()
    {
        
    }
    
    public Intensiteit(int score, Dienst dienst)
    {
        Score = score;
        Dienst = dienst;
    }
}