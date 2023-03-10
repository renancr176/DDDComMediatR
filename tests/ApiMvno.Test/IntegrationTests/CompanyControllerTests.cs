using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ApiMvno.Application.Commands.AddressCommands;
using ApiMvno.Application.Commands.CompanyCommands;
using ApiMvno.Application.Commands.PhoneCommands;
using ApiMvno.Application.Models;
using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Validators;
using ApiMvno.Domain.Validators;
using ApiMvno.Services.Api;
using ApiMvno.Services.Api.Models.Responses;
using ApiMvno.Test.Extensions;
using ApiMvno.Test.IntegrationTests.Config;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApiMvno.Test.IntegrationTests;

[Collection(nameof(IntegrationTestsFixtureCollection))]
public class CompanyControllerTests
{
    private readonly IntegrationTestsFixture<StartupTests> _testsFixture;

    public CompanyControllerTests(IntegrationTestsFixture<StartupTests> testsFixture)
    {
        _testsFixture = testsFixture;
    }

    #region Privates

    private async Task<CreateCompanyCommand> ValidCreateCompanyCommandAsync()
    {

        var company = await _testsFixture.GetNewValidCompanyAsync();

        return new CreateCompanyCommand(
            company.Name,
            company.Email,
            company.Document,
            company.CompanyAddresses.Select(ca =>
                new CreateAddressCommand(
                    ca.Address.AddressTypeId,
                    ca.Address.ZipCode,
                    ca.Address.State,
                    ca.Address.City,
                    ca.Address.Neighborhood,
                    ca.Address.StreetName,
                    ca.Address.StreetNumber,
                    ca.Address.Details,
                    ca.Address.CountryId)
            ),
            company.CompanyPhones.Select(cp =>
                new CreatePhoneCommand(
                    cp.Phone.PhoneTypeId,
                    cp.Phone.Number)
            ));
    }

    private async Task<CreateCompanyCommand> InvalidCreateCompanyCommandAsync()
    {
        var companyValidator = (ICompanyValidator)_testsFixture.Services.GetService(typeof(ICompanyValidator));

        Company company;
        do
        {
            company = _testsFixture.EntityFixture.CompanyFixture.Invalid();
        } while (await companyValidator.IsValidAsync(company));

        return new CreateCompanyCommand(
            company.Name,
            company.Email,
            company.Document,
            company.CompanyAddresses.Select(ca =>
                new CreateAddressCommand(
                    ca.Address.AddressTypeId,
                    ca.Address.ZipCode,
                    ca.Address.State,
                    ca.Address.City,
                    ca.Address.Neighborhood,
                    ca.Address.StreetName,
                    ca.Address.StreetNumber,
                    ca.Address.Details,
                    ca.Address.CountryId)
            ),
            company.CompanyPhones.Select(cp =>
                new CreatePhoneCommand(
                    cp.Phone.PhoneTypeId,
                    cp.Phone.Number)
            ));
    }

    #endregion

    #region Negative Cases


    [Trait("IntegrationTest", "Controllers")]
    [Fact(DisplayName = "Given invalid company should get error response")]
    public async Task Create_GivenInvalidCompany_ShouldGetError()
    {
        // Arrange 
        var request = await InvalidCreateCompanyCommandAsync();

        if (string.IsNullOrEmpty(_testsFixture.AdminAccessToken))
        {
            await _testsFixture.AuthenticateAsAdminAsync();
        }

        // Act 
         var response = await _testsFixture.Client
            .AddToken(_testsFixture.AdminAccessToken)
            .PostAsJsonAsync("/Company", request);

        // Assert 

        response.StatusCode.Should().HaveFlag(HttpStatusCode.BadRequest);
        var responseObj = await response.DeserializeObject<BaseResponse>();
        responseObj.Success.Should().BeFalse();
        responseObj.Errors.Should().HaveCountGreaterThan(0);
        responseObj.Errors
            .Any(e => e.ErrorCode == nameof(CompanyValidator.CompanyNameMinLength))
            .Should()
            .BeTrue();
    }
    
