using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings
{
    public class CompanyCnMapping : EntityMap<CompanyCn>
    {

        public override void Configure(EntityTypeBuilder<CompanyCn> builder)
        {
            base.Configure(builder);

            builder.ToTable("CompanyCns");

            builder.HasIndex(entity => new { entity.CompanyId, entity.CnId })
                .IsUnique();

            builder.Property(entity => entity.CompanyId)
                .HasColumnOrder(2);

            builder.Property(entity => entity.CnId)
                .HasColumnOrder(3);

            builder.Property(entity => entity.CreatedAt)
                .HasColumnOrder(4);

            builder.Property(entity => entity.UpdatedAt)
                .HasColumnOrder(5);

            builder.Property(entity => entity.DeletedAt)
                .HasColumnOrder(6);

            #region Relationships

            builder.HasOne(entity => entity.Company)
                .WithMany(entity => entity.CompanyCns)
                .HasForeignKey(entity => entity.CompanyId);

            builder.HasOne(entity => entity.Cns)
                .WithMany(entity => entity.CompanyCns)
                .HasForeignKey(entity => entity.CnId);

            #endregion
        }
    }
}
