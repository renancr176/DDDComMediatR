using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace ApiMvno.Domain.Validators
{
    public class CountryValidator : AbstractValidator<Country>, ICountryValidator
    {
        #region Consts

        public const string NameIsRequired = "The name is required.";
        public const string NameMinLength = "The name must have at least 5 characters.";
        public const string NameMaxLength = "The name exceeded 50 characters.";
        public const string NameAlreadyExists = "Already exists another country with same name.";

        public const string PhoneCodeIsRequired = "Telephone code is required.";
        public const string PhoneCodeMinLength = "Telephone code must have at least 1 character.";
        public const string PhoneCodeMaxLength = "Telephone code exceeded 10 characters.";

        #endregion

        public ValidationResult ValidationResult { get; set; }

        private readonly ICountryRepository _countryRepository;

        public CountryValidator(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;

            RuleFor(e => e.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithErrorCode(nameof(NameIsRequired))
                .WithMessage(NameIsRequired)
                .NotEmpty()
                .WithErrorCode(nameof(NameIsRequired))
                .WithMessage(NameIsRequired)
                .MinimumLength(5)
                .WithErrorCode(nameof(NameMinLength))
                .WithMessage(NameMinLength)
                .MaximumLength(50)
                .WithErrorCode(nameof(NameMaxLength))
                .WithMessage(NameMaxLength)
                .MustAsync(NameMustBeUnique)
                .WithErrorCode(nameof(NameAlreadyExists))
                .WithMessage(NameAlreadyExists);

            RuleFor(e => e.PhoneCode)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithErrorCode(nameof(PhoneCodeIsRequired))
                .WithMessage(PhoneCodeIsRequired)
                .NotEmpty()
                .WithErrorCode(nameof(PhoneCodeIsRequired))
                .WithMessage(PhoneCodeIsRequired)
                .MinimumLength(1)
                .WithErrorCode(nameof(PhoneCodeMinLength))
                .WithMessage(PhoneCodeMinLength)
                .MaximumLength(10)
                .WithErrorCode(nameof(PhoneCodeMaxLength))
                .WithMessage(PhoneCodeMaxLength);
        }

        private async Task<bool> NameMustBeUnique(Country entity, string name, CancellationToken arg3)
        {
            return !await _countryRepository.AnyAsync(c =>
                c.Id != entity.Id && c.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public async Task<bool> IsValidAsync(Country entity)
        {
            ValidationResult = await ValidateAsync(entity);
            return ValidationResult.IsValid;
        }
    }
}
