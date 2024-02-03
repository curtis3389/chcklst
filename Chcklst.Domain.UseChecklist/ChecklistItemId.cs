using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.UseChecklist;

public class ChecklistItemId : AbstractEntityId<Guid>
{
    public ChecklistItemId(Guid value) : base(value) {}

    public static ChecklistItemId Create() => new ChecklistItemId(Guid.NewGuid());
}
