using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.Management;

public class UserId : AbstractEntityId<Guid>
{
    public UserId(Guid value) : base(value) {}

    public static UserId Create() => new (Guid.NewGuid());
}
