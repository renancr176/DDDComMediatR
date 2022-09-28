using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings;

public class DueDayMapping : EntityMap<DueDay>
{
    public override void Configure(EntityTypeBuilder<DueDay> builder)
    {
        base.Configure(builder);

        builder.ToTable("DueDays");

        builder.Property(entity => entity.Day)
            .HasColumnOrder(2);

        builder.Property(entity => entity.CycleStart)
            .HasColumnOrder(3);

        builder.Property(entity => entity.CycleEnd)
            .HasColumnOrder(4);

        builder.Property(entity => entity.Name)
            .HasMaxLength(50)
            .HasColumnOrder(5);

        builder.Property(entity => entity.Description)
            .HasMaxLength(50)
            .HasColumnOrder(6);

        builder.Property(entity => entity.Active)
            .HasColumnOrder(7);

        builder.Property(entity => entity.CreatedAt)
            .HasColumnOrder(8);

        builder.Property(entity => entity.UpdatedAt)
            .HasColumnOrder(9);

        builder.Property(entity => entity.DeletedAt)
            .HasColumnOrder(10);

        #region Relationships

        builder.HasMany(entity => entity.CompanyDueDays)
            .WithOne(entity => entity.DueDay)
            .HasForeignKey(entity => entity.DueDayId);

        #endregion
    }
}