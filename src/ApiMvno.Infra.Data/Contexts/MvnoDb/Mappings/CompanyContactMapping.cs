using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings
{
    public class CompanyContactMapping : EntityMap<CompanyContact>
    {

        public override void Configure(EntityTypeBuilder<CompanyContact> builder)
        {
            base.Configure(builder);

            builder.ToTable("CompanyContacts");

            builder.Property(entity => entity.CompanyId)
                .HasColumnOrder(2);

            builder.Property(entity => entity.Name)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnOrder(3);

            builder.Property(entity => entity.Email)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnOrder(4);

            builder.Property(entity => entity.Position)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnOrder(5);

            builder.Property(entity => entity.NotifyChanges)
                .HasColumnOrder(6);

            builder.Property(entity => entity.CreatedAt)
                .HasColumnOrder(7);

            builder.Property(entity => entity.UpdatedAt)
                .HasColumnOrder(8);

            builder.Property(entity => entity.DeletedAt)
                .HasColumnOrder(9);

            #region Relationships

            builder.HasOne(entity => entity.Company)
                .WithMany(entity => entity.CompanyContacts)
                .HasForeignKey(entity => entity.CompanyId);

            #endregion
        }

    }
}
