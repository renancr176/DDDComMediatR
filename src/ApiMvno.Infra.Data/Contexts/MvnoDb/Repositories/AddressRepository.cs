using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(MvnoDbContext context)
            : base(context)
        {
        }

        public override async Task DeleteAsync(Guid id)
        {
            var entity = await DbSet.FindAsync(id);

            if (entity != null)
            {
                DbSet.Remove(entity);
            }
        }
    }
}