using ApiMvno.Domain.Attributes;
using ApiMvno.Domain.Core.Extensions;
using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Enums;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using ApiMvno.Infra.Data.Contexts.MvnoDb.Seeders.Interfaces;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Seeders;

public class AddressTypeSeed : IAddressTypeSeed
{
    private readonly IAddressTypeRepository _addressTypeRepository;
    private readonly IAddressTypeValidator _addressTypeValidator;

    public AddressTypeSeed(IAddressTypeRepository addressTypeRepository, IAddressTypeValidator addressTypeValidator)
    {
        _addressTypeRepository = addressTypeRepository;
        _addressTypeValidator = addressTypeValidator;
    }

    public async Task SeedAsync()
    {
        foreach (var addressTypeEnum in Enum.GetValues<AddressTypeEnum>())
        {
            var attribute = addressTypeEnum.GetAttributeOfType<NameForDatabaseAttribute>();
            var addressType = new AddressType(
                addressTypeEnum,
                attribute?.Name ?? addressTypeEnum.ToString(),
                true);

            if (await _addressTypeValidator.IsValidAsync(addressType))
            {
                await _addressTypeRepository.InsertAsync(addressType);
                await _addressTypeRepository.SaveChangesAsync();
            }
        }
    }
}