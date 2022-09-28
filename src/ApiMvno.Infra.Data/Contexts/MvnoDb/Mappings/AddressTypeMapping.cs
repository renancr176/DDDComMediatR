using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings;

public class AddressTypeMapping : EntityIntIdMap<AddressType>
{
    public override void Configure(EntityTypeBuilder<AddressType> builder)
    {
        base.Configure(builder);

        builder.ToTable("AddressTypes");

        builder.HasIndex(entity => entity.Type)
            .IsUnique();

        builder.Property(entity => entity.Type)
            .HasColumnOrder(2);

        builder.Property(entity => entity.Name)
            .HasMaxLength(50)
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

        #region Realationships

        builder.HasMany(entity => entity.Addresses)
            .WithOne(entity => entity.AddressType)
            .HasForeignKey(entity => entity.AddressTypeId);

        #endregion
    }
}