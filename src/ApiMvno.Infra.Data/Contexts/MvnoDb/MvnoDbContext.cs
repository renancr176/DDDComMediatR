using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;
using ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb;

public class MvnoDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>
    , IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    , IUnitOfWork
{
    private readonly IMediator _mediatorHandler;

    public MvnoDbContext(DbContextOptions<MvnoDbContext> options, IMediator mediatorHandler)
        : base(options)
    {
        _mediatorHandler = mediatorHandler;
    }

    #region DbSets

    public DbSet<Company> Companies { get; set; }

    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        #region Mappings

        builder.ApplyConfiguration(new CompanyMapping());

        #endregion
    }

    public async Task<bool> Commit()
    {
        var sucesso = await base.SaveChangesAsync() > 0;
        if (sucesso) await _mediatorHandler.PublishEvent(this);

        return sucesso;
    }
}