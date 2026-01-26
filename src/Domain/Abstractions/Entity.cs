namespace Hotel.src.Domain.Abstractions;

// primary constructor (C# 12)
public abstract class Entity<T>(T id)
    // generic constraint on T, which
    // must be a value type (a record struct),
    // must implement the IEntityId<T> interface.
    where T : struct, IEntityId<T>
{
    public T Id { get; } = id;
    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
