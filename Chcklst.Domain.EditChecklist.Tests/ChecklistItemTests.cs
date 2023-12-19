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
    public void ShouldSetText()
    {
        var text = "Some item text";
        var item = new ChecklistItem();

        item.SetText(text);

        item.Text.Should().Be(text);
    }

    [Fact]
    public void ShouldBeReconstitutable()
    {
        var text = "Some item text";

        var item = new ChecklistItem(text);

        item.Text.Should().Be(text);
    }
}
