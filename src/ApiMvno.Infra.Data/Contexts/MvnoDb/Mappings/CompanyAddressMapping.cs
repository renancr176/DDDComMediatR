using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings
{
    public class CompanyAddressMapping : EntityMap<CompanyAddress>
    {

        public override void Configure(EntityTypeBuilder<CompanyAddress> builder)
        {
            base.Configure(builder);

            builder.ToTable("CompanyAddresses");

            builder.HasIndex(entity => new { entity.CompanyId, entity.AddressId })
                .IsUnique();

            builder.Property(entity => entity.CompanyId)
                .HasColumnOrder(2);

            builder.Property(entity => entity.AddressId)
                .HasColumnOrder(3);

            builder.Property(entity => entity.CreatedAt)
                .HasColumnOrder(4);

            builder.Property(entity => entity.UpdatedAt)
                .HasColumnOrder(5);

            builder.Property(entity => entity.DeletedAt)
                .HasColumnOrder(6);

            #region Relationships

            builder.HasOne(entity => entity.Company)
                .WithMany(entity => entity.CompanyAddress)
                .HasForeignKey(entity => entity.CompanyId);

            builder.HasOne(entity => entity.Address)
                .WithMany(entity => entity.CompanyAddresses)
                .HasForeignKey(entity => entity.AddressId);

            #endregion
        }

    }
}
