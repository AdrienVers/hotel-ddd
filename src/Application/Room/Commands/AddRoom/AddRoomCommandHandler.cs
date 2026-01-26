using FluentValidation;
using Hotel.src.Domain.Room;
using Hotel.src.Domain.Room.Values;
using Hotel.src.Domain.Shared;
using MediatR;

namespace Hotel.src.Application.Room.Commands.AddRoom;

public sealed class AddRoomCommandHandler(IUnitOfWork unitOfWork, IRoomRepository roomRepository)
    : IRequestHandler<AddRoomCommand, RoomId>
{
    public async Task<RoomId> Handle(AddRoomCommand request, CancellationToken cancellationToken)
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
            throw new ValidationException(validationResult.Errors);
        }

        var price = Money.From(request.PricePerNight, Currency.EUR);

        var room = RoomEntity.Create(
            new RoomNumber(request.Number),
            new MaxRoomOccupancy(request.MaxOccupancy),
            price
        );

        await roomRepository.AddAsync(room, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return room.Id.Value;
    }
}
