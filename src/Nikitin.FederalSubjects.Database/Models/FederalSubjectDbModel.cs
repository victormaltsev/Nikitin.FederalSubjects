using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nikitin.FederalSubjects.Database.Models;

public sealed class FederalSubjectDbModel
{
    public short Id { get; set; }
    public short FederalDistrictId { get; set; }
    public short FederalSubjectTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Content { get; set; }
    public FederalDistrictDbModel FederalDistrict { get; set; } = null!;
    public FederalSubjectTypeDbModel FederalSubjectType { get; set; } = null!;
}

internal static class FederalSubjectDbModelExtensions
{
    internal static void Configure(this EntityTypeBuilder<FederalSubjectDbModel> entity)
    {
        entity.ToTable("federal_subjects", "federal_subjects");

        entity.HasKey(e => e.Id).HasName("federal_subjects_pkey");

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.FederalDistrictId).HasColumnName("federal_district_id");
        entity.Property(e => e.FederalSubjectTypeId).HasColumnName("federal_subject_type_id");
        entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(128);
        entity.Property(e => e.Description).HasColumnName("description");
        entity.Property(e => e.Content).HasColumnName("content");

        entity.HasOne(d => d.FederalDistrict).WithMany(p => p.FederalSubjects)
            .HasForeignKey(d => d.FederalDistrictId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("federal_subjects_federal_district_id_fkey");

        entity.HasOne(d => d.FederalSubjectType).WithMany(p => p.FederalSubjects)
            .HasForeignKey(d => d.FederalSubjectTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("federal_subjects_federal_subject_type_id_fkey");

        entity.HasIndex(e => e.Name, "federal_subjects_name_key").IsUnique();
    }
}
