using GB_API.Server.Domain;

namespace GB_API.Server.Data;

public class DienstRepository : IEntityRepository<Dienst>
{
    private readonly MICDbContext _context;

    public DienstRepository(MICDbContext context)
    {
        _context = context;
    }
    
    public List<Dienst> FindAll()
    {
        return _context.Diensten.ToList();
    }

    public void Save(Dienst dienst)
    {
        _context.Diensten.Add(dienst);
        _context.SaveChanges();
    }

    public void DeleteById(long id)
    {
        var diensten = _context.Diensten;
        var dienstToRemove = GetById(id);
        if (dienstToRemove == null) throw new NullReferenceException();
        diensten.Remove(dienstToRemove);
        _context.SaveChanges();
    }

    public Dienst? GetById(long id)
    {
        return _context.Diensten.SingleOrDefault(dienst => dienst.Id == id);
    }
}