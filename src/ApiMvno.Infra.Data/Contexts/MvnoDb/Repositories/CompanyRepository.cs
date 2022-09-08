using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Repositories;

public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    public CompanyRepository(MvnoDbContext context) 
        : base(context)
    {
    }
}