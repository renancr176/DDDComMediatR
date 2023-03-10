using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace ApiMvno.Domain.Validators
{
    public class PhoneValidator : AbstractValidator<Phone>, IPhoneValidator
    {
        #region Consts
        
        public const string PhoneTypeIsRequired = "The phone type is required.";
        public const string PhoneTypeNotExists = "The phone type doesn't exists.";

        public const string NumberIsInvalid = "The phone number is invalid.";

        #endregion

        public ValidationResult ValidationResult { get; set; }

        private readonly IPhoneRepository _phoneRepository;
        private readonly IPhoneTypeRepository _phoneTypeRepository;


        public PhoneValidator(IPhoneRepository phoneRepository, IPhoneTypeRepository phoneTypeRepository)
        {
            _phoneRepository = phoneRepository;
            _phoneTypeRepository = phoneTypeRepository;

            RuleFor(e => e.PhoneTypeId)
                .GreaterThan(0)
                .WithErrorCode(nameof(PhoneTypeIsRequired))
                .WithMessage(PhoneTypeIsRequired)
                .MustAsync(PhoneTypeMustExists)
                .WithErrorCode(nameof(PhoneTypeNotExists))
                .WithMessage(PhoneTypeNotExists);

            RuleFor(e => e.Number)
                .Must(ValidPhoneNumber)
                .WithErrorCode(nameof(NumberIsInvalid))
                .WithMessage(NumberIsInvalid);
        }

        private async Task<bool> PhoneTypeMustExists(Phone entity, long phoneTypeId, CancellationToken arg3)
        {
            return await _phoneTypeRepository.AnyAsync(pt => pt.Id == phoneTypeId && pt.Active);
        }

        private bool ValidPhoneNumber(long number)
        {
            return number.ToString().Length >= 10;
        }

        public async Task<bool> IsValidAsync(Phone entity)
        {
            ValidationResult = await ValidateAsync(entity);
            return ValidationResult.IsValid;
        }
    }
}
