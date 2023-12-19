namespace Chcklst.Domain.UseChecklist;

public class UseEvent
{
    public ChecklistId ChecklistId { get; }

    public UserId UserId { get; }

    public UseEventType EventType { get; }

    public int? ItemIndex { get; }
}
