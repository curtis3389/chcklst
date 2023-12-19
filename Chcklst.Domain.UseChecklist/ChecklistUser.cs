using Chcklst.Domain.SharedKernel;

namespace Chcklst.Domain.UseChecklist;

public class ChecklistUser : AbstractEntity<UserId, Guid>
{
    public ChecklistUser(UserId id) : base(id)
    {
    }

    // TODO: UseChecklist BC's API goes here
    // the methods that API controllers call go here
}
