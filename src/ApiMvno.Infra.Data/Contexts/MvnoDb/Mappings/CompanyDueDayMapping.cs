using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings;

public class CompanyDueDayMapping : EntityMap<CompanyDueDay>
{
    public override void Configure(EntityTypeBuilder<CompanyDueDay> builder)
    {
        base.Configure(builder);

        builder.ToTable("CompanyDueDays");

        builder.HasIndex(entity => new {entity.CompanyId, entity.DueDayId})
            .IsUnique();

        builder.Property(entity => entity.CompanyId)
            .HasColumnOrder(2);

        builder.Property(entity => entity.DueDayId)
            .HasColumnOrder(3);

        builder.Property(entity => entity.CreatedAt)
            .HasColumnOrder(4);

        builder.Property(entity => entity.UpdatedAt)
            .HasColumnOrder(5);

        builder.Property(entity => entity.DeletedAt)
            .HasColumnOrder(6);

        #region Relationships

        builder.HasOne(entity => entity.Company)
            .WithMany(entity => entity.CompanyDueDays)
            .HasForeignKey(entity => entity.CompanyId);

        builder.HasOne(entity => entity.DueDay)
            .WithMany(entity => entity.CompanyDueDays)
            .HasForeignKey(entity => entity.DueDayId);

        builder.HasMany(entity => entity.CompanyAccountTypes)
            .WithOne(entity => entity.CompanyDueDay)
            .HasForeignKey(entity => entity.CompanyDueDayId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        #endregion
    }
}