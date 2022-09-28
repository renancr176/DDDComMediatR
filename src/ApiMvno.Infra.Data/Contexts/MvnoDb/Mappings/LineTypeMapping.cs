using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings
{
    public class LineTypeMapping : EntityMap<LineType>
    {

        public override void Configure(EntityTypeBuilder<LineType> builder)
        {
            base.Configure(builder);

            builder.ToTable("LineTypes");

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

            builder.Property(entity => entity.NumasyId)
                .IsRequired()
                .HasColumnOrder(5);

            builder.Property(entity => entity.CreatedAt)
                .HasColumnOrder(6);

            builder.Property(entity => entity.UpdatedAt)
                .HasColumnOrder(7);

            builder.Property(entity => entity.DeletedAt)
                .HasColumnOrder(8);

            #region Relationships

            builder.HasMany(entity => entity.CompanyLineTypes)
                .WithOne(entity => entity.LineType)
                .HasForeignKey(entity => entity.LineTypeId);

            #endregion
        }

    }
}
