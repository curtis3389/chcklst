namespace Chcklst.Domain.EditChecklist;

using Chcklst.Domain.SharedKernel;

public class ChecklistItemId : AbstractEntityId<Guid>
{
    public ChecklistItemId(Guid value) : base(value) {}

    public static ChecklistItemId Create() => new(Guid.NewGuid());
}
