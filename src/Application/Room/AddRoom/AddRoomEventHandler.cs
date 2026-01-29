using Hotel.src.Domain.Room.Events;
using MediatR;
using Serilog.Context;

namespace Hotel.src.Application.Room.AddRoom;

public sealed class AddRoomEventHandler(ILogger<AddRoomEventHandler> logger)
    : INotificationHandler<CreatedRoomEvent>
{
    public Task Handle(CreatedRoomEvent notification, CancellationToken cancellationToken)
    {
        using (LogContext.PushProperty("EventType", "DomainEvent"))
        {
            logger.LogInformation(
                "Room number {RoomNumber} created with ID: {RoomId} at {Timestamp}",
                notification.RoomNumber,
                notification.RoomId,
                DateTime.UtcNow
            );
        }

        return Task.CompletedTask;
    }
}
