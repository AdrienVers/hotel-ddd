using Hotel.src.Domain.Abstractions;

namespace Hotel.src.Domain.Room.Events;

public sealed record RemovedRoomEvent(Guid RoomId, int RoomNumber) : IDomainEvent;
