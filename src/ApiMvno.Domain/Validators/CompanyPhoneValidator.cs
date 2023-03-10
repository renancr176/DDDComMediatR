using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace ApiMvno.Domain.Validators
{
    public class CompanyPhoneValidator : AbstractValidator<CompanyPhone>, ICompanyPhoneValidator
    {
        #region Const

        public const string CompanyPhoneIsUnique = "There is already a phone registered with that company.";

        #endregion

        public ValidationResult ValidationResult { get; set; }

        private readonly ICompanyPhoneRepository _companyPhoneRepository;
        private readonly IPhoneValidator _phoneValidator;

        public CompanyPhoneValidator(ICompanyPhoneRepository companyPhoneRepository, IPhoneValidator phoneValidator)
        {
            _companyPhoneRepository = companyPhoneRepository;
            _phoneValidator = phoneValidator;

            #region Set children validators

            RuleFor(e => e.Phone)
                .SetValidator(_phoneValidator);

            #endregion

            RuleFor(e => e)
                .MustAsync(Predicate)
                .WithErrorCode(nameof(CompanyPhoneIsUnique))
                .WithMessage(CompanyPhoneIsUnique);
        }

        private async Task<bool> Predicate(CompanyPhone entity, CancellationToken token)
        {
            return !await _companyPhoneRepository.AnyAsync(p =>
                p.Id != entity.Id && p.CompanyId == entity.CompanyId && p.PhoneId == entity.PhoneId);
        }

        public async Task<bool> IsValidAsync(CompanyPhone entity)
        {
            ValidationResult = await ValidateAsync(entity);
            return ValidationResult.IsValid;
        }
    }
}
