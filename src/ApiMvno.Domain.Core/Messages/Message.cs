using System.Runtime.Serialization;

namespace ApiMvno.Domain.Core.Messages;

public abstract class Message
{
    [IgnoreDataMember]
    public string MessageType { get; protected set; }
    [IgnoreDataMember]
    public Guid AggregateId { get; protected set; }

    protected Message()
    {
        MessageType = GetType().Name;
    }
}