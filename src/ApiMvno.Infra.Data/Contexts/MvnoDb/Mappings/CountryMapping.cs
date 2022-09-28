using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings
{
    public class CountryMapping : EntityMap<Country>
    {

        public override void Configure(EntityTypeBuilder<Country> builder)
        {
            base.Configure(builder);

            builder.ToTable("Countries");

            builder.Property(entity => entity.Name)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(entity => entity.PhoneCode)
                .HasMaxLength(10)
                .IsRequired()
                .HasColumnOrder(3);

            builder.Property(entity => entity.Active)
                .HasColumnOrder(4);

            builder.Property(entity => entity.CreatedAt)
                .HasColumnOrder(5);

            builder.Property(entity => entity.UpdatedAt)
                .HasColumnOrder(6);

            builder.Property(entity => entity.DeletedAt)
                .HasColumnOrder(7);

            #region Relationships

            builder.HasMany(entity => entity.Addresses)
                .WithOne(entity => entity.Country)
                .HasForeignKey(entity => entity.CountryId);

            #endregion
        }
    }
}
