using ApiMvno.Application.Commands.CompanyCommands;
using ApiMvno.Application.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Application.Commands;

public static class ComandsIoC
{
    public static void AddCommands(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<CreateCompanyCommand, CompanyModel>, CreateCompanyCommandHandler>();
    }
}