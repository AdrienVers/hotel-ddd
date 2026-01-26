using MediatR;

namespace Hotel.src.Application.Room.Queries.GetAllRooms;

public sealed class GetAllRoomsQueryHandler(IRoomRepository roomRepository)
    : IRequestHandler<GetAllRoomsQuery, List<RoomOutputDto>>
{
    public async Task<List<RoomOutputDto>> Handle(
        GetAllRoomsQuery request,
        CancellationToken cancellationToken
    )
    {
        var rooms = await roomRepository.GetAllRoomsAsync(cancellationToken);

        return
        [
            .. rooms.Select(room => new RoomOutputDto
            {
                RoomId = room.Id.Value,
                Number = room.Number.Value,
                MaxOccupancy = room.MaxOccupancy.Value,
                PricePerNight = room.PricePerNight.Amount,
            }),
        ];
    }
}
