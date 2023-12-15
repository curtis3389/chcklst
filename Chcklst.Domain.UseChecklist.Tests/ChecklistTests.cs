using Chcklst.Shared.Extensions;

namespace Chcklst.Domain.UseChecklist.Tests;

public class ChecklistTests
{
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
    
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void ShouldToggleItem(int itemIndex)
    {
        var checklist = new Checklist(ChecklistId, ChecklistName, ChecklistItems);
        var item = checklist.Items[itemIndex];
        var previousValue = item.Checked;

        checklist.ToggleItem(item);

        checklist.Items[itemIndex].Checked.Should().Be(!previousValue);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ShouldThrowIfToggleUnknownItem(bool isChecked)
    {
        var checklist = new Checklist(ChecklistId, ChecklistName, ChecklistItems);
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
        var checklist = new Checklist(ChecklistId, ChecklistName, ChecklistItems);
        CheckItems(checklist, checkedStates);
        
        checklist.Reset();

        checklist.Items.Should().NotContain(item => item.Checked);
    }
    
    [Fact]
    public void ShouldGetItemsReadonly()
    {
        var checklist = new Checklist(ChecklistId, ChecklistName, ChecklistItems);
        
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