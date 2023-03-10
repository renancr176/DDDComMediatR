using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ApiMvno.Application.Commands.CountryCommands;

public class CreateCountryCommandHandler
    : IRequestHandler<CreateCountryCommand, CountryModel?>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICountryRepository _countryRepository;
    private readonly ICountryValidator _countryValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateCountryCommandHandler(IMediator mediator, IMapper mapper, ICountryRepository countryRepository,
        ICountryValidator countryValidator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _mapper = mapper;
        _countryRepository = countryRepository;
        _countryValidator = countryValidator;
        _httpContextAccessor = httpContextAccessor;
    }

    #region Consts

    public const string InternalServerError = "An internal server error has occurred, please try again later.";

    #endregion

    public async Task<CountryModel?> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var country = _mapper.Map<Country>(request);

            if (!await _countryValidator.IsValidAsync(country))
            {
                foreach (var error in _countryValidator.ValidationResult.Errors)
                {
                    await _mediator.Publish(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return default;
            }

            await _countryRepository.InsertAsync(country);

            if (request.AggregateId == Guid.Empty)
                await _countryRepository.UnitOfWork.Commit();

            return _mapper.Map<CountryModel>(country);
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }
        return default;
    }
}