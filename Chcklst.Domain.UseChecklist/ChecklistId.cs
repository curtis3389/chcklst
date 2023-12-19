using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.UseChecklist;

public class ChecklistId : AbstractEntityId<Guid>
{
    public ChecklistId(Guid value) : base(value) {}
}
