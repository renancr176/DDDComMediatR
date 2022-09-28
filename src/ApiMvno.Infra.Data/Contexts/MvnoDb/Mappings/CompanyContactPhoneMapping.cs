using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings
{
    public class CompanyContactPhoneMapping : EntityMap<CompanyContactPhone>
    {

        public override void Configure(EntityTypeBuilder<CompanyContactPhone> builder)
        {
            base.Configure(builder);

            builder.ToTable("CompanyContactPhones");

            builder.HasIndex(entity => new { entity.CompanyContactId, entity.PhoneId })
                .IsUnique();

            builder.Property(entity => entity.CompanyContactId)
                .HasColumnOrder(2);

            builder.Property(entity => entity.PhoneId)
                .HasColumnOrder(3);

            builder.Property(entity => entity.CreatedAt)
                .HasColumnOrder(4);

            builder.Property(entity => entity.UpdatedAt)
                .HasColumnOrder(5);

            builder.Property(entity => entity.DeletedAt)
                .HasColumnOrder(6);

            #region Relationships

            builder.HasOne(entity => entity.CompanyContact)
                .WithMany(entity => entity.CompanyContactPhones)
                .HasForeignKey(entity => entity.CompanyContactId);

            builder.HasOne(entity => entity.Phone)
                .WithMany(entity => entity.CompanyContactPhones)
                .HasForeignKey(entity => entity.PhoneId);

            #endregion
        }

    }
}
