using GB_API.Server.Domain;
using Microsoft.EntityFrameworkCore;

namespace GB_API.Server.Data.LocatieDB;

public class LocatieContext : DbContext
{
    public DbSet<Locatie> Locaties { get; set; } = null!;

    public LocatieContext(DbContextOptions options) : base(options)
    {
    }
    
}