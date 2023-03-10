using ApiMvno.Domain.Core.Extensions;
using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace ApiMvno.Domain.Validators;

public class CompanyValidator : AbstractValidator<Company>, ICompanyValidator
{
    #region Const

    public const string CompanyNameMinLength = "The name must have at least 5 characters.";
    public const string CompanyNameMaxLength = "The company name exceded 100 characters.";
    public const string CompanyNameIsRequired = "The company name must be provided";
    public const string CompanyNameIsUnique = "There is already a company registered with that name.";

    public const string CompanyDocumentIsRequired = "The company document must be provided";
    public const string CompanyDocumentIsUnique = "There is already a company registered with that document.";
    public const string CompanyDocumentMaxLength = "The company document exceded 50 characters.";

    public const string CompanyEmailIsRequired = "The company email must be provided";
    public const string CompanyEmailMaxLength = "The company email exceded 50 characters.";

    public const string CompanyAddressIsRequired = "The company must have at least 1 address.";

    public const string CompanyPhoneIsRequired = "The company must have at least 1 phone number.";
    
    #endregion

    public ValidationResult ValidationResult { get; set; }

    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyAddressValidator _companyAddressValidator;
    private readonly ICompanyPhoneValidator _phoneValidator;

    public CompanyValidator(ICompanyRepository companyRepository, ICompanyAddressValidator companyAddressValidator,
        ICompanyPhoneValidator phoneValidator)
    {
        _companyRepository = companyRepository;
        _companyAddressValidator = companyAddressValidator;
        _phoneValidator = phoneValidator;

        #region Set children validators

        RuleForEach(e => e.CompanyAddresses)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithErrorCode(nameof(CompanyAddressIsRequired))
            .WithMessage(CompanyAddressIsRequired)
            .SetValidator(_companyAddressValidator);

        RuleForEach(e => e.CompanyPhones)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithErrorCode(nameof(CompanyPhoneIsRequired))
            .WithMessage(CompanyPhoneIsRequired)
            .SetValidator(_phoneValidator);

        #endregion

        RuleFor(entity => entity.Name)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithErrorCode(nameof(CompanyNameIsRequired))
            .WithMessage(CompanyNameIsRequired)
            .NotEmpty()
            .WithErrorCode(nameof(CompanyNameIsRequired))
            .WithMessage(CompanyNameIsRequired)
            .MinimumLength(5)
            .WithErrorCode(nameof(CompanyNameMinLength))
            .WithMessage(CompanyNameMinLength)
            .MaximumLength(100)
            .WithErrorCode(nameof(CompanyNameMaxLength))
            .WithMessage(CompanyNameMaxLength)
            .MustAsync(NameIsUniqueAsync)
            .WithErrorCode(nameof(CompanyNameIsUnique))
            .WithMessage(CompanyNameIsUnique);

        RuleFor(e => e.Document)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithErrorCode(nameof(CompanyDocumentIsRequired))
            .WithMessage(CompanyDocumentIsRequired)
            .NotEmpty()
            .WithErrorCode(nameof(CompanyDocumentIsRequired))
            .WithMessage(CompanyDocumentIsRequired)
            .MaximumLength(50)
            .WithErrorCode(nameof(CompanyDocumentMaxLength))
            .WithMessage(CompanyDocumentMaxLength)
            .MustAsync(DocumentIsUniqueAsync)
            .WithErrorCode(nameof(CompanyDocumentIsUnique))
            .WithMessage(CompanyDocumentIsUnique);


        RuleFor(e => e.Email)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithErrorCode(nameof(CompanyEmailIsRequired))
            .WithMessage(CompanyEmailIsRequired)
            .NotEmpty()
            .WithErrorCode(nameof(CompanyEmailIsRequired))
            .WithMessage(CompanyEmailIsRequired)
            .MaximumLength(50)
            .WithErrorCode(nameof(CompanyEmailMaxLength))
            .WithMessage(CompanyEmailMaxLength);
    }

    private async Task<bool> NameIsUniqueAsync(Company entity, string name, CancellationToken arg3)
    {
        return !await _companyRepository.AnyAsync(c =>
            c.Id != entity.Id && c.Name.Trim().ToLower() == name.Trim().ToLower());
    }

    private async Task<bool> DocumentIsUniqueAsync(Company entity, string document, CancellationToken arg3)
    {
        return !await _companyRepository.AnyAsync(c =>
            c.Id != entity.Id && c.Document.RemoveNonAlphanumeric() == document.RemoveNonAlphanumeric());
    }

    public async Task<bool> IsValidAsync(Company entity)
    {
        ValidationResult = await ValidateAsync(entity);
        return ValidationResult.IsValid;
    }
}