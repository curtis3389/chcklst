using Ardalis.GuardClauses;
using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.UseChecklist;

public class Checklist : AbstractEntity<ChecklistId, Guid>
{
    private readonly UserId userId;
    private readonly IList<ChecklistItem> items;
    private readonly IList<UseEvent> useHistory;

    public Checklist(
        UserId userId,
        ChecklistId id,
        string name,
        IEnumerable<ChecklistItem> items,
        IList<UseEvent> useHistory) : base(id)
    {
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        this.userId = userId;
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
        checkedItems.ForEach(item => this.ReplaceItem(item, ChecklistItem.Uncheck(item)));
        this.useHistory.Add(new ResetEvent(this.Id, this.userId));
    }

    public void ToggleItem(ChecklistItem item)
    {
        Guard.Against.DoesntContain(item, nameof(item), this.items);
        this.ReplaceItem(item, ChecklistItem.Toggle(item));
        UseEvent toggleEvent = this.Items.Single(i => i.Id == item.Id).Checked
            ? new CheckItemEvent(this.Id, this.userId, item.Id)
            : new UncheckItemEvent(this.Id, this.userId, item.Id);
        this.useHistory.Add(toggleEvent);

        if (this.Items.All(item => item.Checked))
        {
            this.useHistory.Add(new CompleteEvent(this.Id, this.userId));
        }
    }

    private void ReplaceItem(ChecklistItem current, ChecklistItem replacement)
    {
        var itemIndex = this.items.IndexOf(current);
        this.items.Remove(current);
        this.items.Insert(itemIndex, replacement);
    }
}
