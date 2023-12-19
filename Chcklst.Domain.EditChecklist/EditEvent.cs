using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.EditChecklist;

// TODO: add deltas and a service to create them to save space
public class EditEvent : AbstractHistoryEvent
{
    public EditEvent() {}

    public EditEvent(HistoryEventId id, DateTimeOffset timestamp) : base(id, timestamp) {}

    public ChecklistId ChecklistId { get; }

    public EditorId EditorId { get; }

    public EditEventType EventType { get; }

    public ChecklistSnapshot? Snapshot { get; }
}
