using Hotel.src.Domain.Abstractions;

namespace Hotel.src.Domain.Customer.Values;

public readonly record struct CustomerId : IEntityId<CustomerId>
{
    public Guid Value { get; }

    // private constructor
    private CustomerId(Guid value) => Value = value;

    public static CustomerId Generate() => new(Guid.NewGuid());

    public static CustomerId From(Guid value) =>
        value == Guid.Empty
            ? throw new ArgumentException("CustomerId cannot be empty.", nameof(value))
            : new CustomerId(value);
}
