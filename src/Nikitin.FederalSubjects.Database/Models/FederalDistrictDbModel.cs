using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nikitin.FederalSubjects.Database.Models;

public sealed class FederalDistrictDbModel
{
    public short Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<FederalSubjectDbModel> FederalSubjects { get; init; } = new List<FederalSubjectDbModel>();
}

internal static class FederalDistrictDbModelExtensions
{
    internal static void Configure(this EntityTypeBuilder<FederalDistrictDbModel> entity)
    {
        entity.ToTable("federal_districts", "federal_subjects");

        entity.HasKey(e => e.Id).HasName("federal_districts_pkey");

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(128);

        entity.HasIndex(e => e.Name, "federal_districts_name_key").IsUnique();
    }
}
