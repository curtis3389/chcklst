using Ardalis.GuardClauses;
using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.EditChecklist;

public class Checklist : AbstractEntity<ChecklistId, Guid>
{
    private readonly IList<ChecklistItem> items = new List<ChecklistItem>();
    
    public Checklist(ChecklistId id, string name) : base(id)
    {
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        this.Name = name;
    }

    public IList<ChecklistItem> Items => this.items.AsReadOnly();
    
    public string Name { get; private set; }

    public void AddItem() => this.items.Add(new ChecklistItem());

    public void ChangeName(string newName)
    {
        Guard.Against.NullOrWhiteSpace(newName, nameof(newName));
        this.Name = newName;
    }

    public void RemoveItem(ChecklistItem item)
    {
        Guard.Against.DoesntContain(item, nameof(item), this.items);
        this.items.Remove(item);
    }

    public void ReorderItem(ChecklistItem item, int index)
    {
        Guard.Against.DoesntContain(item, nameof(item), this.items);
        Guard.Against.OutOfRange(index, nameof(index), 0, this.items.Count - 1);
        this.items.Remove(item);
        this.items.Insert(index, item);
    }
}