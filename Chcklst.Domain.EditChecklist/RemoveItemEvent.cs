namespace Chcklst.Domain.EditChecklist;

using Chcklst.Domain.SharedKernel;

public class RemoveItemEvent : EditEvent
{
    public RemoveItemEvent(ChecklistId checklistId, EditorId editorId, ChecklistItemId itemId) : base(checklistId, editorId)
    {
        this.ItemId = itemId;
    }

    public RemoveItemEvent(HistoryEventId id, DateTimeOffset timestamp, ChecklistId checklistId, EditorId editorId, ChecklistItemId itemId) : base(id, timestamp, checklistId, editorId)
    {
        this.ItemId = itemId;
    }

    public ChecklistItemId ItemId { get; }
}
