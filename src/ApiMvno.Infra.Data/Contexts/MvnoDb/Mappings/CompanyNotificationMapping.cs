using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings
{
    internal class CompanyNotificationMapping : EntityMap<CompanyNotification>
    {
        public override void Configure(EntityTypeBuilder<CompanyNotification> builder)
        {
            base.Configure(builder);

            builder.ToTable("CompanyNotifications");

            builder.Property(entity => entity.CompanyId)
                .HasColumnOrder(2);

            builder.Property(entity => entity.Email)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnOrder(3);

            builder.Property(entity => entity.CreatedAt)
                .HasColumnOrder(4);

            builder.Property(entity => entity.UpdatedAt)
                .HasColumnOrder(5);

            builder.Property(entity => entity.DeletedAt)
                .HasColumnOrder(6);

            #region Relationships

            builder.HasOne(entity => entity.Company)
                .WithMany(entity => entity.CompanyNotifications)
                .HasForeignKey(entity => entity.CompanyId);

            #endregion
        }

    }
}
