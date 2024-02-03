using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.EditChecklist;

// TODO: add deltas and a service to create them to save space
public abstract class EditEvent : AbstractHistoryEvent
{
    public EditEvent(ChecklistId checklistId, EditorId editorId) : base()
    {
        this.ChecklistId = checklistId;
        this.EditorId = editorId;
    }

    public EditEvent(HistoryEventId id, DateTimeOffset timestamp, ChecklistId checklistId, EditorId editorId) : base(id, timestamp)
    {
        this.ChecklistId = checklistId;
        this.EditorId = editorId;
    }

    public ChecklistId ChecklistId { get; }

    public EditorId EditorId { get; }

    public ChecklistSnapshot? Snapshot { get; }
}
