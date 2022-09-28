using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings
{
    public class EventTypeMapping : EntityMap<EventType>
    {

        public override void Configure(EntityTypeBuilder<EventType> builder)
        {
            base.Configure(builder);

            builder.ToTable("EventTypes");

            builder.Property(entity => entity.Type)
                .HasColumnOrder(2);

            builder.Property(entity => entity.Name)
                .HasMaxLength(100)
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

            builder.HasMany(entity => entity.CompanyWebhooks)
                .WithOne(entity => entity.EventType)
                .HasForeignKey(entity => entity.EventTypeId);

            #endregion
        }
    }
}
