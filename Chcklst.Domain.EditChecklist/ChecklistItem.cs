namespace Chcklst.Domain.EditChecklist;

public class ChecklistItem
{
    public ChecklistItem() : this(string.Empty) {}

    public ChecklistItem(string text)
    {
        this.Text = text;
    }

    public string Text { get; private set; }

    public void SetText(string text)
    {
        this.Text = text;
    }
}
