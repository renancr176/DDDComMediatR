using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Repositories
{
    public class AddressTypeRepository : RepositoryIntId<AddressType>, IAddressTypeRepository
    {
        public AddressTypeRepository(MvnoDbContext context)
            : base(context)
        {
        }
    }
}