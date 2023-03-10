using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Repositories
{
    public class CompanyPhoneRepository : Repository<CompanyPhone>, ICompanyPhoneRepository
    {
        public CompanyPhoneRepository(MvnoDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<CompanyPhone>?> GetAllByCompanyIdWithIncludesAsync(Guid id)
        {
            return await BaseQuery.Where(cp => cp.CompanyId == id)
                .Include(nameof(CompanyPhone.Phone))
                .ToListAsync();
        }
    }
}