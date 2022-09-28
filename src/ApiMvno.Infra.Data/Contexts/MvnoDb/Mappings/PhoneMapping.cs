using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings
{
    public class PhoneMapping : EntityMap<Phone>
    {

        public override void Configure(EntityTypeBuilder<Phone> builder)
        {
            base.Configure(builder);

            builder.ToTable("Phones");

            builder.Property(entity => entity.PhoneTypeId)
                .HasColumnOrder(2);

            builder.Property(entity => entity.Number)
                .HasColumnOrder(3);

            builder.Property(entity => entity.Teste)
                .IsRequired()
                .HasColumnOrder(4);

            builder.Property(entity => entity.CreatedAt)
                .HasColumnOrder(5);

            builder.Property(entity => entity.UpdatedAt)
                .HasColumnOrder(6);

            builder.Property(entity => entity.DeletedAt)
                .HasColumnOrder(7);

            #region Relationships

            builder.HasOne(entity => entity.PhoneType)
                .WithMany(entity => entity.Phones)
                .HasForeignKey(entity => entity.PhoneTypeId);

            #endregion

        }
    }
}