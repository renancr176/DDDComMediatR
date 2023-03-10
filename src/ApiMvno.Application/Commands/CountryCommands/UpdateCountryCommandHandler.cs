using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ApiMvno.Application.Commands.CountryCommands;

public class UpdateCountryCommandHandler
    : IRequestHandler<UpdateCountryCommand, CountryModel?>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICountryRepository _countryRepository;
    private readonly ICountryValidator _countryValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateCountryCommandHandler(IMediator mediator, IMapper mapper, ICountryRepository countryRepository,
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
    public const string CountryNotExists = "This country doesn't exists.";

    #endregion

    public async Task<CountryModel?> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var country = await _countryRepository.GetByIdAsync(request.Id);

            if (country is null)
            {
                await _mediator.Publish(new DomainNotification(nameof(CountryNotExists), CountryNotExists));
                return default;
            }

            country = _mapper.Map(request, country);

            if (!await _countryValidator.IsValidAsync(country))
            {
                foreach (var error in _countryValidator.ValidationResult.Errors)
                {
                    await _mediator.Publish(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return default;
            }

            await _countryRepository.UpdateAsync(country);

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