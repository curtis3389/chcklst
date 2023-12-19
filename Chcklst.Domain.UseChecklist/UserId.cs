using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.UseChecklist;

public class UserId : AbstractEntityId<Guid>
{
    public UserId() : this(Guid.NewGuid()) {}

    public UserId(Guid value) : base(value) {}
}
