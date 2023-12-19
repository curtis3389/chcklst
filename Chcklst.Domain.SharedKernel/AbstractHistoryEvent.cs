namespace Chcklst.Domain.SharedKernel;

public abstract class AbstractHistoryEvent : AbstractEntity<HistoryEventId, Guid>
{
    public AbstractHistoryEvent() : this (new HistoryEventId(), DateTimeOffset.Now) {}

    public AbstractHistoryEvent(HistoryEventId id, DateTimeOffset timestamp) : base(id)
    {
        this.Timestamp = timestamp;
    }

    public DateTimeOffset Timestamp { get; }
}
