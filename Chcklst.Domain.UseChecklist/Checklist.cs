using Ardalis.GuardClauses;
using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.UseChecklist;

public class Checklist : AbstractEntity<ChecklistId, Guid>
{
    private readonly IList<ChecklistItem> items;
    private readonly IList<UseEvent> useHistory;

    public Checklist(
        ChecklistId id,
        string name,
        IEnumerable<ChecklistItem> items,
        IList<UseEvent> useHistory) : base(id)
    {
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        this.Name = name;
        this.items = items.ToList();
        this.useHistory = useHistory;
    }

    public IList<ChecklistItem> Items => this.items.AsReadOnly();

    public string Name { get; }

    public IList<UseEvent> UseHistory => this.useHistory.AsReadOnly();

    public void Reset()
    {
        var checkedItems = this.items.Where(item => item.Checked).ToList();
        checkedItems.ForEach(this.ToggleItem);
    }

    public void ToggleItem(ChecklistItem item)
    {
        Guard.Against.DoesntContain(item, nameof(item), this.items);
        this.ReplaceItem(item, ChecklistItem.Toggle(item));
    }

    private void ReplaceItem(ChecklistItem current, ChecklistItem replacement)
    {
        var itemIndex = this.items.IndexOf(current);
        this.items.Remove(current);
        this.items.Insert(itemIndex, replacement);
    }
}
