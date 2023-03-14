using Microsoft.EntityFrameworkCore;
using Nikitin.FederalSubjects.Database.Models;

namespace Nikitin.FederalSubjects.Database;

public class AppDbContext : DbContext
{
    public DbSet<FederalDistrictDbModel> FederalDistricts { get; set; } = null!;
    public DbSet<FederalSubjectDbModel> FederalSubjects { get; set; } = null!;
    public DbSet<FederalSubjectTypeDbModel> FederalSubjectsTypes { get; set; } = null!;

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FederalDistrictDbModel>().Configure();
        modelBuilder.Entity<FederalSubjectDbModel>().Configure();
        modelBuilder.Entity<FederalSubjectTypeDbModel>().Configure();
    }
}
