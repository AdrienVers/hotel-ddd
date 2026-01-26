using FluentValidation;
using Hotel.src.Domain.Room.Values;

namespace Hotel.src.Application.Room.Commands.AddRoom;

public sealed class AddRoomCommandValidator : AbstractValidator<RoomInputDto>
{
    public AddRoomCommandValidator(IRoomRepository roomRepository)
    {
        RuleFor(x => x.Number)
            .NotNull()
            .WithMessage("Room number is required.")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Room number must start start to 1.")
            .MustAsync(
                async (number, cancellationToken) =>
                {
                    var exists = await roomRepository.RoomNumberAlreadyExistsAsync(
                        number,
                        cancellationToken
                    );
                    return !exists;
                }
            )
            .WithMessage("A room with this number already exists.");

        RuleFor(x => x.PricePerNight)
            .NotNull()
            .WithMessage("Price per night is required.")
            .GreaterThan(0)
            .WithMessage("Price per night must be greater than 0.");

        RuleFor(x => x.MaxOccupancy)
            .NotNull()
            .WithMessage("Max occupancy is required.")
            .Must(MaxRoomOccupancy.IsValid)
            .WithMessage("Max occupancy must start to 1.");
    }
}
