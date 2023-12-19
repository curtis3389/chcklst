namespace Chcklst.Domain.EditChecklist;

public class ChecklistSnapshot
{
    public ChecklistSnapshot(string name, IList<ChecklistItem> items)
    {
        this.Name = name;
        this.Items = items.AsReadOnly();
    }

    public string Name { get; }

    public IList<ChecklistItem> Items { get; }
}
