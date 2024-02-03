namespace Chcklst.Domain.EditChecklist;

using Chcklst.Domain.SharedKernel;

public class SetItemTextEvent : EditEvent
{
    public SetItemTextEvent(ChecklistId checklistId, EditorId editorId, ChecklistItemId itemId, string text) : base(checklistId, editorId)
    {
        this.ItemId = itemId;
        this.Text = text;
    }

    public SetItemTextEvent(HistoryEventId id, DateTimeOffset timestamp, ChecklistId checklistId, EditorId editorId, ChecklistItemId itemId, string text) : base(id, timestamp, checklistId, editorId)
    {
        this.ItemId = itemId;
        this.Text = text;
    }

    public ChecklistItemId ItemId { get; }

    public string Text { get; }
}
