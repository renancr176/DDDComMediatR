using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace ApiMvno.Domain.Validators
{
    public class CompanyAddressValidator : AbstractValidator<CompanyAddress>, ICompanyAddressValidator
    {
        #region Consts

        public const string AddressAlreadyExists = "The address provided already exists.";

        #endregion

        public ValidationResult ValidationResult { get; set; }

        private readonly ICompanyAddressRepository _companyAddressRepository;
        private readonly IAddressValidator _addressValidator;

        public CompanyAddressValidator(ICompanyAddressRepository companyAddressRepository, IAddressValidator addressValidator)
        {
            _companyAddressRepository = companyAddressRepository;
            _addressValidator = addressValidator;

            #region Set children validators

            RuleFor(e => e.Address)
                .SetValidator(_addressValidator);

            #endregion

            RuleFor(e => e)
                .MustAsync(AddressMustBeUniqueAsync)
                .WithErrorCode(nameof(AddressAlreadyExists))
                .WithMessage(AddressAlreadyExists);
        }

        private async Task<bool> AddressMustBeUniqueAsync(CompanyAddress entity, CancellationToken arg3)
        {
            return !await _companyAddressRepository.AnyAsync(at =>
                at.Id != entity.Id && at.CompanyId == entity.CompanyId && at.AddressId == entity.AddressId);
        }

        public async Task<bool> IsValidAsync(CompanyAddress entity)
        {
            ValidationResult = await ValidateAsync(entity);
            return ValidationResult.IsValid;
        }
    }
}
