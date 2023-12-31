using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.EditChecklist;

public class ChecklistId : AbstractEntityId<Guid>
{
    public ChecklistId(Guid value) : base(value) {}

    public static ChecklistId Create() => new (Guid.NewGuid());
}
