using Chcklst.Shared.Extensions;

namespace Chcklst.Domain.UseChecklist.Tests;

public class ChecklistTests
{
    private static UserId UserId = new UserId(Guid.NewGuid());
    private static ChecklistId ChecklistId = new ChecklistId(Guid.NewGuid());
    private static readonly string ChecklistName = "A Checklist Name (better than before)";

    private static readonly IList<ChecklistItem> ChecklistItems = new List<ChecklistItem>
    {
        new ("First Item"),
        new ("Second Item"),
        new ("3rd Item", true),
        new ("4rd Item"),
        new ("5th Item"),
    };

    private static readonly IList<UseEvent> UseHistory = new List<UseEvent>();

    [Fact]
    public void ShouldThrowIfEmptyName()
    {
        var createWithEmptyName = () => new Checklist(UserId, ChecklistId, string.Empty, ChecklistItems, UseHistory);

        FluentActions.Invoking(createWithEmptyName).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ShouldHaveProvidedName()
    {
        var checklist = new Checklist(UserId, ChecklistId, ChecklistName, ChecklistItems, UseHistory);

        checklist.Name.Should().Be(ChecklistName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void ShouldToggleItem(int itemIndex)
    {
        var checklist = new Checklist(UserId, ChecklistId, ChecklistName, ChecklistItems, UseHistory);
        var item = checklist.Items[itemIndex];
        var previousValue = item.Checked;

        checklist.ToggleItem(item);

        checklist.Items[itemIndex].Checked.Should().Be(!previousValue);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(4)]
    public void ShouldRecordCheckItemInHistory(int itemIndex)
    {
        var checklist = new Checklist(UserId, ChecklistId, ChecklistName, ChecklistItems, UseHistory);
        var item = checklist.Items[itemIndex];
        var beforeCount = checklist.UseHistory.Count;

        checklist.ToggleItem(item);

        checklist.UseHistory.Count.Should().Be(beforeCount + 1);
        var lastEvent = checklist.UseHistory.Last();
        lastEvent.UserId.Should().Be(UserId);
        lastEvent.ChecklistId.Should().Be(checklist.Id);
        lastEvent.Timestamp.Should().BeBefore(DateTimeOffset.Now);
        lastEvent.Should().BeOfType<CheckItemEvent>().Which.ItemId.Should().Be(item.Id);
    }

    [Theory]
    [InlineData(2)]
    public void ShouldRecordUncheckItemInHistory(int itemIndex)
    {
        var checklist = new Checklist(UserId, ChecklistId, ChecklistName, ChecklistItems, UseHistory);
        var item = checklist.Items[itemIndex];
        var beforeCount = checklist.UseHistory.Count;

        checklist.ToggleItem(item);

        checklist.UseHistory.Count.Should().Be(beforeCount + 1);
        var lastEvent = checklist.UseHistory.Last();
        lastEvent.UserId.Should().Be(UserId);
        lastEvent.ChecklistId.Should().Be(checklist.Id);
        lastEvent.Timestamp.Should().BeBefore(DateTimeOffset.Now);
        lastEvent.Should().BeOfType<UncheckItemEvent>().Which.ItemId.Should().Be(item.Id);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ShouldThrowIfToggleUnknownItem(bool isChecked)
    {
        var checklist = new Checklist(UserId, ChecklistId, ChecklistName, ChecklistItems, UseHistory);
        var item = new ChecklistItem("A Checklist Item", isChecked);

        var toggle = () => checklist.ToggleItem(item);

        FluentActions.Invoking(toggle).Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(new []{false,false,false,false,false})]
    [InlineData(new []{true,true,true,true,true})]
    [InlineData(new []{true,false,true,false,true})]
    [InlineData(new []{false,true,false,true,false})]
    public void ShouldResetAllItems(bool[] checkedStates)
    {
        var checklist = new Checklist(UserId, ChecklistId, ChecklistName, ChecklistItems, UseHistory);
        CheckItems(checklist, checkedStates);

        checklist.Reset();

        checklist.Items.Should().NotContain(item => item.Checked);
    }

    [Fact]
    public void ShouldRecordResetInHistory()
    {
        var checklist = new Checklist(UserId, ChecklistId, ChecklistName, ChecklistItems, UseHistory);
        var beforeCount = checklist.UseHistory.Count;

        checklist.Reset();

        checklist.UseHistory.Count.Should().Be(beforeCount + 1);
        var lastEvent = checklist.UseHistory.Last();
        lastEvent.UserId.Should().Be(UserId);
        lastEvent.ChecklistId.Should().Be(checklist.Id);
        lastEvent.Timestamp.Should().BeBefore(DateTimeOffset.Now);
        lastEvent.Should().BeOfType<ResetEvent>();
    }

    [Fact]
    public void ShouldRecordCompleteInHistory()
    {
        var checklist = new Checklist(UserId, ChecklistId, ChecklistName, ChecklistItems, UseHistory);
        CheckItems(checklist, new[] { true, true, true, true, false });
        var beforeCount = checklist.UseHistory.Count;

        checklist.ToggleItem(checklist.Items.Last());

        checklist.UseHistory.Count.Should().Be(beforeCount + 2);
        var lastEvent = checklist.UseHistory.Last();
        lastEvent.UserId.Should().Be(UserId);
        lastEvent.ChecklistId.Should().Be(checklist.Id);
        lastEvent.Timestamp.Should().BeBefore(DateTimeOffset.Now);
        lastEvent.Should().BeOfType<CompleteEvent>();
    }

    [Fact]
    public void ShouldGetItemsReadonly()
    {
        var checklist = new Checklist(UserId, ChecklistId, ChecklistName, ChecklistItems, UseHistory);

        var clear = () => checklist.Items.Clear();

        FluentActions.Invoking(clear).Should().Throw<NotSupportedException>();
    }

    private static void CheckItems(Checklist checklist, bool[] checkedStates)
    {
        checkedStates.ForEach((isChecked, index) =>
        {
            var item = checklist.Items[index];
            if (isChecked != item.Checked)
            {
                checklist.ToggleItem(item);
            }
        });
    }
}
