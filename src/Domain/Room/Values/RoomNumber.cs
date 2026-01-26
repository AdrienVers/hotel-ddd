namespace Hotel.src.Domain.Room.Values;

public readonly record struct RoomNumber(int Value)
{
    public static bool IsValid(int value) => value >= 1;
}
