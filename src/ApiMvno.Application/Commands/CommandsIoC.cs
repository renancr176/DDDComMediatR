using ApiMvno.Application.Commands.CompanyCommands;
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

        services.AddScoped<IRequestHandler<SingInCommand, SingInResponseModel>, SingInCommandHandler>();
        services.AddScoped<IRequestHandler<SignUpCommand, UserModel>, SignUpCommandHandler>();
        services.AddScoped<IRequestHandler<PasswordResetCommand, MessageModel>, PasswordResetCommandHandler>();
        services.AddScoped<IRequestHandler<ResetPasswordCommand, MessageModel>, ResetPasswordCommandHandler>();
        services.AddScoped<IRequestHandler<UserChangeStatusCommand, bool>, UserChangeStatusCommandHandler>();
        services.AddScoped<IRequestHandler<ConfirmEmailCommand, bool>, ConfirmEmailCommandHandler>();
        services.AddScoped<IRequestHandler<UserAddRoleCommand, bool>, UserAddRoleCommandHandler>();

        #endregion

        #region Company

        services.AddScoped<IRequestHandler<CreateCompanyCommand, CompanyModel>, CreateCompanyCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateCompanyCommand, CompanyModel>, UpdateCompanyCommandHandler>();

        #endregion
    }
}