using Hotel.src.Domain.Abstractions;

namespace Hotel.src.Domain.Room.Values;

public readonly record struct RoomId : IEntityId<RoomId>
{
    public Guid Value { get; }

    // private constructor
    private RoomId(Guid value) => Value = value;

    public static RoomId Generate() => new(Guid.NewGuid());

    public static RoomId From(Guid value) =>
        value == Guid.Empty
            ? throw new ArgumentException("RoomId cannot be empty.", nameof(value))
            : new RoomId(value);

    public static implicit operator RoomId(Guid v) => From(v);
}
