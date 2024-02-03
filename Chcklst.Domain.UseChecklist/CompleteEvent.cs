namespace Chcklst.Domain.UseChecklist;

using Chcklst.Domain.SharedKernel;

public class CompleteEvent : UseEvent
{
    public CompleteEvent(ChecklistId checklistId, UserId userId) : base(checklistId, userId)
    {
    }

    public CompleteEvent(HistoryEventId id, DateTimeOffset timestamp, ChecklistId checklistId, UserId userId) : base(id, timestamp, checklistId, userId)
    {
    }
}
