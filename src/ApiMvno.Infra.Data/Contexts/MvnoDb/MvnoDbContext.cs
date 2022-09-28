using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Core.Messages;
using ApiMvno.Domain.Entities;
using ApiMvno.Infra.Data.Contexts.MvnoDb.Mappings;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb;

public class MvnoDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>
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

    public DbSet<AccountType> AccountTypes { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<AddressType> AddressTypes { get; set; }
    public DbSet<Cn> Cns { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyAccountType> CompanyAccountTypes { get; set; }
    public DbSet<CompanyAddress> CompanyAddresses { get; set; }
    public DbSet<CompanyCn> CompanyCns { get; set; }
    public DbSet<CompanyContact> CompanyContacts { get; set; }
    public DbSet<CompanyContactPhone> CompanyContactPhones { get; set; }
    public DbSet<CompanyDueDay> CompanyDueDays { get; set; }
    public DbSet<CompanyLineCancelationReason> CompanyLineCancelationReasons { get; set; }
    public DbSet<CompanyLineType> CompanyLineTypes { get; set; }
    public DbSet<CompanyNetworkProfile> CompanyNetworkProfiles { get; set; }
    public DbSet<CompanyNotification> CompanyNotifications { get; set; }
    public DbSet<CompanyPhone> CompanyPhones { get; set; }
    public DbSet<CompanySimCardReplacementReason> CompanySimCardReplacementReasons { get; set; }
    public DbSet<CompanyWebhook> CompanyWebhooks { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<DueDay> DueDays { get; set; }
    public DbSet<EventType> EventTypes { get; set; }
    public DbSet<LineCancelationReason> LineCancelationReasons { get; set; }
    public DbSet<LineType> LineTypes { get; set; }
    public DbSet<Phone> Phones { get; set; }
    public DbSet<PhoneType> PhoneTypes { get; set; }
    public DbSet<SimCardReplacementReason> SimCardReplacementReasons { get; set; }
    public DbSet<Webhook> Webhooks { get; set; }

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

        builder.ApplyConfiguration(new AccountTypeMapping());
        builder.ApplyConfiguration(new AddressMapping());
        builder.ApplyConfiguration(new AddressTypeMapping());
        builder.ApplyConfiguration(new CnMapping());
        builder.ApplyConfiguration(new CompanyAccountTypeMapping());
        builder.ApplyConfiguration(new CompanyAddressMapping());
        builder.ApplyConfiguration(new CompanyCnMapping());
        builder.ApplyConfiguration(new CompanyContactMapping());
        builder.ApplyConfiguration(new CompanyContactPhoneMapping());
        builder.ApplyConfiguration(new CompanyDueDayMapping());
        builder.ApplyConfiguration(new CompanyLineCancelationReasonMapping());
        builder.ApplyConfiguration(new CompanyLineTypeMapping());
        builder.ApplyConfiguration(new CompanyMapping());
        builder.ApplyConfiguration(new CompanyNetworkProfileMapping());
        builder.ApplyConfiguration(new CompanyNotificationMapping());
        builder.ApplyConfiguration(new CompanyPhoneMapping());
        builder.ApplyConfiguration(new CompanySimCardReplacementReasonMapping());
        builder.ApplyConfiguration(new CompanyWebhookMapping());
        builder.ApplyConfiguration(new CountryMapping());
        builder.ApplyConfiguration(new DueDayMapping());
        builder.ApplyConfiguration(new EventTypeMapping());
        builder.ApplyConfiguration(new LineCancelationReasonMapping());
        builder.ApplyConfiguration(new LineTypeMapping());
        builder.ApplyConfiguration(new PhoneMapping());
        builder.ApplyConfiguration(new PhoneTypeMapping());
        builder.ApplyConfiguration(new SimCardReplacementReasonMapping());
        builder.ApplyConfiguration(new WebhookMapping());

        #endregion
    }

    public async Task<bool> Commit()
    {
        var sucesso = await base.SaveChangesAsync() > 0;
        if (sucesso) await _mediatorHandler.PublishEvent(this);

        return sucesso;
    }
}