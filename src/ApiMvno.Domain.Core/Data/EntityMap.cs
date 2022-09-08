using ApiMvno.Domain.Core.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMvno.Domain.Core.Data;

public abstract class EntityMap<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.CreatedAt)
            .IsRequired();

        builder.Ignore(entity => entity.Notifications);
    }
}

public abstract class EntityIntIdMap<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityIntId
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Id)
            .UseIdentityColumn();

        builder.Property(entity => entity.CreatedAt)
            .IsRequired();
    }
}