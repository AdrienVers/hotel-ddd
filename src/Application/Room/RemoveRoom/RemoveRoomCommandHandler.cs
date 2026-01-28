using FluentValidation;
using Hotel.src.Application.Abstractions;
using Hotel.src.Domain.Room;
using Hotel.src.Domain.Room.Values;
using Hotel.src.Domain.Shared;
using MediatR;

namespace Hotel.src.Application.Room.RemoveRoom;

public sealed class RemoveRoomCommandHandler(IUnitOfWork unitOfWork, IRoomRepository roomRepository)
    : IRequestHandler<RemoveRoomCommand, Result<RoomId>>
{
    public async Task<Result<RoomId>> Handle(
        RemoveRoomCommand request,
        CancellationToken cancellationToken
    )
    {
        var roomId = RoomId.From(request.RoomId);

        var validator = new RemoveRoomCommandValidator(roomRepository);
        var validationResult = await validator.ValidateAsync(roomId, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors = validationResult
                .Errors.Select(error => error.CustomState as Error)
                .Where(error => error is not null)
                .ToList()!;

            return Result<RoomId>.Failure(errors.First()!);
        }

        var room = await roomRepository.GetByIdAsync(roomId, cancellationToken);

        if (room is null)
        {
            return Result<RoomId>.Failure(RoomErrors.RoomNotFound);
        }

        room.Remove();
        await roomRepository.RemoveAsync(room, cancellationToken);

        try
        {
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<RoomId>.Success(room.Id.Value);
        }
        catch (ConcurrencyException)
        {
            return Result<RoomId>.Failure(RoomErrors.RoomNotFound);
        }
    }
}
