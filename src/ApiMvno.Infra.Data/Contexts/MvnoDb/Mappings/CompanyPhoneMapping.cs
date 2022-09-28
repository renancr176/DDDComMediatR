using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings
{
    public class CompanyPhoneMapping : EntityMap<CompanyPhone>
    {

        public override void Configure(EntityTypeBuilder<CompanyPhone> builder)
        {
            base.Configure(builder);

            builder.ToTable("CompanyPhones");

            builder.HasIndex(entity => new { entity.CompanyId, entity.PhoneId })
                .IsUnique();

            builder.Property(entity => entity.CompanyId)
                .HasColumnOrder(3);

            builder.Property(entity => entity.PhoneId)
                .HasColumnOrder(4);

            builder.Property(entity => entity.CreatedAt)
                .HasColumnOrder(5);

            builder.Property(entity => entity.UpdatedAt)
                .HasColumnOrder(6);

            builder.Property(entity => entity.DeletedAt)
                .HasColumnOrder(7);

            #region Relationships

            builder.HasOne(entity => entity.Company)
                .WithMany(entity => entity.CompanyPhones)
                .HasForeignKey(entity => entity.CompanyId);

            builder.HasOne(entity => entity.Phone)
                .WithMany(entity => entity.CompanyPhones)
                .HasForeignKey(entity => entity.PhoneId);

            #endregion
        }

    }
}
