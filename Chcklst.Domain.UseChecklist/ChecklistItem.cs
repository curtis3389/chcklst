using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.UseChecklist;

public class ChecklistItem : ValueObject
{
    public ChecklistItem(string text, bool isChecked = false)
    {
        this.Checked = isChecked;
        this.Text = text;
    }
    
    public bool Checked { get; } 
    
    public string Text { get; }
    
    public static ChecklistItem Check(ChecklistItem item) => new (item.Text, true);

    public static ChecklistItem Toggle(ChecklistItem item) => new (item.Text, !item.Checked);
    
    public static ChecklistItem Uncheck(ChecklistItem item) => new (item.Text);
}