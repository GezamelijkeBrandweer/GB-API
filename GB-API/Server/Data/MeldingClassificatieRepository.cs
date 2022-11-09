using GB_API.Server.Domain;

namespace GB_API.Server.Data;

public class MeldingClassificatieRepository : IEntityRepository<Meldingsclassificatie>
{
    private readonly MICDbContext _context;

    public MeldingClassificatieRepository(MICDbContext context)
    {
        _context = context;
    }

    public List<Meldingsclassificatie> FindAll()
    {
        return _context.MeldingsClassificaties.ToList();
    }

    public void Save(Meldingsclassificatie t)
    {
        _context.Add(t);
        _context.SaveChanges();
    }

    public void DeleteById(long id)
    {
        var meldingsClassificaties = _context.MeldingsClassificaties;
        var meldingsClassificatiesToRemove = GetById(id);
        if (meldingsClassificatiesToRemove == null) throw new NullReferenceException();
        meldingsClassificaties.Remove(meldingsClassificatiesToRemove);
        _context.SaveChanges();
    }

    public Meldingsclassificatie? GetById(long id)
    {
        return _context.MeldingsClassificaties.SingleOrDefault(classificaties => classificaties.Id == id);
    }

    public void SaveList(List<Meldingsclassificatie> meldingen)
    {
        _context.AddRange(meldingen);
        _context.SaveChanges();
    }
}