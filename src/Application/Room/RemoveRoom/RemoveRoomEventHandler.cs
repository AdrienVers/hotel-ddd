using Hotel.src.Domain.Room.Events;
using MediatR;

namespace Hotel.src.Application.Room.RemoveRoom;

public sealed class RemoveRoomEventHandler(ILogger<RemoveRoomEventHandler> logger)
    : INotificationHandler<RemovedRoomEvent>
{
    public Task Handle(RemovedRoomEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Room number {RoomNumber} removed with ID: {RoomId} at {Timestamp}",
            notification.RoomNumber,
            notification.RoomId,
            DateTime.UtcNow
        );

        return Task.CompletedTask;
    }
}
