using System.Net.Http.Json;
using System.Threading.Tasks;
using ApiMvno.Application.Commands.AddressCommands;
using ApiMvno.Application.Commands.CompanyCommands;
using ApiMvno.Application.Models;
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

    #region Negative Cases



    #endregion

    #region Positive Cases
    
    [Trait("IntegrationTest", "Controllers")]
    [Fact(DisplayName = "Given valid company should create a new company successfuly")]
    public async Task Create_GivenValidCompany_ShouldCreateSuccessfuly()
    {
        // Arrange 
        CreateCompanyCommand request = null;
        do
        {
            var company = _testsFixture.EntityFixture.CompanyFixture.Valid();
            var address = new CreateAddressCommand();

            request = new CreateCompanyCommand();
            
        } while (await _testsFixture.MvnoDbContext.Companies.AnyAsync(c =>
                     c.Name.Trim().ToLower() == request.Name.Trim().ToLower()));
        
        // Act 
        var response = await _testsFixture.Client
            .PostAsJsonAsync("/Company", request);

        // Assert 
        response.EnsureSuccessStatusCode();
        var responseObj = await response.DeserializeObject<BaseResponse<CompanyModel>>();
        responseObj.Success.Should().BeTrue();
        responseObj.Errors.Should().HaveCount(0);
    }

    #endregion
}