    [Trait("IntegrationTest", "Controllers")]
    [Fact(DisplayName = "Given an not existing company should update get error response")]
    public async Task Update_GivenNotExistingCompany_ShouldGetError()
    {
        // Arrange 
        var company = await _testsFixture.MvnoDbContext.Companies.FirstOrDefaultAsync();

        if (company is null)
        {
            company = await _testsFixture.GetInsertedNewCompanyAsync();
        }

        var request = new UpdateCompanyCommand(
            company.Id, 
            "");

        if (string.IsNullOrEmpty(_testsFixture.AdminAccessToken))
        {
            await _testsFixture.AuthenticateAsAdminAsync();
        }

        // Act 
        var response = await _testsFixture.Client
            .AddToken(_testsFixture.AdminAccessToken)
            .PutAsJsonAsync("/Company", request);

        // Assert 
        response.StatusCode.Should().HaveFlag(HttpStatusCode.BadRequest);
        var responseObj = await response.DeserializeObject<BaseResponse>();
        responseObj.Success.Should().BeFalse();
        responseObj.Errors.Should().HaveCountGreaterThan(0);
        responseObj.Errors
            .Any(e => e.ErrorCode == nameof(CompanyValidator.CompanyEmailIsRequired))
            .Should()
            .BeTrue();
    }

    [Trait("IntegrationTest", "Controllers")]
    [Fact(DisplayName = "Given existing company address should update get error response")]
    public async Task Update_GivenExistingCompanyAddress_ShouldGetError()
    {
        // Arrange 
        var companyAddress = await _testsFixture.MvnoDbContext.CompanyAddresses.FirstOrDefaultAsync();

        if (companyAddress is null)
        {
            await _testsFixture.GetInsertedNewCompanyAsync();
            companyAddress = await _testsFixture.MvnoDbContext.CompanyAddresses.FirstOrDefaultAsync();
        }

        var request = new UpdateCompanyAddressCommand(
            companyAddress.Id,
            new UpdateAddressCommand(
                Guid.Empty, 
                Guid.Empty, 
                0,
                "",
                "",
                "",
                "",
                "",
                0,
                "")
        );

        if (string.IsNullOrEmpty(_testsFixture.AdminAccessToken))
        {
            await _testsFixture.AuthenticateAsAdminAsync();
        }

        // Act
        var response = await _testsFixture.Client
            .AddToken(_testsFixture.AdminAccessToken)
            .PutAsJsonAsync("/Company/Address", request);

        // Assert 
        response.StatusCode.Should().HaveFlag(HttpStatusCode.BadRequest);
        var responseObj = await response.DeserializeObject<BaseResponse>();
        responseObj.Success.Should().BeFalse();
        responseObj.Errors.Should().HaveCountGreaterThan(0);
        responseObj.Errors
            .Any(e => e.ErrorCode == nameof(AddressValidator.CountryNotExists))
            .Should()
            .BeTrue();
    }

    [Trait("IntegrationTest", "Controllers")]
    [Fact(DisplayName = "Given existing company phone should update get error response")]
    public async Task Update_GivenAnExistingCompanyPhone_ShouldGetError()
    {
        // Arrange 
        var companyPhone = await _testsFixture.MvnoDbContext.CompanyPhones.FirstOrDefaultAsync();

        if (companyPhone is null)
        {
            await _testsFixture.GetInsertedNewCompanyAsync();
            companyPhone = await _testsFixture.MvnoDbContext.CompanyPhones.FirstOrDefaultAsync();
        }

        var request = new UpdateCompanyPhoneCommand(
            companyPhone.Id,
            companyPhone.Active,
            new UpdatePhoneCommand(
                Guid.Empty,
                0,
                0)
        );

        if (string.IsNullOrEmpty(_testsFixture.AdminAccessToken))
        {
            await _testsFixture.AuthenticateAsAdminAsync();
        }

        // Act
        var response = await _testsFixture.Client
            .AddToken(_testsFixture.AdminAccessToken)
            .PutAsJsonAsync("/Company/Phone", request);

        // Assert 
        response.StatusCode.Should().HaveFlag(HttpStatusCode.BadRequest);
        var responseObj = await response.DeserializeObject<BaseResponse>();
        responseObj.Success.Should().BeFalse();
        responseObj.Errors.Should().HaveCountGreaterThan(0);
        responseObj.Errors
            .Any(e => e.ErrorCode == nameof(PhoneValidator.PhoneTypeNotExists))
            .Should()
            .BeTrue();
    }

    

