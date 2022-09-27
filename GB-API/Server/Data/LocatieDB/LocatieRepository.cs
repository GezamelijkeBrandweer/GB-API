using GB_API.Server.Domain;

namespace GB_API.Server.Data.LocatieDB;

public class LocatieRepository : IEntityRepository<Locatie>
{
    private readonly LocatieContext _context;

    public LocatieRepository(LocatieContext context)
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