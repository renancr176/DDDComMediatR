using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings;

public class CompanyMapping : EntityMap<Company>
{
    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        base.Configure(builder);

        builder.ToTable("Companies");

        builder.HasIndex(entity => entity.Document)
            .IsUnique();

        builder.Property(entity => entity.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnOrder(2);

        builder.Property(entity => entity.Email)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnOrder(3);

        builder.Property(entity => entity.Document)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnOrder(4);

        builder.Property(entity => entity.CreatedAt)
            .HasColumnOrder(19);

        builder.Property(entity => entity.UpdatedAt)
            .HasColumnOrder(20);

        builder.Property(entity => entity.DeletedAt)
            .HasColumnOrder(21);

        #region Relationships

        builder.HasMany(entity => entity.CompanyAddresses)
            .WithOne(entity => entity.Company)
            .HasForeignKey(entity => entity.CompanyId);

        builder.HasMany(entity => entity.CompanyPhones)
            .WithOne(entity => entity.Company)
            .HasForeignKey(entity => entity.CompanyId);

        #endregion
    }
}