namespace Hotel.src.Application.Room;

public sealed class RoomInputDto
{
    public required int Number { get; init; }
    public required int MaxOccupancy { get; init; }
    public required decimal PricePerNight { get; init; }
}
