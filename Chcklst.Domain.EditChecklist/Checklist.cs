using Ardalis.GuardClauses;
using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.EditChecklist;

public class Checklist : AbstractEntity<ChecklistId, Guid>
{
    private readonly EditorId editorId;
    private readonly IList<EditEvent> editHistory;
    private readonly IList<ChecklistItem> items;

    public Checklist(EditorId editorId, string name) : this(
        editorId,
        ChecklistId.Create(),
        name,
        new List<ChecklistItem>(),
        new List<EditEvent>()) {}

    public Checklist(
        EditorId editorId,
        ChecklistId id,
        string name,
        IEnumerable<ChecklistItem> items,
        IList<EditEvent> editHistory) : base(id)
    {
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        this.editorId = editorId;
        this.Name = name;
        this.items = items.ToList();
        this.editHistory = editHistory;
    }

    public IList<EditEvent> EditHistory => this.editHistory.AsReadOnly();

    public IList<ChecklistItem> Items => this.items.AsReadOnly();

    public string Name { get; private set; }

    public void AddItem()
    {
        var item = new ChecklistItem();
        this.items.Add(item);
        this.editHistory.Add(new AddItemEvent(this.Id, this.editorId, item.Id));
    }

    public void ChangeName(string newName)
    {
        Guard.Against.NullOrWhiteSpace(newName, nameof(newName));
        this.Name = newName;
        this.editHistory.Add(new ChangeNameEvent(this.Id, this.editorId, newName));
    }

    public void RemoveItem(ChecklistItem item)
    {
        Guard.Against.DoesntContain(item, nameof(item), this.items);
        this.items.Remove(item);
        this.editHistory.Add(new RemoveItemEvent(this.Id, this.editorId, item.Id));
    }

    public void ReorderItem(ChecklistItem item, int index)
    {
        Guard.Against.DoesntContain(item, nameof(item), this.items);
        Guard.Against.OutOfRange(index, nameof(index), 0, this.items.Count - 1);
        this.items.Remove(item);
        this.items.Insert(index, item);
        this.editHistory.Add(new ReorderItemEvent(this.Id, this.editorId, item.Id, index));
    }

    public void SetItemText(ChecklistItem item, string text)
    {
        Guard.Against.DoesntContain(item, nameof(item), this.items);
        var index = this.items.IndexOf(item);
        this.items.Remove(item);
        this.items.Insert(index, new ChecklistItem(item.Id, text));
        this.editHistory.Add(new SetItemTextEvent(this.Id, this.editorId, item.Id, text));
    }
}
