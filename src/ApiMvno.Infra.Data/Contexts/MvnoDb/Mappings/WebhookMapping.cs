using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings
{
    public class WebhookMapping : EntityMap<Webhook>
    {

        public override void Configure(EntityTypeBuilder<Webhook> builder)
        {
            base.Configure(builder);

            builder.ToTable("Webhooks");

            builder.Property(entity => entity.CompanyWebhookId)
                .HasColumnOrder(2);

            builder.Property(entity => entity.Retries)
                .HasColumnOrder(3);

            builder.Property(entity => entity.SendDate)
                .HasColumnOrder(4);

            builder.Property(entity => entity.SuccessResponse)
                .HasColumnOrder(5);

            builder.Property(entity => entity.JsonData)
                .HasColumnOrder(6);

            builder.Property(entity => entity.ResponseJson)
                .HasColumnOrder(7);

            builder.Property(entity => entity.CreatedAt)
                .HasColumnOrder(8);

            builder.Property(entity => entity.UpdatedAt)
                .HasColumnOrder(9);

            builder.Property(entity => entity.DeletedAt)
                .HasColumnOrder(10);

            #region Relationships

            builder.HasOne(entity => entity.CompanyWebhook)
                .WithMany(entity => entity.Webhooks)
                .HasForeignKey(entity => entity.CompanyWebhookId);

            #endregion
        }

    }
}
