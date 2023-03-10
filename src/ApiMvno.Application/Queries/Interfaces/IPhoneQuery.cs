using ApiMvno.Application.Models;

namespace ApiMvno.Application.Queries.Interfaces;

public interface IPhoneQuery
{
    Task<IEnumerable<PhoneTypeModel>?> GetAllPhoneTypesAsync();
    Task<PhoneModel?> GetByIdAsync(Guid id);
}