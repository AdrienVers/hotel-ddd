using FluentValidation;
using Hotel.src.Domain.Room;
using Hotel.src.Domain.Room.Values;

namespace Hotel.src.Application.Room.RemoveRoom;

public sealed class RemoveRoomCommandValidator : AbstractValidator<RoomId>
{
    public RemoveRoomCommandValidator(IRoomRepository roomRepository)
    {
        RuleFor(x => x.Value)
            .NotEmpty()
            .WithState(_ => RoomErrors.RoomNotFound)
            .MustAsync(
                async (roomId, cancellationToken) =>
                    await roomRepository.GetByIdAsync(RoomId.From(roomId), cancellationToken)
                    != null
            )
            .WithState(_ => RoomErrors.RoomNotFound);
    }
}
