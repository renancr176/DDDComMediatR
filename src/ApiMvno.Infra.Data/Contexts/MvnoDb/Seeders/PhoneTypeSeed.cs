using ApiMvno.Domain.Attributes;
using ApiMvno.Domain.Core.Extensions;
using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Enums;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using ApiMvno.Infra.Data.Contexts.MvnoDb.Seeders.Interfaces;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Seeders;

public class PhoneTypeSeed : IPhoneTypeSeed
{
    private readonly IPhoneTypeRepository _phoneTypeRepository;
    private readonly IPhoneTypeValidator _phoneTypeValidator;

    public PhoneTypeSeed(IPhoneTypeRepository phoneTypeRepository, IPhoneTypeValidator phoneTypeValidator)
    {
        _phoneTypeRepository = phoneTypeRepository;
        _phoneTypeValidator = phoneTypeValidator;
    }

    public async Task SeedAsync()
    {
        foreach (var phoneTypeEnum in Enum.GetValues<PhoneTypeEnum>())
        {
            var attribute = phoneTypeEnum.GetAttributeOfType<NameForDatabaseAttribute>();
            var phoneType = new PhoneType(
                phoneTypeEnum,
                attribute?.Name ?? phoneTypeEnum.ToString(), 
                true);

            if (await _phoneTypeValidator.IsValidAsync(phoneType))
            {
                await _phoneTypeRepository.InsertAsync(phoneType);
                await _phoneTypeRepository.SaveChangesAsync();
            }
        }
    }
}