using GB_API.Server.Domain;

namespace GB_API.Server.Data;

public class MeldingClassificatieRepository : IEntityRepository<MeldingsClassificaties>
{
    private readonly MICDbContext _context;

    public MeldingClassificatieRepository(MICDbContext context)
    {
        _context = context;
    }

    public List<MeldingsClassificaties> FindAll()
    {
        return _context.MeldingsClassificaties.ToList();
    }

    public void Save(MeldingsClassificaties t)
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

    public MeldingsClassificaties? GetById(long id)
    {
        return _context.MeldingsClassificaties.SingleOrDefault(classificaties => classificaties.Id == id);
    }
}