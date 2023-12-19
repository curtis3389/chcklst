namespace Chcklst.Domain.SharedKernel;

public class HistoryEventId : AbstractEntityId<Guid>
{
    public HistoryEventId() : this(Guid.NewGuid()) {}

    public HistoryEventId(Guid value) : base(value)
    {
    }
}
