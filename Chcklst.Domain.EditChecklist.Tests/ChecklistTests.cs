namespace Chcklst.Domain.EditChecklist.Tests;

public class ChecklistTests
{
    private static readonly ChecklistId ChecklistId = new ChecklistId(Guid.NewGuid());
    private static readonly EditorId EditorId = new EditorId(Guid.NewGuid());
    private static readonly string ChecklistName = "A Checklist Name";

    public static readonly IList<ChecklistItem> ChecklistItems = new List<ChecklistItem>
    {
        new (ChecklistItemId.Create(), "First Item"),
        new (ChecklistItemId.Create(), "Second Item"),
        new (ChecklistItemId.Create(), "Third Item"),
        new (ChecklistItemId.Create(), "Fourst Item"),
        new (ChecklistItemId.Create(), "Fiveth Item"),
    };

    public static readonly IList<EditEvent> EditHistory = new List<EditEvent>();

    [Fact]
    public void ShouldThrowIfEmptyName()
    {
        var createWithEmptyName = () => new Checklist(EditorId, string.Empty);

        FluentActions.Invoking(createWithEmptyName).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ShouldHaveProvidedName()
    {
        var checklist = new Checklist(EditorId, ChecklistName);

        checklist.Name.Should().Be(ChecklistName);
    }

    [Fact]
    public void ShouldStartWithZeroItems()
    {
        var checklist = new Checklist(EditorId, ChecklistName);

        checklist.Items.Count.Should().Be(0);
    }

    [Fact]
    public void ShouldAddItemWhenAddingAnItem()
    {
        var checklist = new Checklist(EditorId, ChecklistName);
        var beforeCount = checklist.Items.Count;

        checklist.AddItem();

        checklist.Items.Count.Should().Be(beforeCount + 1);
    }

    [Fact]
    public void ShouldRecordAddItemInHistory()
    {
        var checklist = new Checklist(EditorId, ChecklistName);
        var beforeCount = checklist.EditHistory.Count;

        checklist.AddItem();

        checklist.EditHistory.Count.Should().Be(beforeCount + 1);
        var lastEvent = checklist.EditHistory.Last();
        lastEvent.ChecklistId.Should().Be(checklist.Id);
        lastEvent.Timestamp.Should().BeBefore(DateTimeOffset.Now);
        var lastItem = checklist.Items.Last();
        lastEvent.Should().BeOfType<AddItemEvent>()
            .Which.ItemId.Should().Be(lastItem.Id);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void ShouldRemoveProvidedItem(int index)
    {
        var checklist = new Checklist(EditorId, ChecklistName);
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
    public void ShouldRecordRemoveItemInHistory()
    {
        var checklist = new Checklist(EditorId, ChecklistName);
        checklist.AddItem();
        var beforeCount = checklist.EditHistory.Count;
        var item = checklist.Items.Last();

        checklist.RemoveItem(item);

        checklist.EditHistory.Count.Should().Be(beforeCount + 1);
        var lastEvent = checklist.EditHistory.Last();
        lastEvent.ChecklistId.Should().Be(checklist.Id);
        lastEvent.Timestamp.Should().BeBefore(DateTimeOffset.Now);
        lastEvent.Should().BeOfType<RemoveItemEvent>()
            .Which.ItemId.Should().Be(item.Id);
    }

    [Fact]
    public void ShouldGetItemsReadonly()
    {
        var checklist = new Checklist(EditorId, ChecklistName);
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
        var checklist = new Checklist(EditorId, ChecklistName);
        checklist.AddItem();
        checklist.AddItem();
        checklist.AddItem();
        checklist.AddItem();
        checklist.AddItem();
        var item = checklist.Items[itemIndex];

        checklist.ReorderItem(item, destinationIndex);

        checklist.Items[destinationIndex].Should().Be(item);
    }

    [Fact]
    public void ShouldRecordReorderItemInHistory()
    {
        var checklist = new Checklist(EditorId, ChecklistName);
        checklist.AddItem();
        checklist.AddItem();
        var beforeCount = checklist.EditHistory.Count;
        var item = checklist.Items.First();
        var otherItem = checklist.Items.Last();
        var destinationIndex = checklist.Items.IndexOf(otherItem);

        checklist.ReorderItem(item, destinationIndex);

        checklist.EditHistory.Count.Should().Be(beforeCount + 1);
        var lastEvent = checklist.EditHistory.Last();
        lastEvent.ChecklistId.Should().Be(checklist.Id);
        lastEvent.Timestamp.Should().BeBefore(DateTimeOffset.Now);
        var reorderEvent = lastEvent.Should().BeOfType<ReorderItemEvent>().Which;
        reorderEvent.ItemId.Should().Be(item.Id);
        reorderEvent.Index.Should().Be(destinationIndex);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(5)]
    [InlineData(-100)]
    [InlineData(500)]
    public void ShouldThrowForInvalidReorderIndex(int index)
    {
        var checklist = new Checklist(EditorId, ChecklistName);
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
        var checklist = new Checklist(EditorId, ChecklistName);
        var item = new ChecklistItem();

        var reorder = () => checklist.ReorderItem(item, 0);

        FluentActions.Invoking(reorder).Should().Throw<ArgumentException>();
        checklist.Items.Should().NotContain(item);
    }

    [Fact]
    public void ShouldThrowForRemoveUnknownItem()
    {
        var checklist = new Checklist(EditorId, ChecklistName);
        var item = new ChecklistItem();

        var reorder = () => checklist.RemoveItem(item);

        FluentActions.Invoking(reorder).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ShouldChangeName()
    {
        var newName = "A New Checklist Name";
        var checklist = new Checklist(EditorId, ChecklistName);

        checklist.ChangeName(newName);

        checklist.Name.Should().Be(newName);
    }

    [Fact]
    public void ShouldRecordChangeNameInHistory()
    {
        var newName = "A New Checklist Name";
        var checklist = new Checklist(EditorId, ChecklistName);
        var beforeCount = checklist.EditHistory.Count;

        checklist.ChangeName(newName);

        checklist.EditHistory.Count.Should().Be(beforeCount + 1);
        var lastEvent = checklist.EditHistory.Last();
        lastEvent.ChecklistId.Should().Be(checklist.Id);
        lastEvent.Timestamp.Should().BeBefore(DateTimeOffset.Now);
        lastEvent.Should().BeOfType<ChangeNameEvent>()
            .Which.NewName.Should().Be(newName);
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
        var checklist = new Checklist(EditorId, ChecklistName);

        var changeName = () => checklist.ChangeName(invalidName);

        FluentActions.Invoking(changeName).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ShouldBeReconstitutable()
    {
        var checklist = new Checklist(EditorId, ChecklistId, ChecklistName, ChecklistItems, EditHistory);

        checklist.Items.Should().Equal(ChecklistItems);
        checklist.EditHistory.Should().Equal(EditHistory);
    }

    [Fact]
    public void ShouldSetItemText()
    {
        var text = "the item text";
        var secondText = "the second item text";
        var checklist = new Checklist(EditorId, ChecklistName);
        checklist.AddItem();

        checklist.SetItemText(checklist.Items.Last(), text);

        checklist.Items.Last().Text.Should().Be(text);

        checklist.SetItemText(checklist.Items.Last(), secondText);

        checklist.Items.Last().Text.Should().Be(secondText);
    }

    [Fact]
    public void ShouldRecordSetItemTextInHistory()
    {
        var text = "the item text";
        var secondText = "the second item text";
        var checklist = new Checklist(EditorId, ChecklistName);
        checklist.AddItem();
        var beforeCount = checklist.EditHistory.Count;
        var item = checklist.Items.Last();

        checklist.SetItemText(item, text);

        checklist.EditHistory.Count.Should().Be(beforeCount + 1);
        var lastEvent = checklist.EditHistory.Last();
        lastEvent.ChecklistId.Should().Be(checklist.Id);
        lastEvent.Timestamp.Should().BeBefore(DateTimeOffset.Now);
        var setTextEvent = lastEvent.Should().BeOfType<SetItemTextEvent>().Which;
        setTextEvent.ItemId.Should().Be(item.Id);
        setTextEvent.Text.Should().Be(text);

        checklist.SetItemText(checklist.Items.Last(), secondText);

        checklist.EditHistory.Count.Should().Be(beforeCount + 2);
        lastEvent = checklist.EditHistory.Last();
        lastEvent.ChecklistId.Should().Be(checklist.Id);
        lastEvent.Timestamp.Should().BeBefore(DateTimeOffset.Now);
        setTextEvent = lastEvent.Should().BeOfType<SetItemTextEvent>().Which;
        setTextEvent.ItemId.Should().Be(item.Id);
        setTextEvent.Text.Should().Be(secondText);
    }

    [Fact]
    public void ShouldThrowIfSetTextForUnknownItem()
    {
        var text = "the item text";
        var checklist = new Checklist(EditorId, ChecklistName);
        var item = new ChecklistItem();

        var setText = () => checklist.SetItemText(item, text);

        FluentActions.Invoking(setText).Should().Throw<ArgumentException>();
        item.Text.Should().NotBe(text);
    }
}
