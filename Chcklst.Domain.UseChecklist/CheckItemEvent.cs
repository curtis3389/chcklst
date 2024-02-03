namespace Chcklst.Domain.UseChecklist;

using Chcklst.Domain.SharedKernel;

public class CheckItemEvent : UseEvent
{
    public CheckItemEvent(ChecklistId checklistId, UserId userId, ChecklistItemId itemId) : base(checklistId, userId)
    {
        this.ItemId = itemId;
    }

    public CheckItemEvent(HistoryEventId id, DateTimeOffset timestamp, ChecklistId checklistId, UserId userId, ChecklistItemId itemId) : base(id, timestamp, checklistId, userId)
    {
        this.ItemId = itemId;
    }

    public ChecklistItemId ItemId { get; }
}
