using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nikitin.FederalSubjects.Database.Models;

public sealed class FederalSubjectTypeDbModel
{
    public short Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<FederalSubjectDbModel> FederalSubjects { get; init; } = new List<FederalSubjectDbModel>();
}

internal static class FederalSubjectTypeDbModelExtensions
{
    internal static void Configure(this EntityTypeBuilder<FederalSubjectTypeDbModel> entity)
    {
        entity.ToTable("federal_subjects_types", "federal_subjects");

        entity.HasKey(e => e.Id).HasName("federal_subjects_types_pkey");

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(128);

        entity.HasIndex(e => e.Name, "federal_subjects_types_name_key").IsUnique();
    }
}
