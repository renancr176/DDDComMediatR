using ApiMvno.Application.Commands.CompanyCommands;
using ApiMvno.Application.Commands.CountryCommands;
using ApiMvno.Application.Commands.UserCommands;
using ApiMvno.Application.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Application.Commands;

public static class CommandsIoC
{
    public static void AddCommands(this IServiceCollection services)
    {
        #region User

        services.AddScoped<IRequestHandler<SignInCommand, SignInResponseModel>, SignInCommandHandler>();
        services.AddScoped<IRequestHandler<SignUpCommand, UserModel>, SignUpCommandHandler>();
        services.AddScoped<IRequestHandler<PasswordResetCommand, MessageModel>, PasswordResetCommandHandler>();
        services.AddScoped<IRequestHandler<ResetPasswordCommand, MessageModel>, ResetPasswordCommandHandler>();
        services.AddScoped<IRequestHandler<UserChangeStatusCommand, bool>, UserChangeStatusCommandHandler>();
        services.AddScoped<IRequestHandler<ConfirmEmailCommand, bool>, ConfirmEmailCommandHandler>();
        services.AddScoped<IRequestHandler<UserAddRoleCommand, bool>, UserAddRoleCommandHandler>();
        services.AddScoped<IRequestHandler<UserAddCompanyCommand, bool>, UserAddCompanyCommandHandler>();
        services.AddScoped<IRequestHandler<UserDeleteCompanyCommand, bool>, UserDeleteCompanyCommandHandler>();

        #endregion

        #region Types and pre existing data

        services.AddScoped<IRequestHandler<CreateCountryCommand, CountryModel?>, CreateCountryCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateCountryCommand, CountryModel?>, UpdateCountryCommandHandler>();

        #endregion

        #region Company

        services.AddScoped<IRequestHandler<CreateCompanyCommand, CompanyModel?>, CreateCompanyCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateCompanyCommand, CompanyModel?>, UpdateCompanyCommandHandler>();

        services.AddScoped<IRequestHandler<CreateCompanyAddressCommand, CompanyAddressModel?>, CreateCompanyAddressCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateCompanyAddressCommand, CompanyAddressModel?>, UpdateCompanyAddressCommandHandler>();

        services.AddScoped<IRequestHandler<CreateCompanyPhoneCommand, CompanyPhoneModel?>, CreateCompanyPhoneCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateCompanyPhoneCommand, CompanyPhoneModel?>, UpdateCompanyPhoneCommandHandler>();

        #endregion
    }
}