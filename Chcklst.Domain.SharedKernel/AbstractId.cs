namespace Chcklst.Domain.SharedKernel;

public abstract class AbstractId<T> : ValueObject
{
    public AbstractId(T value)
    {
        this.Value = value;
    }
    
    public T Value { get; }

    public static implicit operator T(AbstractId<T> id) => id.Value;
}