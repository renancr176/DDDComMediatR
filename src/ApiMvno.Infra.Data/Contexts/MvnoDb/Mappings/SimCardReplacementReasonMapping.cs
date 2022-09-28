using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings
{
    public class SimCardReplacementReasonMapping : EntityMap<SimCardReplacementReason>
    {

        public override void Configure(EntityTypeBuilder<SimCardReplacementReason> builder)
        {
            base.Configure(builder);

            builder.ToTable("SimCardReplacementReasons");

            builder.Property(entity => entity.Description)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(entity => entity.Active)
                .HasColumnOrder(3);

            builder.Property(entity => entity.CreatedAt)
                .HasColumnOrder(4);

            builder.Property(entity => entity.UpdatedAt)
                .HasColumnOrder(5);

            builder.Property(entity => entity.DeletedAt)
                .HasColumnOrder(6);

            #region Relationships

            builder.HasMany(entity => entity.CompanySimCardReplacementReasons)
                .WithOne(entity => entity.SimCardReplacementReason)
                .HasForeignKey(entity => entity.SimCardReplacementReasonId);

            #endregion
        }

    }
}
