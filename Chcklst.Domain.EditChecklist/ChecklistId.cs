using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.EditChecklist;

public class ChecklistId : AbstractId<Guid>
{
    public ChecklistId(Guid value) : base(value) {}
}