    #endregion

    #region Positive Cases

    #region Company

    [Trait("IntegrationTest", "Controllers")]
    [Fact(DisplayName = "Company given valid company should create a new company successfuly")]
    public async Task Create_GivenValidCompany_ShouldCreateSuccessfuly()
    {
        // Arrange 
        var request = await ValidCreateCompanyCommandAsync();

        if (string.IsNullOrEmpty(_testsFixture.AdminAccessToken))
        {
            await _testsFixture.AuthenticateAsAdminAsync();
        }

        // Act 
        var response = await _testsFixture.Client
            .AddToken(_testsFixture.AdminAccessToken)
            .PostAsJsonAsync("/Company", request);

        // Assert 
        response.EnsureSuccessStatusCode();
        var responseObj = await response.DeserializeObject<BaseResponse<CompanyModel>>();
        responseObj.Success.Should().BeTrue();
        responseObj.Errors.Should().HaveCount(0);
    }

    [Trait("IntegrationTest", "Controllers")]
    [Fact(DisplayName = "Given existing company should update a company successfuly")]
    public async Task Update_GivenExistingCompany_ShouldUpdateSuccessfuly()
    {
        // Arrange 
        var company = await _testsFixture.MvnoDbContext.Companies.FirstOrDefaultAsync();

        if (company is null)
        {
            company = await _testsFixture.GetInsertedNewCompanyAsync();
        }

        var request = new UpdateCompanyCommand(
            company.Id,
            _testsFixture.EntityFixture.Faker.Internet.Email());

        if (string.IsNullOrEmpty(_testsFixture.AdminAccessToken))
        {
            await _testsFixture.AuthenticateAsAdminAsync();
        }

        // Act 
        var response = await _testsFixture.Client
            .AddToken(_testsFixture.AdminAccessToken)
            .PutAsJsonAsync("/Company", request);

        // Assert 
        response.EnsureSuccessStatusCode();
        var responseObj = await response.DeserializeObject<BaseResponse<CompanyModel>>();
        responseObj.Success.Should().BeTrue();
        responseObj.Errors.Should().HaveCount(0);
    }

    #endregion

    #region Address

    [Trait("IntegrationTest", "Controllers")]
    [Fact(DisplayName = "Given existing company address should update a company successfuly")]
    public async Task Update_GivenAnExistingCompanyAddress_ShouldUpdateSuccessfuly()
    {
        // Arrange 
        var companyAddress = await _testsFixture.MvnoDbContext.CompanyAddresses
            .Include(nameof(CompanyAddress.Address))
            .FirstOrDefaultAsync();

        if (companyAddress is null)
        {
            var company = await _testsFixture.GetInsertedNewCompanyAsync();
            companyAddress = company.CompanyAddresses.FirstOrDefault();
        }

        var request = new UpdateCompanyAddressCommand(
            companyAddress.Id,
            new UpdateAddressCommand(
                companyAddress.Address.Id,
                companyAddress.Address.CountryId,
                companyAddress.Address.AddressTypeId,
                companyAddress.Address.ZipCode,
                companyAddress.Address.State,
                companyAddress.Address.City,
                companyAddress.Address.Neighborhood,
                _testsFixture.EntityFixture.Faker.Address.StreetName(),
                companyAddress.Address.StreetNumber,
                companyAddress.Address.Details)
            );

        if (string.IsNullOrEmpty(_testsFixture.AdminAccessToken))
        {
            await _testsFixture.AuthenticateAsAdminAsync();
        }

        // Act
        var response = await _testsFixture.Client
            .AddToken(_testsFixture.AdminAccessToken)
            .PutAsJsonAsync("/Company/Address", request);

        // Assert 
        response.EnsureSuccessStatusCode();
        var responseObj = await response.DeserializeObject<BaseResponse<CompanyAddressModel>>();
        responseObj.Success.Should().BeTrue();
        responseObj.Errors.Should().HaveCount(0);
    }

