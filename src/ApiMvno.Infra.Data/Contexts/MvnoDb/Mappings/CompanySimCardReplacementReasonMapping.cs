using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings
{
    public class CompanySimCardReplacementReasonMapping : EntityMap<CompanySimCardReplacementReason>
    {

        public override void Configure(EntityTypeBuilder<CompanySimCardReplacementReason> builder)
        {
            base.Configure(builder);

            builder.ToTable("CompanySimCardReplacementReasons");

            builder.HasIndex(entity => new {entity.CompanyId, SimCardReplacementReasonsId = entity.SimCardReplacementReasonId})
                .IsUnique();

            builder.Property(entity => entity.CompanyId)
                .HasColumnOrder(2);

            builder.Property(entity => entity.SimCardReplacementReasonId)
                .HasColumnOrder(3);

            builder.Property(entity => entity.CreatedAt)
                .HasColumnOrder(4);

            builder.Property(entity => entity.UpdatedAt)
                .HasColumnOrder(5);

            builder.Property(entity => entity.DeletedAt)
                .HasColumnOrder(6);

            #region Relationships

            builder.HasOne(entity => entity.Company)
                .WithMany(entity => entity.CompanySimCardReplacementReasons)
                .HasForeignKey(entity => entity.CompanyId);

            builder.HasOne(entity => entity.SimCardReplacementReason)
                .WithMany(entity => entity.CompanySimCardReplacementReasons)
                .HasForeignKey(entity => entity.SimCardReplacementReasonId);

            #endregion
        }

    }
}
