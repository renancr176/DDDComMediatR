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
public class PhoneControllerTests
{
    private readonly IntegrationTestsFixture<StartupTests> _testsFixture;

    public PhoneControllerTests(IntegrationTestsFixture<StartupTests> testsFixture)
    {
        _testsFixture = testsFixture;
    }

    #region Negative Cases

    #endregion

    #region Positive Cases

    [Trait("IntegrationTest", "Controllers")]
    [Fact(DisplayName = "PhoneType get all should at least get a success response code 200/OK")]
    public async Task GetAll_RequestPhoneType_ShuldQuerySuccessfuly()
    {
        // Arrange 

        // Act & Assert
        var responseObj = await _testsFixture.Client
            .GetFromJsonAsync<BaseResponse<IEnumerable<PhoneTypeModel>?>>("/Phone/Types");

        // Assert 
        responseObj.Should().NotBeNull();
        responseObj.Success.Should().BeTrue();
        responseObj.Errors.Should().HaveCount(0);
    }

    [Trait("IntegrationTest", "Controllers")]
    [Fact(DisplayName = "Phone get phone by ID should get a phone successfuly")]
    public async Task GetAll_RequestPhoneById_ShuldGetPhoneSuccessfuly()
    {
        // Arrange 
        var phone = await _testsFixture.MvnoDbContext.Phones.FirstOrDefaultAsync();

        if (phone is null)
        {
            phone = _testsFixture.EntityFixture.PhoneFixture.Valid();
            await _testsFixture.MvnoDbContext.Phones.AddAsync(phone);
            await _testsFixture.MvnoDbContext.SaveChangesAsync();
        }

        // Act & Assert
        var responseObj = await _testsFixture.Client
            .GetFromJsonAsync<BaseResponse<PhoneModel?>>($"/Phone/{phone.Id}");

        // Assert 
        responseObj.Should().NotBeNull();
        responseObj.Success.Should().BeTrue();
        responseObj.Errors.Should().HaveCount(0);
    }

    #endregion
}