namespace Chcklst.Domain.UseChecklist;

using Chcklst.Domain.SharedKernel;

public class ResetEvent : UseEvent
{
    public ResetEvent(ChecklistId checklistId, UserId userId) : base(checklistId, userId)
    {
    }

    public ResetEvent(HistoryEventId id, DateTimeOffset timestamp, ChecklistId checklistId, UserId userId) : base(id, timestamp, checklistId, userId)
    {
    }
}
