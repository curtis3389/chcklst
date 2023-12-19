namespace Chcklst.Domain.EditChecklist.Tests;

public class ChecklistTests
{
    private static readonly ChecklistId ChecklistId = new ChecklistId(Guid.NewGuid());
    private static readonly string ChecklistName = "A Checklist Name";

    public static readonly IList<ChecklistItem> ChecklistItems = new List<ChecklistItem>
    {
        new ("First Item"),
        new ("Second Item"),
        new ("Third Item"),
        new ("Fourst Item"),
        new ("Fiveth Item"),
    };

    [Fact]
    public void ShouldThrowIfEmptyName()
    {
        var createWithEmptyName = () => new Checklist(string.Empty);

        FluentActions.Invoking(createWithEmptyName).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ShouldHaveProvidedName()
    {
        var checklist = new Checklist(ChecklistName);

        checklist.Name.Should().Be(ChecklistName);
    }

    [Fact]
    public void ShouldStartWithZeroItems()
    {
        var checklist = new Checklist(ChecklistName);

        checklist.Items.Count.Should().Be(0);
    }

    [Fact]
    public void ShouldAddItemWhenAddingAnItem()
    {
        var checklist = new Checklist(ChecklistName);
        var beforeCount = checklist.Items.Count;

        checklist.AddItem();

        checklist.Items.Count.Should().Be(beforeCount + 1);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void ShouldRemoveProvidedItem(int index)
    {
        var checklist = new Checklist(ChecklistName);
        checklist.AddItem();
        checklist.AddItem();
        checklist.AddItem();
        checklist.AddItem();
        checklist.AddItem();
        var item = checklist.Items[index];

        checklist.RemoveItem(item);

        checklist.Items.Should().NotContain(item);
    }

    [Fact]
    public void ShouldGetItemsReadonly()
    {
        var checklist = new Checklist(ChecklistName);
        checklist.AddItem();
        checklist.AddItem();
        checklist.AddItem();
        checklist.AddItem();
        checklist.AddItem();
        var clear = () => checklist.Items.Clear();

        FluentActions.Invoking(clear).Should().Throw<NotSupportedException>();
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(4, 4)]
    [InlineData(0, 4)]
    [InlineData(4, 0)]
    [InlineData(0, 2)]
    [InlineData(4, 3)]
    [InlineData(1, 0)]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(1, 3)]
    [InlineData(1, 4)]
    public void ShouldReorderItems(int itemIndex, int destinationIndex)
    {
        var checklist = new Checklist(ChecklistName);
        checklist.AddItem();
        checklist.AddItem();
        checklist.AddItem();
        checklist.AddItem();
        checklist.AddItem();
        var item = checklist.Items[itemIndex];

        checklist.ReorderItem(item, destinationIndex);

        checklist.Items[destinationIndex].Should().Be(item);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(5)]
    [InlineData(-100)]
    [InlineData(500)]
    public void ShouldThrowForInvalidReorderIndex(int index)
    {
        var checklist = new Checklist(ChecklistName);
        checklist.AddItem();
        checklist.AddItem();
        checklist.AddItem();
        checklist.AddItem();
        checklist.AddItem();
        var item = checklist.Items[0];

        var reorder = () => checklist.ReorderItem(item, index);

        FluentActions.Invoking(reorder).Should().Throw<ArgumentOutOfRangeException>();
        checklist.Items.Should().Contain(item);
    }

    [Fact]
    public void ShouldThrowForReorderUnknownItem()
    {
        var checklist = new Checklist(ChecklistName);
        var item = new ChecklistItem();

        var reorder = () => checklist.ReorderItem(item, 0);

        FluentActions.Invoking(reorder).Should().Throw<ArgumentException>();
        checklist.Items.Should().NotContain(item);
    }

    [Fact]
    public void ShouldThrowForRemoveUnknownItem()
    {
        var checklist = new Checklist(ChecklistName);
        var item = new ChecklistItem();

        var reorder = () => checklist.RemoveItem(item);

        FluentActions.Invoking(reorder).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ShouldChangeName()
    {
        var newName = "A New Checklist Name";
        var checklist = new Checklist(ChecklistName);

        checklist.ChangeName(newName);

        checklist.Name.Should().Be(newName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData("\r")]
    [InlineData("\r\n")]
    [InlineData(" ")]
    [InlineData("    \t   \n ")]
    public void ShouldThrowForInvalidNameChange(string invalidName)
    {
        var checklist = new Checklist(ChecklistName);

        var changeName = () => checklist.ChangeName(invalidName);

        FluentActions.Invoking(changeName).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ShouldBeReconstitutable()
    {
        var checklist = new Checklist(ChecklistId, ChecklistName, ChecklistItems);

        checklist.Items.Should().Equal(ChecklistItems);
    }
}
