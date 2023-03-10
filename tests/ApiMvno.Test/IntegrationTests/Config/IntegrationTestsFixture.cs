using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ApiMvno.Application.Commands.UserCommands;
using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Enums;
using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Enums;
using ApiMvno.Domain.Interfaces.Validators;
using ApiMvno.Infra.Data.Contexts.MvnoDb;
using ApiMvno.Services.Api;
using ApiMvno.Services.Api.Models.Responses;
using ApiMvno.Test.Extensions;
using ApiMvno.Test.Fixtures;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
    public string? AdminAccessToken { get; set; }

    public string UserName { get; set; }
    public string UserPassword { get; set; }
    public string? UserAccessToken { get; set; }

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
            var userManager = (UserManager<User>)Services.GetService(typeof(UserManager<User>));

            var user = await userManager.FindByNameAsync(AdminUserName);
            if (user == null)
            {
                await userManager.CreateAsync(new User(AdminUserName, "Admin", UserStatusEnum.Active), AdminPassword);

                user = await userManager.FindByNameAsync(AdminUserName);
                user.EmailConfirmed = true;
                await userManager.UpdateAsync(user);

                await userManager.AddToRoleAsync(user, RoleEnum.Admin.ToString());
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

    public async Task AuthenticateAsAdminAsync()
    {
        AdminAccessToken = await AuthenticateAsync(AdminUserName, AdminPassword);
    }

    public async Task AuthenticateAsUserAsync()
    {
        UserAccessToken = await AuthenticateAsync(UserName, UserPassword);
    }

    public async Task<string> AuthenticateAsync(string userName, string password)
    {
        var request = new SignInCommand
        {
            UserName = userName,
            Password = password
        };

        // Recriando o client para evitar configurações de outro startup.
        Client = Factory.CreateClient();

        var response = await Client.AddJsonMediaType()
            .PostAsJsonAsync("/User/SignIn", request);
        response.EnsureSuccessStatusCode();
        var responseObj =
            JsonConvert.DeserializeObject<BaseResponse<SignInResponseModel>>(await response.Content.ReadAsStringAsync());
        if (string.IsNullOrEmpty(responseObj?.Data.AccessToken))
            throw new ArgumentNullException("AccessToken", "Unable to retrieve authentication token.");

        return responseObj?.Data.AccessToken;
    }

    #region Company
    public async Task<Company> GetNewValidCompanyAsync()
    {
        var companyValidator = (ICompanyValidator)Services.GetService(typeof(ICompanyValidator));

        var country = await MvnoDbContext.Countries.FirstOrDefaultAsync();
        var addressTypes = await MvnoDbContext.AddressTypes.ToListAsync();
        var phoneTypes = await MvnoDbContext.PhoneTypes.ToListAsync();

        Company company;
        var retries = 3;
        do
        {
            company = EntityFixture.CompanyFixture.Valid();

            foreach (var companyAddress in company.CompanyAddresses)
            {
                companyAddress.Address.CountryId = country.Id;
                companyAddress.Address.AddressTypeId = EntityFixture.Faker.PickRandom(addressTypes).Id;
            }

            foreach (var companyCompanyPhone in company.CompanyPhones)
            {
                companyCompanyPhone.Phone.PhoneTypeId = companyCompanyPhone.Phone.Number.ToString().Length == 13
                    ? phoneTypes.FirstOrDefault(pt => pt.Type == PhoneTypeEnum.Mobile).Id
                    : phoneTypes.FirstOrDefault(pt => pt.Type == PhoneTypeEnum.LandLine).Id;
                companyCompanyPhone.Phone.Number =
                    long.Parse($"{country.PhoneCode}{companyCompanyPhone.Phone.Number.ToString().Substring(2)}");
            }

            retries--;
        } while (retries > 0 && !await companyValidator.IsValidAsync(company));

        if (company == null)
        {
            throw new Exception("Couldn't create a valid fake company to be inserted on database.");
        }

        return company;
    }

    public async Task<Company> GetInsertedNewCompanyAsync()
    {
        var company = await GetNewValidCompanyAsync();

        await MvnoDbContext.Companies.AddAsync(company);
        await MvnoDbContext.SaveChangesAsync();

        return company;
    }

    #endregion

    public void Dispose()
    {
        Client.Dispose();
        Factory.Dispose();
    }
}