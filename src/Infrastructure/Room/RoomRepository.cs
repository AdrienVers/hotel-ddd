using Hotel.src.Application.Room;
using Hotel.src.Domain.Room;
using Hotel.src.Domain.Room.Values;
using Hotel.src.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel.src.Infrastructure.Room;

public sealed class RoomRepository(ApplicationDbContext dbContext) : IRoomRepository
{
    private readonly ApplicationDbContext dbContext = dbContext;

    public async Task AddAsync(RoomEntity room, CancellationToken cancellationToken)
    {
        await dbContext.Rooms.AddAsync(room, cancellationToken);
    }

    public async Task<IReadOnlyList<RoomEntity>> GetAllRoomsAsync(
        CancellationToken cancellationToken
    )
    {
        // AsNoTracking for read-only queries
        return await dbContext.Rooms.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<RoomEntity>> GetAvailableRoomsAsync(
        Period period,
        CancellationToken cancellationToken
    )
    {
        // AsNoTracking EF Core method for read-only queries
        return await dbContext.Rooms.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<RoomEntity?> GetByIdAsync(RoomId roomId, CancellationToken cancellationToken)
    {
        return await dbContext.Rooms.FirstOrDefaultAsync(
            room => room.Id == roomId,
            cancellationToken
        );
    }

    public async Task<bool> RoomNumberAlreadyExistsAsync(
        int number,
        CancellationToken cancellationToken
    )
    {
        var roomNumber = new RoomNumber(number);
        return await dbContext.Rooms.AnyAsync(room => room.Number == roomNumber, cancellationToken);
    }

    public async Task RemoveAsync(RoomEntity room, CancellationToken cancellationToken)
    {
        dbContext.Rooms.Remove(room);
    }
}
