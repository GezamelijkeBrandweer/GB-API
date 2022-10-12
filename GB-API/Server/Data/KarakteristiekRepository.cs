using GB_API.Server.Domain;

namespace GB_API.Server.Data;

public class KarakteristiekRepository : IEntityRepository<Karakteristiek>
{
    private readonly MICDbContext _context;

    public KarakteristiekRepository(MICDbContext context)
    {
        _context = context;
    }

    public List<Karakteristiek> FindAll()
    {
        return _context.Karakteristieks.ToList();
    }

    public void Save(Karakteristiek t)
    {
        _context.Add(t);
        _context.SaveChanges();
    }

    public void DeleteById(long id)
    {
        var kar = _context.Karakteristieks;
        var karakteristiek = GetById(id);
        if (karakteristiek == null) throw new NullReferenceException();
        kar.Remove(karakteristiek);
        _context.SaveChanges();
    }

    public Karakteristiek? GetById(long id)
    {
        return _context.Karakteristieks.SingleOrDefault(karakteristiek => karakteristiek.Id == id);
    }

    public void SaveList(IEnumerable<Karakteristiek> karakteristiek)
    {
        _context.AddRange(karakteristiek);
        _context.SaveChanges();
    }
}