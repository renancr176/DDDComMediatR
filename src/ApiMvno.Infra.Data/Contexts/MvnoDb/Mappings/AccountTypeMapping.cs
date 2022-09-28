using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings;

public class AccountTypeMapping : EntityIntIdMap<AccountType>
{
    public override void Configure(EntityTypeBuilder<AccountType> builder)
    {
        base.Configure(builder);

        builder.ToTable("AccountTypes");

        builder.HasIndex(entity => entity.Type)
            .IsUnique();

        builder.Property(entity => entity.Type)
            .HasColumnOrder(2);

        builder.Property(entity => entity.Name)
            .HasMaxLength(20)
            .HasColumnOrder(3);

        builder.Property(entity => entity.RequireDueDay)
            .HasColumnOrder(4);

        builder.Property(entity => entity.Active)
            .HasColumnOrder(5);

        builder.Property(entity => entity.CreatedAt)
            .HasColumnOrder(6);

        builder.Property(entity => entity.UpdatedAt)
            .HasColumnOrder(7);

        builder.Property(entity => entity.DeletedAt)
            .HasColumnOrder(8);

        #region Relationships

        builder.HasMany(entity => entity.CompanyAccountTypes)
            .WithOne(entity => entity.AccountType)
            .HasForeignKey(entity => entity.AccountTypeId);

        #endregion
    }
}