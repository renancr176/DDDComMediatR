using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings
{
    public class PhoneTypeMapping : EntityIntIdMap<PhoneType>
    {

        public override void Configure(EntityTypeBuilder<PhoneType> builder)
        {
            base.Configure(builder);

            builder.ToTable("PhoneTypes");

            builder.Property(entity => entity.Name)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(entity => entity.Active)
                .HasColumnOrder(3);

            builder.Property(entity => entity.CreatedAt)
                .HasColumnOrder(4);

            builder.Property(entity => entity.UpdatedAt)
                .HasColumnOrder(5);

            builder.Property(entity => entity.DeletedAt)
                .HasColumnOrder(6);

            #region Relationships

            builder.HasMany(entity => entity.Phones)
                .WithOne(entity => entity.PhoneType)
                .HasForeignKey(entity => entity.PhoneTypeId);

            #endregion
        }
    }
}
