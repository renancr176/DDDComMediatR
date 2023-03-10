using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Repositories
{
    public class PhoneTypeRepository : RepositoryIntId<PhoneType>, IPhoneTypeRepository
    {
        public PhoneTypeRepository(MvnoDbContext context)
            : base(context)
        {
        }
    }
}