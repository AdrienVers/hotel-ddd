using Hotel.src.Domain.Abstractions;

namespace Hotel.src.Domain.Room.Events;

public sealed record CreatedRoomEvent(Guid RoomId) : IDomainEvent;
