using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.Management;

public class Checklist : AbstractEntity<ChecklistId, Guid>
{
    public Checklist(ChecklistId id) : base(id) {}
}
