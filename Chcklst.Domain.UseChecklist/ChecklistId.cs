using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.UseChecklist;

public class ChecklistId : AbstractId<Guid>
{
    public ChecklistId(Guid value) : base(value) {}
}
