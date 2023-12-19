namespace Chcklst.Domain.SharedKernel;

public abstract class AbstractEntityId<T> : ValueObject
{
    public AbstractEntityId(T value)
    {
        this.Value = value;
    }

    public T Value { get; }

    public static implicit operator T(AbstractEntityId<T> entityId) => entityId.Value;
}
