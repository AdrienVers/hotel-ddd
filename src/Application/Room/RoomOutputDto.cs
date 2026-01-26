namespace Hotel.src.Application.Room;

public sealed class RoomOutputDto
{
    public Guid RoomId { get; init; }
    public required int Number { get; init; }
    public required int MaxOccupancy { get; init; }
    public required decimal PricePerNight { get; init; }
}
