using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;

namespace ApiMvno.Infra.Data.Contexts.IdentityDb.Repositories;

public class UserCompanyRepository : Repository<UserCompany>, IUserCompanyRepository
{
    public UserCompanyRepository(IdentityDbContext context) 
        : base(context)
    {
    }

    public override async Task DeleteAsync(Guid id)
    {
        var entity = await DbSet.FindAsync(id);
        if (entity != null)
            DbSet.Remove(entity);
    }
}