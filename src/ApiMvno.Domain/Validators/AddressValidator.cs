using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace ApiMvno.Domain.Validators
{
    public class AddressValidator : AbstractValidator<Address>, IAddressValidator
    {
        #region Consts

        public const string CountryIsRequired = "The country must be provided.";
        public const string CountryNotExists = "The country provided doesn't exists or is not active.";
        
        public const string AddressTypeIsRequired = "The address type must be provided.";
        public const string AddressTypeNotExists = "The address type provided doesn't exists or is not active.";

        public const string ZipCodeIsRequired = "The zipcode must be provided.";
        public const string ZipCodeMinLength = "The zipcode must have at least 8 characters.";
        public const string ZipCodeMaxLength = "The zipcode exceded 9 characters.";

        public const string StateIsRequired = "The state must be provided.";
        public const string StateMinLength = "The state must have at least 2 characters.";
        public const string StateMaxLength = "The state exceded 100 characters.";

        public const string CityIsRequired = "The city must be provided.";
        public const string CityMinLength = "The city must have at least 5 characters.";
        public const string CityMaxLength = "The city exceded 50 characters.";

        public const string NeighborhoodIsRequired = "The neighborhood must be provided.";
        public const string NeighborhoodMinLength = "The neighborhood must have at least 5 characters.";
        public const string NeighborhoodMaxLength = "The neighborhood exceded 50 characters.";

        public const string StreetNameIsRequired = "The street name must be provided.";
        public const string StreetNameMinLength = "The street name must have at least 5 characters.";
        public const string StreetNameMaxLength = "The street name exceded 50 characters.";

        public const string StreetNumberGreaterThanZero = "The street number must be greater than zero.";

        public const string DetailsMaxLength = "The deatails exceded 255 characters.";

        #endregion

        public ValidationResult ValidationResult { get; set; }

        private readonly IAddressRepository _addressRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IAddressTypeRepository _addressTypeRepository;

        public AddressValidator(IAddressRepository addressRepository, ICountryRepository countryRepository,
            IAddressTypeRepository addressTypeRepository)
        {
            _addressRepository = addressRepository;
            _countryRepository = countryRepository;
            _addressTypeRepository = addressTypeRepository;

            RuleFor(e => e.CountryId)
                .NotNull()
                .WithErrorCode(nameof(CountryIsRequired))
                .WithMessage(CountryIsRequired)
                .MustAsync(CountryMustExists)
                .WithErrorCode(nameof(CountryNotExists))
                .WithMessage(CountryNotExists);

            RuleFor(e => e.AddressTypeId)
                .GreaterThan(0)
                .WithErrorCode(nameof(AddressTypeIsRequired))
                .WithMessage(AddressTypeIsRequired)
                .MustAsync(AddressTypeMustExists)
                .WithErrorCode(nameof(AddressTypeNotExists))
                .WithMessage(AddressTypeNotExists);

            RuleFor(e => e.ZipCode)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithErrorCode(nameof(ZipCodeIsRequired))
                .WithMessage(ZipCodeIsRequired)
                .NotEmpty()
                .WithErrorCode(nameof(ZipCodeIsRequired))
                .WithMessage(ZipCodeIsRequired)
                .MinimumLength(8)
                .WithErrorCode(nameof(ZipCodeMinLength))
                .WithMessage(ZipCodeMinLength)
                .MaximumLength(9)
                .WithErrorCode(nameof(ZipCodeMaxLength))
                .WithMessage(ZipCodeMaxLength);

            RuleFor(e => e.State)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithErrorCode(nameof(StateIsRequired))
                .WithMessage(StateIsRequired)
                .NotEmpty()
                .WithErrorCode(nameof(StateIsRequired))
                .WithMessage(StateIsRequired)
                .MinimumLength(2)
                .WithErrorCode(nameof(StateMinLength))
                .WithMessage(StateMinLength)
                .MaximumLength(50)
                .WithErrorCode(nameof(StateMaxLength))
                .WithMessage(StateMaxLength);

            RuleFor(e => e.City)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithErrorCode(nameof(CityIsRequired))
                .WithMessage(CityIsRequired)
                .NotEmpty()
                .WithErrorCode(nameof(CityIsRequired))
                .WithMessage(CityIsRequired)
                .MinimumLength(5)
                .WithErrorCode(nameof(CityMinLength))
                .WithMessage(CityMinLength)
                .MaximumLength(50)
                .WithErrorCode(nameof(CityMaxLength))
                .WithMessage(CityMaxLength);

            RuleFor(e => e.Neighborhood)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithErrorCode(nameof(NeighborhoodIsRequired))
                .WithMessage(NeighborhoodIsRequired)
                .NotEmpty()
                .WithErrorCode(nameof(NeighborhoodIsRequired))
                .WithMessage(NeighborhoodIsRequired)
                .MinimumLength(5)
                .WithErrorCode(nameof(NeighborhoodMinLength))
                .WithMessage(NeighborhoodMinLength)
                .MaximumLength(50)
                .WithErrorCode(nameof(NeighborhoodMaxLength))
                .WithMessage(NeighborhoodMaxLength);

            RuleFor(e => e.StreetName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithErrorCode(nameof(StreetNameIsRequired))
                .WithMessage(StreetNameIsRequired)
                .NotEmpty()
                .WithErrorCode(nameof(StreetNameIsRequired))
                .WithMessage(StreetNameIsRequired)
                .MinimumLength(5)
                .WithErrorCode(nameof(StreetNameMinLength))
                .WithMessage(StreetNameMinLength)
                .MaximumLength(50)
                .WithErrorCode(nameof(StreetNameMaxLength))
                .WithMessage(StreetNameMaxLength);

            RuleFor(e => e.StreetNumber)
                .GreaterThan(0)
                .WithErrorCode(nameof(StreetNumberGreaterThanZero))
                .WithMessage(StreetNumberGreaterThanZero);

            RuleFor(e => e.Details)
                .MaximumLength(255)
                .WithErrorCode(nameof(DetailsMaxLength))
                .WithMessage(DetailsMaxLength);
        }

        private async Task<bool> CountryMustExists(Address entity, Guid countryId, CancellationToken arg3)
        {
            return await _countryRepository.AnyAsync(c => c.Id == countryId && c.Active);
        }

        private async Task<bool> AddressTypeMustExists(Address entity, long addressTypeId, CancellationToken arg3)
        {
            return await _addressTypeRepository.AnyAsync(c => c.Id == addressTypeId && c.Active);
        }

        public async Task<bool> IsValidAsync(Address entity)
        {
            ValidationResult = await ValidateAsync(entity);
            return ValidationResult.IsValid;
        }
    }
}
