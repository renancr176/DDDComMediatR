using System.Linq.Expressions;
using ApiMvno.Application.Models.Queries.Requests;
using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Application.Models.Queries.Responses;

public class PagedResponse<TData>
{
    public int PageIndex { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<TData> Data { get; set; }

    public PagedResponse()
    {
    }

    public PagedResponse(PagedRequest request)
    {
        PageIndex = request.PageIndex;
        PageSize = request.PageSize;
    }

    public async Task SetTotals<TEntity>(IRepository<TEntity> repository,
        Expression<Func<TEntity, bool>> predicate = null)
        where TEntity : Entity
    {
        TotalCount = await repository.CountAsync(predicate ?? (entity => true));
        TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
}