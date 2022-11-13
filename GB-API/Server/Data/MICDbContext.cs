using GB_API.Server.Domain;
using Microsoft.EntityFrameworkCore;

namespace GB_API.Server.Data;

public class MICDbContext : DbContext
{
    public DbSet<Locatie> Locaties { get; set; }
    public DbSet<Incident> Incidents { get; set; }
    public DbSet<Meldingsclassificatie> MeldingsClassificaties { get; set; }
    public DbSet<Karakteristiek> Karakteristieks { get; set; }
    public DbSet<Dienst> Diensten { get; set;}
    public DbSet<KarakteristiekIntensiteit> KarakteristiekIntensiteiten { get; set; }
    public DbSet<MeldingsclassificatieIntensiteit> MeldingIntensiteiten { get; set; }
    

    public MICDbContext(DbContextOptions<MICDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Configure default schema
        modelBuilder.HasDefaultSchema("MIC-DB");

        //map entity to table
        modelBuilder.Entity<Incident>().ToTable("incident");
        modelBuilder.Entity<Dienst>().ToTable("dienst");
        modelBuilder.Entity<Intensiteit>().ToTable("intensiteit");
        modelBuilder.Entity<Karakteristiek>().ToTable("karakteristiek");
        modelBuilder.Entity<KarakteristiekIntensiteit>().ToTable("kIntensiteit");
        modelBuilder.Entity<Meldingsclassificatie>().ToTable("meldingClassificatie");
        modelBuilder.Entity<MeldingsclassificatieIntensiteit>().ToTable("mIntensiteit");
        modelBuilder.Entity<Locatie>().ToTable("locatie");

        //Configure relations
        modelBuilder
            .Entity<Incident>()
            .HasMany(i => i.Karakteristieken)
            .WithMany("_incidents")
            .UsingEntity(j => j.ToTable("Karakteristiek_Incident"));
        
        // modelBuilder.Entity<Incident>().HasOne<Meldingsclassificatie>();
        // modelBuilder.Entity<Incident>().HasOne(i => i.Locatie);
        // modelBuilder.Entity<KarakteristiekIntensiteit>().HasOne<Karakteristiek>();
        // modelBuilder.Entity<MeldingsclassificatieIntensiteit>().HasOne<Meldingsclassificatie>();
        
        
        modelBuilder.UseSerialColumns();
    }
}