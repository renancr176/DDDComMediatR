using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings
{
    public class CompanyWebhookMapping : EntityMap<CompanyWebhook>
    {

        public override void Configure(EntityTypeBuilder<CompanyWebhook> builder)
        {
            base.Configure(builder);

            builder.ToTable("CompanyWebhooks");

            builder.HasIndex(entity => new { entity.CompanyId, entity.EventTypeId })
                .IsUnique();

            builder.Property(entity => entity.CompanyId)
                .HasColumnOrder(2);

            builder.Property(entity => entity.EventTypeId)
                .HasColumnOrder(3);

            builder.Property(entity => entity.Url)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnOrder(4);

            builder.Property(entity => entity.Token)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnOrder(5);

            builder.Property(entity => entity.CreatedAt)
                .HasColumnOrder(6);

            builder.Property(entity => entity.UpdatedAt)
                .HasColumnOrder(7);

            builder.Property(entity => entity.DeletedAt)
                .HasColumnOrder(8);

            #region Relationships

            builder.HasOne(entity => entity.Company)
                .WithMany(entity => entity.CompanyWebhooks)
                .HasForeignKey(entity => entity.CompanyId);

            builder.HasOne(entity => entity.EventType)
                .WithMany(entity => entity.CompanyWebhooks)
                .HasForeignKey(entity => entity.EventTypeId);

            #endregion
        }

    }
}
