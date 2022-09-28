using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings;

public class CompanyAccountTypeMapping : EntityMap<CompanyAccountType>
{
    public override void Configure(EntityTypeBuilder<CompanyAccountType> builder)
    {
        base.Configure(builder);

        builder.ToTable("CompanyAccountTypes");

        builder.HasIndex(entity => new {entity.CompanyId, entity.AccountTypeId})
            .IsUnique();

        builder.Property(entity => entity.CompanyId)
            .HasColumnOrder(2);

        builder.Property(entity => entity.AccountTypeId)
            .HasColumnOrder(3);

        builder.Property(entity => entity.CompanyDueDayId)
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

        builder.HasOne(entity => entity.Company)
            .WithMany(entity => entity.CompanyAccountTypes)
            .HasForeignKey(entity => entity.CompanyId);

        builder.HasOne(entity => entity.AccountType)
            .WithMany(entity => entity.CompanyAccountTypes)
            .HasForeignKey(entity => entity.AccountTypeId);

        builder.HasOne(entity => entity.CompanyDueDay)
            .WithMany(entity => entity.CompanyAccountTypes)
            .HasForeignKey(entity => entity.CompanyDueDayId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        #endregion
    }
}