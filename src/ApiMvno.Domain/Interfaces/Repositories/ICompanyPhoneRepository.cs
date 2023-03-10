using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Entities;

namespace ApiMvno.Domain.Interfaces.Repositories
{
    public interface ICompanyPhoneRepository : IRepository<CompanyPhone>
    {
        Task<IEnumerable<CompanyPhone>?> GetAllByCompanyIdWithIncludesAsync(Guid id);
    }
}
