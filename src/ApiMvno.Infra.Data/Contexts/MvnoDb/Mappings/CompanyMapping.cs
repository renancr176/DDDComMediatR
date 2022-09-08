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

        builder.Property(entity => entity.Name)
            .HasMaxLength(100);

        #region Relationships

        #endregion
    }
}