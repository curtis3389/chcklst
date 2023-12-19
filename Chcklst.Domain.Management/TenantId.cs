using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.Management;

public class TenantId : AbstractEntityId<Guid>
{
    public TenantId(Guid value) : base(value) {}

    public static TenantId Create() => new (Guid.NewGuid());
}
