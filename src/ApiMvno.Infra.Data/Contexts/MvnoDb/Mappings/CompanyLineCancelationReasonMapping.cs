using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings;

public class CompanyLineCancelationReasonMapping : EntityMap<CompanyLineCancelationReason>
{
    public override void Configure(EntityTypeBuilder<CompanyLineCancelationReason> builder)
    {
        base.Configure(builder);

        builder.ToTable("CompanyLineCancelationReasons");

        builder.HasIndex(entity => new { entity.CompanyId, entity.LineCancelationReasonId })
            .IsUnique();

        builder.Property(entity => entity.CompanyId)
            .HasColumnOrder(2);

        builder.Property(entity => entity.LineCancelationReasonId)
            .HasColumnOrder(3);

        builder.Property(entity => entity.CreatedAt)
            .HasColumnOrder(4);

        builder.Property(entity => entity.UpdatedAt)
            .HasColumnOrder(5);

        builder.Property(entity => entity.DeletedAt)
            .HasColumnOrder(6);

        #region Relationships

        builder.HasOne(entity => entity.Company)
            .WithMany(entity => entity.CompanyLineCancelationReasons)
            .HasForeignKey(entity => entity.CompanyId);

        builder.HasOne(entity => entity.LineCancelationReason)
            .WithMany(entity => entity.CompanyLineCancelationReasons)
            .HasForeignKey(entity => entity.LineCancelationReasonId);

        #endregion
    }
}