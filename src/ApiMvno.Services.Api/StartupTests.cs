using System.Reflection;
using System.Text.Json.Serialization;
using ApiMvno.Infra.CrossCutting.IoC;
using ApiMvno.Infra.Data.Contexts.IdentityDb;
using ApiMvno.Infra.Data.Contexts.MvnoDb;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiMvno.Services.Api;

public class StartupTests
{
    public StartupTests()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile($"appsettings.Testing.json", true, true)
            .AddEnvironmentVariables();

        Configuration = builder.Build();
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); ;
        services.AddEndpointsApiExplorer();
        services.AddMediatR(typeof(StartupTests).GetTypeInfo().Assembly);
        services.AddAutoMapper(typeof(StartupTests));
        services.AddHttpContextAccessor();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.RegisterServices(Configuration);

        Init(services.BuildServiceProvider());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    private void Init(IServiceProvider serviceProvider)
    {
        var scriptClearDataBase = @"USE #DbName;
        DECLARE @Sql NVARCHAR(500) DECLARE @Cursor CURSOR

        SET @Cursor = CURSOR FAST_FORWARD FOR
        SELECT DISTINCT sql = 'ALTER TABLE [' + tc2.TABLE_SCHEMA + '].[' + tc2.TABLE_NAME + '] DROP [' + rc1.CONSTRAINT_NAME + '];'
        FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc1
        LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc2 ON tc2.CONSTRAINT_NAME = rc1.CONSTRAINT_NAME

        OPEN @Cursor FETCH NEXT FROM @Cursor INTO @Sql

        WHILE(@@FETCH_STATUS = 0)
        BEGIN
            Exec sp_executesql @Sql
        FETCH NEXT FROM @Cursor INTO @Sql
        END

        CLOSE @Cursor DEALLOCATE @Cursor

        EXEC sp_MSforeachtable 'DROP TABLE ?'";

        #region IdentityDbContext

        var identityDbName = Configuration.GetSection("ConnectionStrings:IdentityConnection").Value
            .Split(";")
            .FirstOrDefault(i => i.ToLower().Contains("database=".ToLower()))
            .Split("=")
            .LastOrDefault();

        var identityDbContext = serviceProvider.GetService<IdentityDbContext>();

        //Delete all tables before run migrations
        identityDbContext.Database.ExecuteSqlRaw(scriptClearDataBase.Replace("#DbName", identityDbName));

        serviceProvider.IdentityDbMigrate();

        #endregion

        #region MvnoDbContext

        var dbName = Configuration.GetSection("ConnectionStrings:DefaultConnection").Value
            .Split(";")
            .FirstOrDefault(i => i.ToLower().Contains("database=".ToLower()))
            .Split("=")
            .LastOrDefault();
         
        var mvnoDbContext = serviceProvider.GetService<MvnoDbContext>();

        //Delete all tables before run migrations
        mvnoDbContext.Database.ExecuteSqlRaw(scriptClearDataBase.Replace("#DbName", dbName));

        serviceProvider.MvnoDbMigrate();

        #endregion
    }
}