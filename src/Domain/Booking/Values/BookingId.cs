using Hotel.src.Domain.Abstractions;

namespace Hotel.src.Domain.Booking.Values;

public readonly record struct BookingId : IEntityId<BookingId>
{
    public Guid Value { get; }

    // private constructor
    private BookingId(Guid value) => Value = value;

    // create a new unique id
    public static BookingId Generate() => new(Guid.NewGuid());

    // create from existing guid
    public static BookingId From(Guid value) =>
        value == Guid.Empty
            ? throw new ArgumentException("BookingId cannot be empty.", nameof(value))
            : new BookingId(value);
}
