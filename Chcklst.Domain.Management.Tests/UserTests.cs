namespace Chcklst.Domain.Management.Tests;

public class UserTests
{
    private static readonly UserId UserId = UserId.Create();

    [Fact]
    public void ShouldActivate()
    {
        var user = new User(UserId, false);
        user.Active.Should().BeFalse();

        user.Activate();

        user.Active.Should().BeTrue();
    }

    [Fact]
    public void ShouldDoNothingIfAlreadyActive()
    {
        var user = new User(UserId, true);
        user.Active.Should().BeTrue();

        user.Activate();

        user.Active.Should().BeTrue();
    }

    [Fact]
    public void ShouldDeactivate()
    {
        var user = new User(UserId, true);
        user.Active.Should().BeTrue();

        user.Deactivate();

        user.Active.Should().BeFalse();
    }

    [Fact]
    public void ShouldDoNothingIfAlreadyDeactivated()
    {
        var user = new User(UserId, false);
        user.Active.Should().BeFalse();

        user.Deactivate();

        user.Active.Should().BeFalse();
    }
}
