using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Extensions;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Core.Options;
using ApiMvno.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace ApiMvno.Application.Commands.UserCommands;

public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommand, MessageModel>
{
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager;
    private readonly IDistributedCache _memoryCache;
    private readonly IOptions<GeneralOptions> _generalOptions;

    public GeneralOptions GeneralOptions => _generalOptions.Value;

    public PasswordResetCommandHandler(IMediator mediator, UserManager<User> userManager, IDistributedCache memoryCache,
        IOptions<GeneralOptions> generalOptions)
    {
        _mediator = mediator;
        _userManager = userManager;
        _memoryCache = memoryCache;
        _generalOptions = generalOptions;
    }

    #region Consts

    public const string InternalServerError = "An internal server error has occurred, please try again later.";
    public const string UserNotFound = "User not found.";
    public const string UnableSendMail = "Unable to send email.";

    public const string EmailPasswordResetSubject = "Alteração de senha";
    public const string EmailPasswordResetBody =
        @"<p>Olá #Name</p><br/><p>Para alterar a senha, por favor <a href=""#Url/auth/resetpassword/#Token"">Clique aqui<a/>.</p>";

    public const string PasswordResetEmailSent = "Check your email for further instructions.";
    #endregion

    public async Task<MessageModel> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user is null)
            {
                await _mediator.Publish(new DomainNotification(nameof(UserNotFound), UserNotFound));
                return default!;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            await _memoryCache.SetObjectAsync(token,
                user,
                new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                },
                cancellationToken);

            var body = EmailPasswordResetBody
                .Replace("#Name", user.Name)
                .Replace("#Url", GeneralOptions.FrontUrl)
                .Replace("#Token", token.Base64Encode());

            //if (!await _mailService.SendAsync(new SendMailResquest(user.Email, EmailPasswordResetSubject, body)))
            //{
            //    await _mediator.Publish(new DomainNotification(nameof(UnableSendMail), UnableSendMail));
            //    return default!;
            //}

            return new MessageModel()
            {
                Message = PasswordResetEmailSent
            };
        }
        catch (Exception)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return default!;
    }
}