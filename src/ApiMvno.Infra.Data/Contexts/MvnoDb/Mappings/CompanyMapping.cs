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

        builder.Property(entity => entity.MainCompanyId)
            .HasColumnOrder(2);

        builder.Property(entity => entity.Email)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnOrder(3);

        builder.Property(entity => entity.National)
            .HasColumnOrder(4);

        builder.Property(entity => entity.Document)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnOrder(5);

        builder.Property(entity => entity.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnOrder(6);

        builder.Property(entity => entity.TradeName)
            .HasMaxLength(100)
            .HasColumnOrder(7);

        builder.Property(entity => entity.ValidateDocument)
            .HasColumnOrder(8);

        builder.Property(entity => entity.Active)
            .HasColumnOrder(9);

        builder.Property(entity => entity.SmallName)
            .HasMaxLength(100)
            .HasColumnOrder(10);

        builder.Property(entity => entity.MvnoCode)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnOrder(11);

        builder.Property(entity => entity.DealerCode)
            .HasMaxLength(20)
            .HasColumnOrder(12);

        builder.Property(entity => entity.LogoUrl)
            .HasMaxLength(255)
            .HasColumnOrder(13);

        builder.Property(entity => entity.SalesforceQueueName)
            .HasMaxLength(255)
            .HasColumnOrder(14);

        builder.Property(entity => entity.SalesforceCode)
            .HasMaxLength(255)
            .HasColumnOrder(15);

        builder.Property(entity => entity.JscCode)
            .HasMaxLength(255)
            .HasColumnOrder(16);

        builder.Property(entity => entity.CntPersonGroupId)
            .HasColumnOrder(17);

        builder.Property(entity => entity.CntCode)
            .HasMaxLength(255)
            .HasColumnOrder(18);

        builder.Property(entity => entity.CreatedAt)
            .HasColumnOrder(19);

        builder.Property(entity => entity.UpdatedAt)
            .HasColumnOrder(20);

        builder.Property(entity => entity.DeletedAt)
            .HasColumnOrder(21);

        #region Relationships

        builder.HasMany(entity => entity.CompanyAddress)
            .WithOne(entity => entity.Company)
            .HasForeignKey(entity => entity.CompanyId);

        builder.HasMany(entity => entity.CompanyContacts)
            .WithOne(entity => entity.Company)
            .HasForeignKey(entity => entity.CompanyId);

        builder.HasMany(entity => entity.CompanyPhones)
            .WithOne(entity => entity.Company)
            .HasForeignKey(entity => entity.CompanyId);

        builder.HasMany(entity => entity.CompanyLineTypes)
            .WithOne(entity => entity.Company)
            .HasForeignKey(entity => entity.CompanyId);

        builder.HasMany(entity => entity.CompanyCns)
            .WithOne(entity => entity.Company)
            .HasForeignKey(entity => entity.CompanyId);

        builder.HasMany(entity => entity.CompanySimCardReplacementReasons)
            .WithOne(entity => entity.Company)
            .HasForeignKey(entity => entity.CompanyId);

        builder.HasMany(entity => entity.CompanyWebhooks)
            .WithOne(entity => entity.Company)
            .HasForeignKey(entity => entity.CompanyId);

        builder.HasMany(entity => entity.CompanyNetworkProfiles)
            .WithOne(entity => entity.Company)
            .HasForeignKey(entity => entity.CompanyId);

        builder.HasMany(entity => entity.CompanyDueDays)
            .WithOne(entity => entity.Company)
            .HasForeignKey(entity => entity.CompanyId);

        builder.HasMany(entity => entity.CompanyAccountTypes)
            .WithOne(entity => entity.Company)
            .HasForeignKey(entity => entity.CompanyId);

        builder.HasMany(entity => entity.CompanyLineCancelationReasons)
            .WithOne(entity => entity.Company)
            .HasForeignKey(entity => entity.CompanyId);

        #endregion
    }
}