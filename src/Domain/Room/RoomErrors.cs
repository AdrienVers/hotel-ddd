using Hotel.src.Application.Abstractions;

namespace Hotel.src.Domain.Room;

public static class RoomErrors
{
    public static Error RoomAlreadyExists =>
        Error.Conflict("Room.RoomAlreadyExists", "A room with the same number already exists.");

    public static Error RoomNotFound =>
        Error.NotFound("Room.RoomNotFound", "The specified room was not found.");

    public static Error InvalidNumber =>
        Error.Validation("Room.InvalidNumber", "Room number must be greater than or equal to 1.");

    public static Error InvalidMaxOccupancy =>
        Error.Validation(
            "Room.InvalidMaxOccupancy",
            "Max occupancy must be greater than or equal to 1."
        );

    public static Error InvalidPrice =>
        Error.Validation("Room.InvalidPrice", "Price per night must be greater than 0.");
}
