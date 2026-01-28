using Hotel.src.Domain.Abstractions;
using Hotel.src.Domain.Room.Events;
using Hotel.src.Domain.Room.Values;
using Hotel.src.Domain.Shared;

namespace Hotel.src.Domain.Room;

public sealed class RoomEntity(
    RoomId id,
    RoomNumber number,
    MaxRoomOccupancy maxOccupancy,
    Money pricePerNight
) : Entity<RoomId>(id)
{
    public RoomNumber Number { get; } = number;
    public MaxRoomOccupancy MaxOccupancy { get; } = maxOccupancy;
    public Money PricePerNight { get; } = pricePerNight;

    public static RoomEntity Create(
        RoomNumber number,
        MaxRoomOccupancy maxOccupancy,
        Money pricePerNight
    )
    {
        var room = new RoomEntity(RoomId.Generate(), number, maxOccupancy, pricePerNight);
        room.RaiseDomainEvent(new CreatedRoomEvent(room.Id.Value, number.Value));
        return room;
    }

    public void Remove()
    {
        RaiseDomainEvent(new RemovedRoomEvent(Id.Value, Number.Value));
    }
}
