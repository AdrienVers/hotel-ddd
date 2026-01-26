namespace Hotel.src.Domain.Abstractions;

public interface IEntityId<T>
    where T : struct, IEntityId<T>
{
    Guid Value { get; }

    static abstract T Generate();
}
