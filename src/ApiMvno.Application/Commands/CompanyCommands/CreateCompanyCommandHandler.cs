using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ApiMvno.Application.Commands.CompanyCommands;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyModel?>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyValidator _companyValidator;

    public CreateCompanyCommandHandler(IHttpContextAccessor httpContextAccessor, IMediator mediator,
        IMapper mapper, ICompanyRepository companyRepository, ICompanyValidator companyValidator)
    {
        _httpContextAccessor = httpContextAccessor;
        _mediator = mediator;
        _mapper = mapper;
        _companyRepository = companyRepository;
        _companyValidator = companyValidator;
    }

    #region Consts

    public const string InternalServerError = "An internal server error has occurred, please try again later.";

    #endregion

    public async Task<CompanyModel?> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var company = _mapper.Map<Company>(request);

            if (!await _companyValidator.IsValidAsync(company))
            {
                foreach (var error in _companyValidator.ValidationResult.Errors)
                {
                    await _mediator.Publish(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return default;
            }

            await _companyRepository.InsertAsync(company);

            if (request.AggregateId == Guid.Empty)
                await _companyRepository.UnitOfWork.Commit();

            return _mapper.Map<CompanyModel>(company);
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return default;
    }
}