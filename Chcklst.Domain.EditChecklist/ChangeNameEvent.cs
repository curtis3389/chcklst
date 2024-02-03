namespace Chcklst.Domain.EditChecklist;

using Chcklst.Domain.SharedKernel;

public class ChangeNameEvent : EditEvent
{
    public ChangeNameEvent(ChecklistId checklistId, EditorId editorId, string newName) : base(checklistId, editorId)
    {
        this.NewName = newName;
    }

    public ChangeNameEvent(HistoryEventId id, DateTimeOffset timestamp, ChecklistId checklistId, EditorId editorId, string newName) : base(id, timestamp, checklistId, editorId)
    {
        this.NewName = newName;
    }

    public string NewName { get; }
}
