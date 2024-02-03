namespace Chcklst.Domain.EditChecklist.Tests;

public class ChecklistItemTests
{
    [Fact]
    public void ShouldStartWithEmptyText()
    {
        var item = new ChecklistItem();

        item.Text.Should().Be(string.Empty);
    }

    [Fact]
    public void ShouldBeReconstitutable()
    {
        var id = ChecklistItemId.Create();
        var text = "Some item text";

        var item = new ChecklistItem(id, text);

        item.Id.Should().Be(id);
        item.Text.Should().Be(text);
    }
}
