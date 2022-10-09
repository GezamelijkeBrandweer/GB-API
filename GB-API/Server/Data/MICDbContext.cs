using GB_API.Server.Domain;
using Microsoft.EntityFrameworkCore;

namespace GB_API.Server.Data;

public class MICDbContext : DbContext
{
    public DbSet<Locatie> Locaties { get; set; }
    public DbSet<Incident> Incidents { get; set; }
    
    
    public MICDbContext(DbContextOptions<MICDbContext> options)
    : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Incident>().Navigation(i => i.Locatie).AutoInclude();
        modelBuilder.UseSerialColumns();
    }
}