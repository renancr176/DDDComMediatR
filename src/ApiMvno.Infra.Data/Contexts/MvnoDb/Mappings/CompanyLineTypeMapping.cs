using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings
{
    public class CompanyLineTypeMapping : EntityMap<CompanyLineType>
    {

        public override void Configure(EntityTypeBuilder<CompanyLineType> builder)
        {
            base.Configure(builder);

            builder.ToTable("CompanyLineTypes");

            builder.HasIndex(entity => new {entity.CompanyId, entity.LineTypeId})
                .IsUnique();

            builder.Property(entity => entity.CompanyId)
                .HasColumnOrder(2);

            builder.Property(entity => entity.LineTypeId)
                .HasColumnOrder(3);

            builder.Property(entity => entity.Active)
                .HasColumnOrder(4);

            builder.Property(entity => entity.TaxProductId)
                .HasColumnOrder(5);

            builder.Property(entity => entity.CreatedAt)
                .HasColumnOrder(6);

            builder.Property(entity => entity.UpdatedAt)
                .HasColumnOrder(7);

            builder.Property(entity => entity.DeletedAt)
                .HasColumnOrder(8);

            #region Relationships

            builder.HasOne(entity => entity.LineType)
                .WithMany(entity => entity.CompanyLineTypes)
                .HasForeignKey(entity => entity.LineTypeId);

            #endregion
        }

    }
}
