using Hotel.src.Domain.Room;
using Hotel.src.Domain.Room.Values;
using Hotel.src.Domain.Shared;

namespace Hotel.src.Application.Room;

public interface IRoomRepository
{
    Task<IReadOnlyList<RoomEntity>> GetAllRoomsAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<RoomEntity>> GetAvailableRoomsAsync(
        Period period,
        CancellationToken cancellationToken
    );
    Task<RoomEntity?> GetByIdAsync(RoomId roomId, CancellationToken cancellationToken);
    Task AddAsync(RoomEntity room, CancellationToken cancellationToken);
    Task<bool> RoomNumberAlreadyExistsAsync(int number, CancellationToken cancellationToken);
}
