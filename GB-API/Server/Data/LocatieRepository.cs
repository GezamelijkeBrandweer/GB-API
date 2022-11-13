using GB_API.Server.Domain;

namespace GB_API.Server.Data;

public class LocatieRepository : IEntityRepository<Locatie>
{
    private readonly MICDbContext _context;

    public LocatieRepository(MICDbContext context)
    {
        _context = context;
    }

    public List<Locatie> FindAll()
    {
        return _context.Locaties.ToList();
    }

    public void Save(Locatie locatie)
    {
        _context.Add(locatie);
        _context.SaveChanges();
    }

    public void DeleteById(long id)
    {
        var locaties = _context.Locaties;
        var locatieToRemove = GetById(id);
        if (locatieToRemove == null) throw new NullReferenceException();
        locaties.Remove(locatieToRemove);
        _context.SaveChanges();
    }

    public Locatie? GetById(long id)
    {
       return _context.Locaties.SingleOrDefault(locatie => locatie.Id == id);
    }
}