namespace Chcklst.Domain.Management.Tests;

public class TenantTests
{
    private static readonly TenantId TenantId = TenantId.Create();
    private static readonly string TenantName = "A Tenant Name";
    private static readonly IList<Checklist> TenantChecklists = new List<Checklist>();
    private static readonly IList<User> TenantUsers = new List<User>();

    [Fact]
    public void ShouldActivate()
    {
        var tenant = new Tenant(
            TenantId,
            TenantName,
            false,
            TenantChecklists,
            TenantUsers);
        tenant.Active.Should().BeFalse();

        tenant.Activate();

        tenant.Active.Should().BeTrue();
    }

    [Fact]
    public void ShouldDoNothingIfAlreadyActive()
    {
        var tenant = new Tenant(
            TenantId,
            TenantName,
            true,
            TenantChecklists,
            TenantUsers);
        tenant.Active.Should().BeTrue();

        tenant.Activate();

        tenant.Active.Should().BeTrue();
    }

    [Fact]
    public void ShouldDeactivate()
    {
        var tenant = new Tenant(
            TenantId,
            TenantName,
            true,
            TenantChecklists,
            TenantUsers);
        tenant.Active.Should().BeTrue();

        tenant.Deactivate();

        tenant.Active.Should().BeFalse();
    }

    [Fact]
    public void ShouldDoNothingIfAlreadyDeactivated()
    {
        var tenant = new Tenant(
            TenantId,
            TenantName,
            false,
            TenantChecklists,
            TenantUsers);
        tenant.Active.Should().BeFalse();

        tenant.Deactivate();

        tenant.Active.Should().BeFalse();
    }

    [Fact]
    public void ShouldReturnChecklistsReadOnly()
    {
        var tenant = new Tenant(
            TenantId,
            TenantName,
            true,
            TenantChecklists,
            TenantUsers);
        var checklist = new Checklist(new ChecklistId(Guid.NewGuid()));

        var directInsert = () => tenant.Checklists.Add(checklist);

        FluentActions.Invoking(directInsert).Should().Throw<NotSupportedException>();
    }

    [Fact]
    public void ShouldReturnUsersReadOnly()
    {
        var tenant = new Tenant(
            TenantId,
            TenantName,
            true,
            TenantChecklists,
            TenantUsers);
        var user = new User();

        var directInsert = () => tenant.Users.Add(user);

        FluentActions.Invoking(directInsert).Should().Throw<NotSupportedException>();
    }

    [Fact]
    public void ShouldChangeName()
    {
        var tenant = new Tenant(
            TenantId,
            TenantName,
            true,
            TenantChecklists,
            TenantUsers);
        var newName = "New Tenant Name";

        tenant.SetName(newName);

        tenant.Name.Should().Be(newName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData("\r")]
    [InlineData("\r\n")]
    [InlineData(" ")]
    [InlineData("    \t   \n ")]
    public void ShouldThrowIfInvalidNameChange(string newName)
    {
        var tenant = new Tenant(
            TenantId,
            TenantName,
            true,
            TenantChecklists,
            TenantUsers);

        var changeName = () => tenant.SetName(newName);

        FluentActions.Invoking(changeName).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ShouldCreateUser()
    {
        var tenant = new Tenant(
            TenantId,
            TenantName,
            true,
            TenantChecklists,
            new List<User>());
        var beforeCount = tenant.Users.Count;

        var user = tenant.CreateUser();

        tenant.Users.Should().Contain(user);
        tenant.Users.Count.Should().Be(beforeCount + 1);
    }

    [Fact]
    public void ShouldDeleteUser()
    {
        var user = new User();
        var tenant = new Tenant(
            TenantId,
            TenantName,
            true,
            TenantChecklists,
            new List<User>{user});
        var beforeCount = tenant.Users.Count;
        tenant.Users.Should().Contain(user);

        tenant.DeleteUser(user);

        tenant.Users.Should().NotContain(user);
        tenant.Users.Count.Should().Be(beforeCount - 1);
    }

    [Fact]
    public void ShouldThrowIfDeleteUnknownUser()
    {
        var user = new User();
        var tenant = new Tenant(
            TenantId,
            TenantName,
            true,
            TenantChecklists,
            new List<User>());
        var beforeCount = tenant.Users.Count;
        tenant.Users.Should().NotContain(user);

        var deleteUser = () => tenant.DeleteUser(user);

        FluentActions.Invoking(deleteUser).Should().Throw<ArgumentException>();
        tenant.Users.Should().NotContain(user);
        tenant.Users.Count.Should().Be(beforeCount);
    }
}
