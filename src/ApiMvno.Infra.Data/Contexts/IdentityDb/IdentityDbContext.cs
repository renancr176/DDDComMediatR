using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Core.Messages;
using ApiMvno.Domain.Entities;
using ApiMvno.Infra.Data.Contexts.IdentityDb.Mappings;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ApiMvno.Infra.Data.Contexts.IdentityDb;

public class IdentityDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>
    , IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    , IUnitOfWork
{
    private readonly IMediator _mediatorHandler;

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options, IMediator mediatorHandler)
        : base(options)
    {
        _mediatorHandler = mediatorHandler;
    }

    #region DbSets

    public DbSet<UserCompany> UserCompanies { get; set; }

    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("IdentityConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Ignore<Event>();

        #region Mappings

        builder.ApplyConfiguration(new UserCompanyMapping());

        #endregion
    }

    public async Task<bool> Commit()
    {
        var sucesso = await base.SaveChangesAsync() > 0;
        if (sucesso) await _mediatorHandler.PublishEvent(this);

        return sucesso;
    }
}