using FluentValidation;
using Hotel.src.Domain.Room;
using Hotel.src.Domain.Room.Values;

namespace Hotel.src.Application.Room.AddRoom;

public sealed class AddRoomCommandValidator : AbstractValidator<RoomInputDto>
{
    public AddRoomCommandValidator(IRoomRepository roomRepository)
    {
        RuleFor(x => x.Number)
            .NotNull()
            .WithState(_ => RoomErrors.InvalidNumber)
            .GreaterThanOrEqualTo(1)
            .WithState(_ => RoomErrors.InvalidNumber)
            .MustAsync(
                async (number, cancellationToken) =>
                    !await roomRepository.RoomNumberAlreadyExistsAsync(number, cancellationToken)
            )
            .WithState(_ => RoomErrors.RoomAlreadyExists);

        RuleFor(x => x.PricePerNight)
            .NotNull()
            .WithState(_ => RoomErrors.InvalidPrice)
            .GreaterThan(0)
            .WithState(_ => RoomErrors.InvalidPrice);

        RuleFor(x => x.MaxOccupancy)
            .NotNull()
            .WithState(_ => RoomErrors.InvalidMaxOccupancy)
            .Must(MaxRoomOccupancy.IsValid)
            .WithState(_ => RoomErrors.InvalidMaxOccupancy);
    }
}
