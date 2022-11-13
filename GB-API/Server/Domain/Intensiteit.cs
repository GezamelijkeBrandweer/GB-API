namespace GB_API.Server.Domain;

public class Intensiteit
{
    public long Id { get; set; }
    public int Score { get; set; }
    public Dienst Dienst { get; set; }

    public Intensiteit()
    {
        
    }
    
    public Intensiteit(Dienst dienst, Meldingsclassificatie classificatie, List<long> karakteristiekenIds)
    {
        Dienst = dienst;
        Score = GetTotalScore(classificatie, karakteristiekenIds);
    }
    
    private int GetTotalScore(Meldingsclassificatie classificatie, List<long> karakteristiekenIds)
    {
        int total = 0;
        
        var kIntensiteitenAll = Dienst.KarakteristiekIntensiteiten;
        var mIntensiteitenAll = Dienst.MeldingsclassificatieIntensiteiten;
        
        // Hier wordt nog gefilterd op de classificatie en karakteristieken van het incident
        // Duurt ook lang
        var mIntensiteiten = mIntensiteitenAll
            .Where(mIntensiteit => mIntensiteit.Meldingsclassificatie.Id == classificatie.Id)
            .ToList();
        
        var kIntensiteiten = kIntensiteitenAll
            .Where(kIntensiteit => karakteristiekenIds.Contains(kIntensiteit.Karakteristiek.Id))
            .ToList();
        
        kIntensiteiten.ForEach(kIntensiteit => total += kIntensiteit.Punten);
        mIntensiteiten.ForEach(mIntensiteit => total += mIntensiteit.Punten);
        
        return total;
    }
    
    
}