using Hotel.src.Domain.Room.Events;
using MediatR;

namespace Hotel.src.Application.Room.AddRoom;

public sealed class AddRoomEventHandler(ILogger<AddRoomEventHandler> logger)
    : INotificationHandler<CreatedRoomEvent>
{
    public Task Handle(CreatedRoomEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Room created with ID: {RoomId} at {Timestamp}",
            notification.RoomId,
            DateTime.UtcNow
        );

        return Task.CompletedTask;
    }
}
