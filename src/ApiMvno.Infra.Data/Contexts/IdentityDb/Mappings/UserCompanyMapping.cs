using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.IdentityDb.Mappings;

public class UserCompanyMapping : EntityMap<UserCompany>
{
    public override void Configure(EntityTypeBuilder<UserCompany> builder)
    {
        base.Configure(builder);

        builder.ToTable("UserCompanies");

        builder.HasIndex(e => new {e.UserId, e.CompanyId, e.DeletedAt})
            .IsUnique();

        builder.Property(e => e.UserId)
            .HasColumnOrder(2);

        builder.Property(e => e.CompanyId)
            .HasColumnOrder(3);
        
        builder.Property(e => e.CreateByUserId)
            .HasColumnOrder(4);

        builder.Property(e => e.DeletedByUserId)
            .HasColumnOrder(5);

        builder.Property(e => e.CreatedAt)
            .HasColumnOrder(6);

        builder.Property(e => e.UpdatedAt)
            .HasColumnOrder(7);
        
        builder.Property(e => e.DeletedAt)
            .HasColumnOrder(8);

        #region Relationships

        builder.HasOne(e => e.User)
            .WithMany(e => e.UserCompanies)
            .HasForeignKey(e => e.UserId);

        builder.HasOne(e => e.CreateByUser)
            .WithMany(e => e.UserCreatedCompanies)
            .HasForeignKey(e => e.CreateByUserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.DeletedByUser)
            .WithMany(e => e.UserDeletedCompanies)
            .HasForeignKey(e => e.DeletedByUserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        #endregion
    }
}