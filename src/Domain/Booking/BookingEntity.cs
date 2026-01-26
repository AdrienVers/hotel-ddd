using Hotel.src.Domain.Booking.Values;
using Hotel.src.Domain.Customer.Values;
using Hotel.src.Domain.Room.Values;
using Hotel.src.Domain.Shared;

namespace Hotel.src.Domain.Booking;

public sealed class BookingEntity(
    BookingId id,
    Period period,
    DateTime createdUtc,
    CustomerId customerId,
    RoomId roomId
) : Entity<BookingId>(id)
{
    public CustomerId CustomerId { get; } = customerId;
    public RoomId RoomId { get; } = roomId;
    public Period Period { get; init; } = period;
    public DateTime CreatedUtc { get; } = createdUtc;
    public DateTime? ConfirmedUtc { get; }
    public DateTime? RejectedUtc { get; }
    public DateTime? CancelledUtc { get; }
    public DateTime? CompletedUtc { get; }
}
