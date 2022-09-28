using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Repositories
{
    public class AddressesRepository : Repository<Address>, IAddressesRepository
    {
        public AddressesRepository(MvnoDbContext context)
            : base(context)
        {
        }
    }
}