using Ardalis.GuardClauses;
using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.Management;

public class Tenant : AbstractEntity<TenantId, Guid>
{
    private readonly IList<Checklist> checklists;
    private readonly IList<User> users;

    public Tenant(string name) : this(
        TenantId.Create(),
        name,
        true,
        new List<Checklist>(),
        new List<User>()) {}

    public Tenant(
        TenantId id,
        string name,
        bool active,
        IList<Checklist> checklists,
        IList<User> users) : base(id)
    {
        Guard.Against.NullOrWhiteSpace(name);
        this.Name = name;
        this.Active = active;
        this.checklists = checklists;
        this.users = users;
    }

    public bool Active { get; private set; }

    public string Name { get; private set; }

    public IList<Checklist> Checklists => this.checklists.AsReadOnly();

    public IList<User> Users => this.users.AsReadOnly();

    public void Activate() => this.Active = true;

    public User CreateUser()
    {
        var newUser = new User();
        this.users.Add(newUser);
        return newUser;
    }

    public void Deactivate() => this.Active = false;

    public void DeleteUser(User user)
    {
        Guard.Against.DoesntContain(user, nameof(user), this.users);
        this.users.Remove(user);
    }

    public void SetName(string newName)
    {
        Guard.Against.NullOrWhiteSpace(newName);
        this.Name = newName;
    }
}
