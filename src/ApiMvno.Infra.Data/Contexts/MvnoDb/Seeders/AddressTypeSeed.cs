using ApiMvno.Domain.Enums;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Seeders;

public class AddressTypeSeed : IAddressTypeSeed
{
    //private readonly IAddressTypeRepository _addressTypeRepository;

    //public AddressTypeSeed(IAddressTypeRepository addressTypeRepository)
    //{
    //    _addressTypeRepository = addressTypeRepository;
    //}

    public async Task SeedAsync()
    {
        foreach (var addressTypeEnum in Enum.GetValues<AddressTypeEnum>())
        {
            //if (!await _addressTypeRepository.AnyAsync(at => at.Type == addressTypeEnum))
            //{
            //    await _addressTypeRepository.InsertAsync(new AddressType()
            //    {
            //        Type = addressTypeEnum,
            //        Name = addressTypeEnum.ToString()
            //    });

            //    await _addressTypeRepository.SaveChangesAsync();
            //}
        }
    }
}