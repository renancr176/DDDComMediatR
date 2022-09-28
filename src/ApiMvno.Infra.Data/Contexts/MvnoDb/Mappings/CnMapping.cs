using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings
{
    public class CnMapping : EntityMap<Cn>
    {

        public override void Configure(EntityTypeBuilder<Cn> builder)
        {
            base.Configure(builder);

            builder.ToTable("Cns");

            builder.Property(entity => entity.Code)
                .HasColumnOrder(2);

            builder.Property(entity => entity.Uf)
                .IsRequired()
                .HasMaxLength(2)
                .HasColumnOrder(3);

            builder.Property(entity => entity.Active)
                .HasColumnOrder(4);

            builder.Property(entity => entity.CreatedAt)
                .HasColumnOrder(5);

            builder.Property(entity => entity.UpdatedAt)
                .HasColumnOrder(6);

            builder.Property(entity => entity.DeletedAt)
                .HasColumnOrder(7);
        }

    }
}
