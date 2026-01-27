using FluentValidation;
using Hotel.src.Application.Abstractions;
using Hotel.src.Domain.Room;
using Hotel.src.Domain.Room.Values;
using Hotel.src.Domain.Shared;
using MediatR;

namespace Hotel.src.Application.Room.AddRoom;

public sealed class AddRoomCommandHandler(IUnitOfWork unitOfWork, IRoomRepository roomRepository)
    : IRequestHandler<AddRoomCommand, Result<RoomId>>
{
    public async Task<Result<RoomId>> Handle(
        AddRoomCommand request,
        CancellationToken cancellationToken
    )
    {
        var validator = new AddRoomCommandValidator(roomRepository);
        var validationResult = await validator.ValidateAsync(
            new RoomInputDto
            {
                Number = request.Number,
                MaxOccupancy = request.MaxOccupancy,
                PricePerNight = request.PricePerNight,
            },
            cancellationToken
        );

        if (!validationResult.IsValid)
        {
            var errors = validationResult
                .Errors.Select(error => error.CustomState as Error)
                .Where(error => error is not null)
                .ToList()!;

            return Result<RoomId>.Failure(errors.First()!);
        }

        var price = Money.From(request.PricePerNight, Currency.EUR);

        var room = RoomEntity.Create(
            new RoomNumber(request.Number),
            new MaxRoomOccupancy(request.MaxOccupancy),
            price
        );

        try
        {
            await roomRepository.AddAsync(room, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<RoomId>.Success(room.Id.Value);
        }
        catch (ConcurrencyException)
        {
            return Result<RoomId>.Failure(RoomErrors.RoomAlreadyExists);
        }
    }
}
