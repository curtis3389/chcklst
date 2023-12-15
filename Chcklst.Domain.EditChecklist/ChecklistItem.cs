namespace Chcklst.Domain.EditChecklist;

public class ChecklistItem
{
    public ChecklistItem()
    {
        this.Text = string.Empty;
    }
    
    public string Text { get; private set; }

    public void SetText(string text)
    {
        this.Text = text;
    }
}