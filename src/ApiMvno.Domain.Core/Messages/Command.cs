using System.Runtime.Serialization;
using MediatR;

namespace ApiMvno.Domain.Core.Messages;

public abstract class Command<TResponse> : Message, IRequest<TResponse>
{
    [IgnoreDataMember]
    public DateTime Timestamp { get; private set; }

    [IgnoreDataMember]
    public string? RequestId { get; set; }

    protected Command()
    {
        Timestamp = DateTime.UtcNow;
    }
}

public abstract class Command : Command<bool>
{
}