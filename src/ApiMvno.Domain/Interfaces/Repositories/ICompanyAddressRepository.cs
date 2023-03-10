using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;

namespace ApiMvno.Domain.Interfaces.Repositories
{
    public interface ICompanyAddressRepository : IRepository<CompanyAddress>
    {
        Task<IEnumerable<CompanyAddress>?> GetAllByCompanyIdWithIncludesAsync(Guid id);
    }
}
