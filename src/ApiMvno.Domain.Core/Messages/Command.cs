using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using ApiMvno.Domain.Core.Data;
using ApiMvno.Domain.Core.DomainObjects;
using MediatR;

namespace ApiMvno.Domain.Core.Messages;

public abstract class Command<TResponse> : Message, IRequest<TResponse>
{
    public DateTime Timestamp { get; private set; }

    protected Command()
    {
        Timestamp = DateTime.UtcNow;
    }
}

public abstract class Command : Command<bool>
{
}

public abstract class PagedCommand<TData, TEntity> : Command<PagedCommandResponse<TData, TEntity>>
    where TEntity : Entity
{
    [Range(1, int.MaxValue)]
    public int PageIndex { get; set; } = 1;
    [Range(1, int.MaxValue)]
    public int PageSize { get; set; } = 50;
}

public abstract class PagedCommandResponse<TData, TEntity> 
    where TEntity : Entity
{
    public int PageIndex { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<TData> Data { get; set; }

    public PagedCommandResponse(PagedCommand<TData, TEntity> command,
        IRepository<TEntity> repository,
        IEnumerable<TData> data,
        Expression<Func<TEntity, bool>> predicate = null)
        
    {
        PageIndex = command.PageIndex;
        Task.Run(async () =>
        {
            TotalCount = await repository.CountAsync(predicate ?? (entity => true));
        }).Wait();
        TotalPages = (int) Math.Ceiling(TotalCount / (double) PageSize);
        PageIndex = TotalPages > 0 && command.PageSize > TotalPages? TotalPages : command.PageSize;
        Data = data;
    }

    public PagedCommandResponse(PagedCommand<TData, TEntity> command,
        IRepository<TEntity> repository,
        List<TData> data,
        Expression<Func<TEntity, bool>> predicate = null)
        : this(command, repository, data.AsEnumerable(), predicate)

    {
    }
}