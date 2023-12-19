namespace Chcklst.Domain.SharedKernel;

public abstract class AbstractEntity<IdType, T> where IdType: AbstractEntityId<T>
{
    protected AbstractEntity(IdType id)
    {
        this.Id = id;
    }

    public IdType Id { get; }
}
