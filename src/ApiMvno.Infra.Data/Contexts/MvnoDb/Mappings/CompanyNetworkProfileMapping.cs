using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings
{
    public class CompanyNetworkProfileMapping : EntityMap<CompanyNetworkProfile>
    {

        public override void Configure(EntityTypeBuilder<CompanyNetworkProfile> builder)
        {
            base.Configure(builder);

            builder.ToTable("CompanyNetworkProfiles");

            builder.Property(entity => entity.CompanyId)
                .HasColumnOrder(2);

            builder.Property(entity => entity.PartnerCode)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnOrder(3);

            builder.Property(entity => entity.Apn)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnOrder(4);

            builder.Property(entity => entity.DownloadSpeed)
                .HasDefaultValue(0)
                .IsRequired()
                .HasColumnOrder(5);

            builder.Property(entity => entity.UpdloadSpeed)
                .HasDefaultValue(0)
                .IsRequired()
                .HasColumnOrder(6);

            builder.Property(entity => entity.CreatedAt)
                .HasColumnOrder(7);

            builder.Property(entity => entity.UpdatedAt)
                .HasColumnOrder(8);

            builder.Property(entity => entity.DeletedAt)
                .HasColumnOrder(9);

            #region Relationships

            builder.HasOne(entity => entity.Company)
                .WithMany(entity => entity.CompanyNetworkProfiles)
                .HasForeignKey(entity => entity.CompanyId);

            #endregion
        }

    }
}
