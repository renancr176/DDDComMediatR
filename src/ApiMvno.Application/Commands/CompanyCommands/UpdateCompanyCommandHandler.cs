using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ApiMvno.Application.Commands.CompanyCommands;

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CompanyModel?>
{
    #region Consts

    public const string InternalServerError = "An internal server error has occurred, please try again later.";
    public const string CompanyNotExists = "Company not exists.";

    #endregion

    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyValidator _companyValidator;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateCompanyCommandHandler(IMediator mediator, IMapper mapper, ICompanyRepository companyRepository,
        ICompanyValidator companyValidator, IWebHostEnvironment webHostEnvironment,
        IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _mapper = mapper;
        _companyRepository = companyRepository;
        _companyValidator = companyValidator;
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CompanyModel?> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var company = await _companyRepository.GetByIdAsync(request.Id);

            if (company is null)
            {
                await _mediator.Publish(new DomainNotification(nameof(CompanyNotExists), CompanyNotExists));
                return default;
            }

            company = _mapper.Map(request, company);

            if (!await _companyValidator.IsValidAsync(company))
            {
                foreach (var error in _companyValidator.ValidationResult.Errors)
                {
                    await _mediator.Publish(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return default;
            }

            await _companyRepository.UpdateAsync(company);

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