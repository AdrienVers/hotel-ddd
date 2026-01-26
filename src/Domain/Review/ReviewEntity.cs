using Hotel.src.Domain.Booking.Values;
using Hotel.src.Domain.Review.Values;

namespace Hotel.src.Domain.Review;

public sealed class ReviewEntity(ReviewId id, BookingId bookingId, Rating rating, string comment)
    : Entity<ReviewId>(id)
{
    public BookingId BookingId { get; } = bookingId;
    public Rating Rating { get; } = rating;
    public string Comment { get; } = comment;
}
