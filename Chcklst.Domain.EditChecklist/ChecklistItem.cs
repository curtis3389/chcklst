namespace Chcklst.Domain.EditChecklist;

using Chcklst.Domain.SharedKernel;

public class ChecklistItem : AbstractEntity<ChecklistItemId, Guid>
{
    public ChecklistItem() : base(ChecklistItemId.Create())
    {
        this.Text = string.Empty;
    }

    public ChecklistItem(ChecklistItemId id, string text) : base(id)
    {
        this.Text = text;
    }

    public string Text { get; }
}
