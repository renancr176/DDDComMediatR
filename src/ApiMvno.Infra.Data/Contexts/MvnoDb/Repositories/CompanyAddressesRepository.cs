using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Repositories
{
    public class CompanyAddressesRepository : Repository<CompanyAddress>, ICompanyAddressesRepository
    {
        public CompanyAddressesRepository(MvnoDbContext context)
            : base(context)
        {
        }
    }
}