    [Trait("IntegrationTest", "Controllers")]
    [Fact(DisplayName = "CompanyAddress get address by Company ID should get a company address successfuly")]
    public async Task GetAllCompanyAddress_RequestAddressByCompanyId_ShouldGetCompanyAddressSuccessfuly()
    {
        // Arrange 
        var company = await _testsFixture.MvnoDbContext.Companies.FirstOrDefaultAsync() ?? await _testsFixture.GetInsertedNewCompanyAsync();

        if (string.IsNullOrEmpty(_testsFixture.AdminAccessToken))
        {
            await _testsFixture.AuthenticateAsAdminAsync();
        }

        // Act & Assert
        var responseObj = await _testsFixture.Client
            .AddToken(_testsFixture.AdminAccessToken)
            .GetFromJsonAsync<BaseResponse<IEnumerable<CompanyAddressModel>?>>($"/Company/{company.Id}/Addresses");

        // Assert 
        responseObj.Should().NotBeNull();
        responseObj.Success.Should().BeTrue();
        responseObj.Errors.Should().HaveCount(0);
    }

    #endregion

    #region Phone

    [Trait("IntegrationTest", "Controllers")]
    [Fact(DisplayName = "Given existing company phone should update a company successfuly")]
    public async Task Update_GivenAnExistingCompanyPhone_ShouldUpdateSuccessfuly()
    {
        // Arrange 
        var companyPhone = await _testsFixture.MvnoDbContext.CompanyPhones
            .Include(nameof(CompanyPhone.Phone))
            .FirstOrDefaultAsync();

        if (companyPhone is null)
        {
            var company = await _testsFixture.GetInsertedNewCompanyAsync();
            companyPhone = company.CompanyPhones.FirstOrDefault();
        }

        var request = new UpdateCompanyPhoneCommand(
            companyPhone.Id,
            companyPhone.Active,
            new UpdatePhoneCommand(
                companyPhone.Phone.Id,
                companyPhone.Phone.PhoneTypeId,
                companyPhone.Phone.Number)
        );

        if (string.IsNullOrEmpty(_testsFixture.AdminAccessToken))
        {
            await _testsFixture.AuthenticateAsAdminAsync();
        }

        // Act
        var response = await _testsFixture.Client
            .AddToken(_testsFixture.AdminAccessToken)
            .PutAsJsonAsync("/Company/Phone", request);

        // Assert 
        response.EnsureSuccessStatusCode();
        var responseObj = await response.DeserializeObject<BaseResponse<CompanyAddressModel>>();
        responseObj.Success.Should().BeTrue();
        responseObj.Errors.Should().HaveCount(0);
    }

    [Trait("IntegrationTest", "Controllers")]
    [Fact(DisplayName = "CompanyPhone get phone by Company ID should get a company phone successfuly")]
    public async Task GetAllCompanyPhones_RequestPhoneByCompanyId_ShouldGetCompanyPhoneSuccessfuly()
    {
        // Arrange 
        var company = await _testsFixture.MvnoDbContext.Companies.FirstOrDefaultAsync() ?? await _testsFixture.GetInsertedNewCompanyAsync();

        if (string.IsNullOrEmpty(_testsFixture.AdminAccessToken))
        {
            await _testsFixture.AuthenticateAsAdminAsync();
        }

        // Act & Assert
        var responseObj = await _testsFixture.Client
            .AddToken(_testsFixture.AdminAccessToken)
            .GetFromJsonAsync<BaseResponse<IEnumerable<CompanyPhoneModel>?>>($"/Company/{company.Id}/Phones");

        // Assert 
        responseObj.Should().NotBeNull();
        responseObj.Success.Should().BeTrue();
        responseObj.Errors.Should().HaveCount(0);
    }

    #endregion

    #region Queries

    #endregion

    #endregion
}