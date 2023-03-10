using ApiMvno.Application.Models;

namespace ApiMvno.Application.Queries.Interfaces;

public interface IAddressQuery
{
    Task<AddressModel?> GetByIdAsync(Guid id);
    Task<IEnumerable<AddressTypeModel>?> GetAllAddressTypesAsync();
}