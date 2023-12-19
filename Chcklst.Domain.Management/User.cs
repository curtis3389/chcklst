using Ardalis.GuardClauses;
using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.Management;

public class User : AbstractEntity<UserId, Guid>
{
    private bool active;

    public User() : this(UserId.Create(), true) {}

    public User(UserId id, bool active) : base(id)
    {
        this.active = active;
    }

    public bool Active => this.active;

    public void Activate() => this.active = true;

    public void Deactivate() => this.active = false;
}
