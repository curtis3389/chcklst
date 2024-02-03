using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.EditChecklist;

public class ChecklistEditor : AbstractEntity<EditorId, Guid>
{
    public ChecklistEditor(EditorId id) : base(id)
    {
        this.EditorId = id;
    }

    public EditorId EditorId { get; }

    // TODO: EditChecklist BC's API goes here
    // the methods that API controllers call go here
}
