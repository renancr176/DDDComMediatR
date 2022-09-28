using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Repositories;

public class LineTypeRepository : Repository<LineType>, ILineTypeRepository
{
    public LineTypeRepository(MvnoDbContext context) : base(context)
    {
    }
}