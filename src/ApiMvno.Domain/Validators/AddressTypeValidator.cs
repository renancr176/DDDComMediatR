using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Enums;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace ApiMvno.Domain.Validators
{
    public class AddressTypeValidator : AbstractValidator<AddressType>, IAddressTypeValidator
    {
        #region Consts
        
        public const string TypeInvalid = "The type is invalid.";
        public const string TypeAlreadyExists = "Already exists another address type with same type.";

        public const string NameIsRequired = "The name must be provided.";
        public const string NameMinLength = "The name must have at least 5 characters.";
        public const string NameMaxLength = "The name exceded 100 characters.";

        #endregion

        public ValidationResult ValidationResult { get; set; }

        private readonly IAddressTypeRepository _addressTypeRepository;

        public AddressTypeValidator(IAddressTypeRepository addressTypeRepository)
        {
            _addressTypeRepository = addressTypeRepository;

            RuleFor(e => e.Type)
                .IsInEnum()
                .WithErrorCode(nameof(TypeInvalid))
                .WithMessage(TypeInvalid)
                .MustAsync(TypeMustBeUnique)
                .WithErrorCode(nameof(TypeAlreadyExists))
                .WithMessage(TypeAlreadyExists);

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
                .MaximumLength(100)
                .WithErrorCode(nameof(NameMaxLength))
                .WithMessage(NameMaxLength);
        }

        private async Task<bool> TypeMustBeUnique(AddressType entity, AddressTypeEnum type, CancellationToken arg3)
        {
            return !await _addressTypeRepository.AnyAsync(at => at.Id != entity.Id && at.Type == type);
        }

        public async Task<bool> IsValidAsync(AddressType entity)
        {
            ValidationResult = await ValidateAsync(entity);
            return ValidationResult.IsValid;
        }
    }
}
