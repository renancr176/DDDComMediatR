using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings
{
    public class AddressMapping : EntityMap<Address>
    {

        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            base.Configure(builder);

            builder.ToTable("Addresses");

            builder.Property(entity => entity.AddressTypeId)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(entity => entity.CountryId)
                .IsRequired()
                .HasColumnOrder(3);

            builder.Property(entity => entity.ZipCode)
                .HasMaxLength(9)
                .HasColumnOrder(4);

            builder.Property(entity => entity.State)
                .HasMaxLength(50)
                .HasColumnOrder(5);

            builder.Property(entity => entity.City)
                .HasMaxLength(50)
                .HasColumnOrder(6);

            builder.Property(entity => entity.Neighborhood)
                .HasMaxLength(50)
                .HasColumnOrder(7);

            builder.Property(entity => entity.StreetName)
                .HasMaxLength(50)
                .HasColumnOrder(8);

            builder.Property(entity => entity.StreetNumber)
                .HasColumnOrder(9);

            builder.Property(entity => entity.Details)
                .HasMaxLength(255)
                .HasColumnOrder(10);

            builder.Property(entity => entity.CreatedAt)
                .HasColumnOrder(11);

            builder.Property(entity => entity.UpdatedAt)
                .HasColumnOrder(12);

            builder.Property(entity => entity.DeletedAt)
                .HasColumnOrder(13);

            #region Relationships

            builder.HasOne(entity => entity.Country)
                .WithMany(entity => entity.Addresses)
                .HasForeignKey(entity => entity.CountryId);

            builder.HasOne(entity => entity.AddressType)
                .WithMany(entity => entity.Addresses)
                .HasForeignKey(entity => entity.AddressTypeId);

            #endregion
        }
    }
}
