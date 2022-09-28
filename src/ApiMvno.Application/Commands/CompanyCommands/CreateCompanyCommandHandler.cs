using ApiMvno.Application.Events;
using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using AutoMapper;
using MediatR;

namespace ApiMvno.Application.Commands.CompanyCommands;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyModel>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyValidator _companyValidator;
    
    public CreateCompanyCommandHandler(IMediator mediator, IMapper mapper, ICompanyRepository companyRepository,
        ICompanyValidator companyValidator)
    {
        _mediator = mediator;
        _mapper = mapper;
        _companyRepository = companyRepository;
        _companyValidator = companyValidator;
    }

    public async Task<CompanyModel> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
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

                return default!;
            }
            

            company.AddEvent(new CompanyCreatedEvent(company));

            await _companyRepository.InsertAsync(company);
            await _companyRepository.UnitOfWork.Commit();
            
            
            return _mapper.Map<CompanyModel>(company);
        }
        catch (Exception)
        {
            await _mediator.Publish(new DomainNotification("InternalServerError", "Ouve um erro interno, tenta novamente mais tarde."));
        }

        return default!;
    }
}