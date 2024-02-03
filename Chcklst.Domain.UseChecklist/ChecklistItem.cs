using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.UseChecklist;

public class ChecklistItem : AbstractEntity<ChecklistItemId, Guid>
{
    public ChecklistItem(string text, bool isChecked = false) : base(ChecklistItemId.Create())
    {
        this.Checked = isChecked;
        this.Text = text;
    }

    public ChecklistItem(ChecklistItemId id, string text, bool isChecked) : base(id)
    {
        this.Checked = isChecked;
        this.Text = text;
    }

    public bool Checked { get; }

    public string Text { get; }

    public static ChecklistItem Check(ChecklistItem item) => new (item.Id, item.Text, true);

    public static ChecklistItem Toggle(ChecklistItem item) => new (item.Id, item.Text, !item.Checked);

    public static ChecklistItem Uncheck(ChecklistItem item) => new (item.Id, item.Text, false);
}
