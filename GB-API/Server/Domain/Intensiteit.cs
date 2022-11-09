namespace GB_API.Server.Domain;

public class Intensiteit
{
    public long Id { get; set; }
    public int Score { get; }
    public Dienst Dienst { get; set; }

    public Intensiteit()
    {
        
    }
    
    public Intensiteit(Dienst dienst)
    {
        Dienst = dienst;
        Score = GetTotalScore();
    }
    
    private int GetTotalScore()
    {
        int total = 0;

        var kIntensiteiten = Dienst.KarakteristiekIntensiteiten;
        var mIntensiteiten = Dienst.MeldingsclassificatieIntensiteiten;
        
        kIntensiteiten.ForEach(kIntensiteit => total += kIntensiteit.Punten);
        mIntensiteiten.ForEach(mIntensiteit => total += mIntensiteit.Punten);
        
        return total;
    }
    
    
}