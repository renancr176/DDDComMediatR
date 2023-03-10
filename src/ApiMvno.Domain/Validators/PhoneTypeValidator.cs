using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Enums;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace ApiMvno.Domain.Validators
{
    public class PhoneTypeValidator : AbstractValidator<PhoneType>, IPhoneTypeValidator
    {
        #region Consts

        public const string TypeIsInvalid = "The type is invalid.";
        public const string TypeAlreadyExists = "Already exists another phone type with same type.";

        public const string NameIsRequired = "The name is required.";
        public const string NameMinLength = "The name must have at least 1 character.";
        public const string NameMaxLength = "The name exceeded 50 characters.";

        #endregion

        public ValidationResult ValidationResult { get; set; }

        private readonly IPhoneTypeRepository _phoneTypeRepository;

        public PhoneTypeValidator(IPhoneTypeRepository phoneTypeRepository)
        {
            _phoneTypeRepository = phoneTypeRepository;

            RuleFor(e => e.Type)
                .Cascade(CascadeMode.Stop)
                .IsInEnum()
                .WithErrorCode(nameof(TypeIsInvalid))
                .WithMessage(TypeIsInvalid)
                .MustAsync(TypeMustBeUnique)
                .WithErrorCode(nameof(TypeAlreadyExists))
                .WithMessage(TypeAlreadyExists);

            RuleFor(e => e.Name)
                .NotNull()
                .WithErrorCode(nameof(NameIsRequired))
                .WithMessage(NameIsRequired)
                .NotEmpty()
                .WithErrorCode(nameof(NameIsRequired))
                .WithMessage(NameIsRequired)
                .MinimumLength(1)
                .WithErrorCode(nameof(NameMinLength))
                .WithMessage(NameMinLength)
                .MaximumLength(50)
                .WithErrorCode(nameof(NameMaxLength))
                .WithMessage(NameMaxLength);
        }

        private async Task<bool> TypeMustBeUnique(PhoneType entity, PhoneTypeEnum type, CancellationToken arg3)
        {
            return !await _phoneTypeRepository.AnyAsync(pt => pt.Id != entity.Id && pt.Type == type);
        }

        public async Task<bool> IsValidAsync(PhoneType entity)
        {
            ValidationResult = await ValidateAsync(entity);
            return ValidationResult.IsValid;
        }
    }
}
