using System;
using System.Net.Http;
using System.Threading.Tasks;
using ApiMvno.Infra.Data.Contexts.MvnoDb;
using ApiMvno.Services.Api;
using ApiMvno.Test.Fixtures;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace ApiMvno.Test.IntegrationTests.Config;

[CollectionDefinition(nameof(IntegrationTestsFixtureCollection))]
public class IntegrationTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupTests>> { }

public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
{
    public readonly StartupFactory<TStartup> Factory;
    public HttpClient Client;
    public EntityFixture EntityFixture;
    public IServiceProvider Services;
    public MvnoDbContext MvnoDbContext;

    public string AdminUserName { get; set; }
    public string AdminPassword { get; set; }
    public string AdminAccessToken { get; set; }

    public string UserName { get; set; }
    public string UserPassword { get; set; }
    public string UserAccessToken { get; set; }

    public IntegrationTestsFixture()
    {
        Factory = new StartupFactory<TStartup>();
        Client = Factory.CreateClient();
        EntityFixture = new EntityFixture();

        AdminUserName = "usertest@telecall.com.br";
        AdminPassword = "g}}P9=#%2L~R,fH?=_<]76Dc#96@Em65";

        Services = Factory.Server.Services;
        MvnoDbContext = (MvnoDbContext)Services.GetService(typeof(MvnoDbContext));

        if (MvnoDbContext == null)
        {
            throw new ArgumentNullException(nameof(MvnoDbContext), "Database connection can't be null");
        }

        Task.Run(async () =>
        {
            var userManager = (UserManager<IdentityUser<Guid>>)Services.GetService(typeof(UserManager<IdentityUser<Guid>>));

            var user = await userManager.FindByNameAsync(AdminUserName);
            if (user == null)
            {
                await userManager.CreateAsync(new IdentityUser<Guid>(AdminUserName), AdminPassword);

                user = await userManager.FindByNameAsync(AdminUserName);
                user.EmailConfirmed = true;
                await userManager.UpdateAsync(user);
            }
            
            await MvnoDbContext.SaveChangesAsync();
        }).Wait();
    }

    public void GenerateUserAndPassword()
    {
        var faker = new Faker("pt_BR");
        UserName = faker.Internet.Email().ToLower();
        UserPassword = faker.Internet.Password(8, false, "", "Ab@1_");
    }

    //public async Task AuthenticateAsAdminAsync()
    //{
    //    AdminAccessToken = await AuthenticateAsync(AdminUserName, AdminPassword);
    //}

    //public async Task AuthenticateAsUserAsync()
    //{
    //    UserAccessToken = await AuthenticateAsync(UserName, UserPassword);
    //}

    //public async Task<string> AuthenticateAsync(string userName, string password)
    //{
    //    var request = new SignInRequest
    //    {
    //        UserName = userName,
    //        Password = password
    //    };

    //    // Recriando o client para evitar configurações de outro startup.
    //    Client = Factory.CreateClient();

    //    var response = await Client.AddJsonMediaType()
    //        .PostAsJsonAsync("/Auth/SignIn", request);
    //    response.EnsureSuccessStatusCode();
    //    var responseObj =
    //        JsonConvert.DeserializeObject<BaseResponse<SignInResponse>>(await response.Content.ReadAsStringAsync());
    //    if (string.IsNullOrEmpty(responseObj?.Data.AccessToken))
    //        throw new ArgumentNullException("AccessToken", "Unable to retrieve authentication token.");

    //    return responseObj?.Data.AccessToken;
    //}

    public void Dispose()
    {
        Client.Dispose();
        Factory.Dispose();
    }
}