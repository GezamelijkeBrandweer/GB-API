using GB_API.Server.Domain;

namespace GB_API.Server.Data;

public class ReadIntensiteitenRepository
{
    private readonly MICDbContext _context;

    public ReadIntensiteitenRepository(MICDbContext context)
    {
        _context = context;
    }

    public List<MeldingsclassificatieIntensiteit>? GetClassificatieIntensiteitenFromDienstAndClassificatie(Dienst dienst, 
        Meldingsclassificatie meldingsclassificatie)
    {
        return _context.MeldingIntensiteiten
            .Where(m => m.Dienst.Id == dienst.Id)
            .Where(m => m.Meldingsclassificatie.Id == meldingsclassificatie.Id)
            .ToList();
    }
    
    public List<KarakteristiekIntensiteit>? GetKarakteristiekIntensiteitenFromDienstAndKarakteristieken(Dienst dienst, 
        List<Karakteristiek> karakteristieken)
    {
        List<long> karakteristiekenIds = karakteristieken.Select(k => k.Id).ToList();
        return _context.KarakteristiekIntensiteiten
            .Where(k => k.Dienst.Id == dienst.Id)
            .Where(k => karakteristiekenIds.Contains(k.Karakteristiek.Id))
            .ToList();
    }
    
    
    
}