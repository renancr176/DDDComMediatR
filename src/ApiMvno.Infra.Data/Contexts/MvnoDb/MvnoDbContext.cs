using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Core.Messages;
using ApiMvno.Domain.Entities;
using ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb;

public class MvnoDbContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediatorHandler;

    public MvnoDbContext(DbContextOptions<MvnoDbContext> options, IMediator mediatorHandler)
        : base(options)
    {
        _mediatorHandler = mediatorHandler;
    }

    #region DbSets

    public DbSet<Address> Addresses { get; set; }
    public DbSet<AddressType> AddressTypes { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyAddress> CompanyAddresses { get; set; }
    public DbSet<CompanyPhone> CompanyPhones { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Phone> Phones { get; set; }
    public DbSet<PhoneType> PhoneTypes { get; set; }

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

        builder.Ignore<Event>();

        #region Mappings

        builder.ApplyConfiguration(new AddressMapping());
        builder.ApplyConfiguration(new AddressTypeMapping());
        builder.ApplyConfiguration(new CompanyAddressMapping());
        builder.ApplyConfiguration(new CompanyMapping());
        builder.ApplyConfiguration(new CompanyPhoneMapping());
        builder.ApplyConfiguration(new CountryMapping());
        builder.ApplyConfiguration(new PhoneMapping());
        builder.ApplyConfiguration(new PhoneTypeMapping());

        #endregion
    }

    public async Task<bool> Commit()
    {
        var sucesso = await base.SaveChangesAsync() > 0;
        if (sucesso) await _mediatorHandler.PublishEvent(this);

        return sucesso;
    }
}