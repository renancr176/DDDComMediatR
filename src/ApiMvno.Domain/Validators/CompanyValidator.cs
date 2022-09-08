using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace ApiMvno.Domain.Validators;

public class CompanyValidator : AbstractValidator<Company>, ICompanyValidator
{
    static string companyNameIsRequiredError = "O nome da empresa é obrigatório.";
    static int companyNameMinLength = 5;
    static string companyNameMinLengthError = $"O nome da empresa deve possuir ao menos {companyNameMinLength} caracteres.";
    static int companyNameMaxLength = 100;
    static string companyNameMaxLengthError = $"O nome da empresa excedeu {companyNameMaxLength} caracteres.";
    private static string companyNameIsUnique = "Já existe uma empresa cadastrada com esse nome.";

    public ValidationResult ValidationResult { get; set; }

    private readonly ICompanyRepository _companyRepository;

    public CompanyValidator(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    
        RuleFor(entity => entity.Name)
            .NotNull()
            .WithErrorCode(nameof(companyNameIsRequiredError))
            .WithMessage(companyNameIsRequiredError)
            .NotEmpty()
            .WithErrorCode(nameof(companyNameIsRequiredError))
            .WithMessage(companyNameIsRequiredError)
            .MinimumLength(companyNameMinLength)
            .WithErrorCode(nameof(companyNameMinLengthError))
            .WithMessage(companyNameMinLengthError)
            .MaximumLength(companyNameMaxLength)
            .WithErrorCode(nameof(companyNameMaxLengthError))
            .WithMessage(companyNameMaxLengthError)
            .MustAsync(NameIsUniqueAsync)
            .WithErrorCode(nameof(companyNameIsUnique))
            .WithMessage(companyNameIsUnique);
    }

    private async Task<bool> NameIsUniqueAsync(Company entity, string name, CancellationToken arg3)
    {
        return !await _companyRepository.AnyAsync(c =>
            c.Id != entity.Id && c.Name.Trim().ToLower() == name.Trim().ToLower());
    }

    public async Task<bool> IsValidAsync(Company entity)
    {
        ValidationResult = await ValidateAsync(entity);
        return ValidationResult.IsValid;
    }
}