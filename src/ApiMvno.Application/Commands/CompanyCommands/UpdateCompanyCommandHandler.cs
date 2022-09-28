using ApiMvno.Application.Events;
using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using AutoMapper;
using MediatR;

namespace ApiMvno.Application.Commands.CompanyCommands;

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CompanyModel>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyValidator _companyValidator;
    private readonly IAddressesRepository _addressesRepository;
    private readonly ICompanyAddressesRepository _companyAddressesRepository;

    public UpdateCompanyCommandHandler(
            IMediator mediator,
            IMapper mapper,
            ICompanyRepository companyRepository,
            ICompanyValidator companyValidator,
            IAddressesRepository addressesReposiroty,
            ICompanyAddressesRepository companyAddressesRepository
        )
    {
        _mediator = mediator;
        _mapper = mapper;
        _companyRepository = companyRepository;
        _companyValidator = companyValidator;
        _addressesRepository = addressesReposiroty;
        _companyAddressesRepository = companyAddressesRepository;
    }

    public async Task<CompanyModel> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var company = await _companyRepository.GetByIdAsync(request.Id);

            company = _mapper.Map(request, company);

            if (!await _companyValidator.IsValidAsync(company))
            {
                foreach (var error in _companyValidator.ValidationResult.Errors)
                {
                    await _mediator.Publish(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return default!;
            }

            await _companyRepository.UpdateAsync(company);

            await _companyRepository.UnitOfWork.Commit();

            return _mapper.Map<CompanyModel>(company);
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification("InternalServerError", "Ouve um erro interno, tenta novamente mais tarde."));
        }

        return default;
    }
}