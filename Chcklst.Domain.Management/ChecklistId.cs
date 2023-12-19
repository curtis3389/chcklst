using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.Management;

public class ChecklistId : AbstractEntityId<Guid>
{
    public ChecklistId(Guid value) : base(value) {}
}
