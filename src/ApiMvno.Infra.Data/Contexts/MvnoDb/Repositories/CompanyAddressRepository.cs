using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Repositories
{
    public class CompanyAddressRepository : Repository<CompanyAddress>, ICompanyAddressRepository
    {
        public CompanyAddressRepository(MvnoDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<CompanyAddress>?> GetAllByCompanyIdWithIncludesAsync(Guid id)
        {
            return await BaseQuery.Where(cp => cp.CompanyId == id)
                .Include(nameof(CompanyAddress.Address))
                .Include($"{nameof(CompanyAddress.Address)}.{nameof(Address.Country)}")
                .ToListAsync();
        }
    }
}
