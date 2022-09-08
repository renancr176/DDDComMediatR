using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Infra.Data.Contexts.MvnoDb.Repositories;
using ApiMvno.Infra.Data.Contexts.MvnoDb.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb;

public static class MvnoDb
{
    public static void AddMvnoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MvnoDbContext>(dbContextOptions =>
            dbContextOptions.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<MvnoDbContext>();

        #region Repositories

        services.AddScoped<ICompanyRepository, CompanyRepository>();

        #endregion

        #region Seeders

        services.AddScoped<IRoleSeed, RoleSeed>();

        #endregion
    }

    public static void MvnoDbMigrate(this IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetService<MvnoDbContext>();
        dbContext.Database.Migrate();

        #region Seeders

        Task.Run(async () =>
        {
            await serviceProvider.GetService<IRoleSeed>().SeedAsync();
        }).Wait();

        #endregion
    }
}