using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(MvnoDbContext context)
            : base(context)
        {
        }
    }
}