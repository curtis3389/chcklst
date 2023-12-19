using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.EditChecklist;

public class EditorId : AbstractEntityId<Guid>
{
    public EditorId() : this(Guid.NewGuid()) {}

    public EditorId(Guid value) : base(value)
    {
    }
}
