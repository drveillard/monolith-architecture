using BuildingBlocks.Abstractions.CQRS.Event;
using BuildingBlocks.Core.Types;

namespace BuildingBlocks.Core.CQRS.Event;

public abstract record Event : IEvent
{
    public Guid EventId { get; protected set; } = Guid.NewGuid();

    public long EventVersion { get; protected set; } = -1;

    public DateTime OccurredOn { get; protected set; } = DateTime.Now;

    public DateTimeOffset TimeStamp { get; protected set; } = DateTimeOffset.Now;

    public string EventType => TypeMapper.GetTypeName(GetType());
}
