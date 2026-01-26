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
            // throw new ValidationException(validationResult.Errors);
            var firstError = validationResult.Errors.First();
            return Result<RoomId>.Failure(
                Error.Validation("Room.ValidationFailed", firstError.ErrorMessage)
            );
        }
        var exists = await roomRepository.RoomNumberAlreadyExistsAsync(
            request.Number,
            cancellationToken
        );

        if (exists)
        {
            return Result<RoomId>.Failure(RoomErrors.RoomAlreadyExists);
        }

        // Validation du prix
        if (request.PricePerNight <= 0)
        {
            return Result<RoomId>.Failure(RoomErrors.InvalidPrice);
        }

        var price = Money.From(request.PricePerNight, Currency.EUR);

        // Création de la chambre
        var room = RoomEntity.Create(
            new RoomNumber(request.Number),
            new MaxRoomOccupancy(request.MaxOccupancy),
            price
        );

        await roomRepository.AddAsync(room, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<RoomId>.Success(room.Id.Value);
    }
}
