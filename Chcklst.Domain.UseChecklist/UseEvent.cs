namespace Chcklst.Domain.UseChecklist;

using Chcklst.Domain.SharedKernel;

public abstract class UseEvent : AbstractHistoryEvent
{
    public UseEvent(ChecklistId checklistId, UserId userId) : base()
    {
        this.ChecklistId = checklistId;
        this.UserId = userId;
    }

    public UseEvent(HistoryEventId id, DateTimeOffset timestamp, ChecklistId checklistId, UserId userId) : base(id, timestamp)
    {
        this.ChecklistId = checklistId;
        this.UserId = userId;
    }

    public ChecklistId ChecklistId { get; }

    public UserId UserId { get; }
}
