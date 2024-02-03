namespace Chcklst.Domain.EditChecklist;

using Chcklst.Domain.SharedKernel;

public class AddItemEvent : EditEvent
{
    public AddItemEvent(ChecklistId checklistId, EditorId editorId, ChecklistItemId itemId) : base(checklistId, editorId)
    {
        this.ItemId = itemId;
    }

    public AddItemEvent(HistoryEventId id, DateTimeOffset timestamp, ChecklistId checklistId, EditorId editorId, ChecklistItemId itemId) : base(id, timestamp, checklistId, editorId)
    {
        this.ItemId = itemId;
    }

    public ChecklistItemId ItemId { get; }
}
