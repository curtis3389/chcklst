namespace Chcklst.Domain.EditChecklist;

using Chcklst.Domain.SharedKernel;

public class ReorderItemEvent : EditEvent
{
    public ReorderItemEvent(ChecklistId checklistId, EditorId editorId, ChecklistItemId itemId, int index) : base(checklistId, editorId)
    {
        this.ItemId = itemId;
        this.Index = index;
    }

    public ReorderItemEvent(HistoryEventId id, DateTimeOffset timestamp, ChecklistId checklistId, EditorId editorId, ChecklistItemId itemId, int index) : base(id, timestamp, checklistId, editorId)
    {
        this.ItemId = itemId;
        this.Index = index;
    }

    public ChecklistItemId ItemId { get; }

    public int Index { get; }
}
