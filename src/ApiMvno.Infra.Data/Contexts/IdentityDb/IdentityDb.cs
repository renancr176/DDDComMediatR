using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Infra.Data.Contexts.IdentityDb.Repositories;
using ApiMvno.Infra.Data.Contexts.IdentityDb.Seeders;
using ApiMvno.Infra.Data.Contexts.IdentityDb.Seeders.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Infra.Data.Contexts.IdentityDb;

public static class IdentityDb
{
    public static void AddIdentityDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(dbContextOptions =>
            dbContextOptions.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

        #region Repositories

        services.AddScoped<IUserCompanyRepository, UserCompanyRepository>();

        #endregion

        #region Seeders

        services.AddScoped<IRoleSeed, RoleSeed>();

        #endregion
    }

    public static void IdentityDbMigrate(this IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetService<IdentityDbContext>();
        dbContext.Database.Migrate();

        #region Seeders

        Task.Run(async () =>
        {
            await serviceProvider.GetService<IRoleSeed>().SeedAsync();
        }).Wait();

        #endregion
    }
}