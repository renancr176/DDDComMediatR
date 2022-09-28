using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;

namespace ApiMvno.Domain.Interfaces.Repositories
{
    public interface ICompanyAddressesRepository : IRepository<CompanyAddress>
    {
        //Task<IEnumerable<CompanyAddresses>> GetAllByCompanyIdAsync();
    }
}
