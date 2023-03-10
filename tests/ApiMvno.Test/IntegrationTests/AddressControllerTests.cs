using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ApiMvno.Application.Models;
using ApiMvno.Services.Api;
using ApiMvno.Services.Api.Models.Responses;
using ApiMvno.Test.IntegrationTests.Config;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApiMvno.Test.IntegrationTests;

[Collection(nameof(IntegrationTestsFixtureCollection))]
public class AddressControllerTests
{
    private readonly IntegrationTestsFixture<StartupTests> _testsFixture;

    public AddressControllerTests(IntegrationTestsFixture<StartupTests> testsFixture)
    {
        _testsFixture = testsFixture;
    }

    #region Negative Cases

    #endregion

    #region Positive Cases

    [Trait("IntegrationTest", "Controllers")]
    [Fact(DisplayName = "Address get address by ID should get a address successfuly")]
    public async Task GetAll_RequestAddressById_ShuldGetAddressSuccessfuly()
    {
        // Arrange 
        var address = await _testsFixture.MvnoDbContext.Addresses.FirstOrDefaultAsync();

        if (address is null)
        {
            await _testsFixture.GetInsertedNewCompanyAsync();
            address = await _testsFixture.MvnoDbContext.Addresses.FirstOrDefaultAsync();
        }

        // Act & Assert
        var responseObj = await _testsFixture.Client
            .GetFromJsonAsync<BaseResponse<AddressModel?>>($"/Address/{address.Id}");

        // Assert 
        responseObj.Should().NotBeNull();
        responseObj.Success.Should().BeTrue();
        responseObj.Errors.Should().HaveCount(0);
    }

    [Trait("IntegrationTest", "Controllers")]
    [Fact(DisplayName = "AddressType get all should at least get a success response code 200/OK")]
    public async Task GetAll_RequestAddressType_ShuldQuerySuccessfuly()
    {
        // Arrange 

        // Act & Assert
        var responseObj = await _testsFixture.Client
            .GetFromJsonAsync<BaseResponse<IEnumerable<AddressTypeModel>?>>("/Address/Types");

        // Assert 
        responseObj.Should().NotBeNull();
        responseObj.Success.Should().BeTrue();
        responseObj.Errors.Should().HaveCount(0);
    }
    #endregion
}