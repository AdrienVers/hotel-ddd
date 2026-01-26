using Hotel.src.Domain.Shared;
using MediatR;

namespace Hotel.src.Application.Room.Queries.GetRoomsFromPeriod;

public sealed class GetRoomsFromPeriodQueryHandler(IRoomRepository roomRepository)
    : IRequestHandler<GetRoomsFromPeriodQuery, IReadOnlyList<RoomOutputDto>>
{
    public async Task<IReadOnlyList<RoomOutputDto>> Handle(
        GetRoomsFromPeriodQuery request,
        CancellationToken cancellationToken
    )
    {
        var period = new Period(request.Start!.Value, request.End!.Value);

        var availableRooms = await roomRepository.GetAvailableRoomsAsync(period, cancellationToken);

        return
        [
            .. availableRooms.Select(room => new RoomOutputDto
            {
                RoomId = room.Id.Value,
                Number = room.Number.Value,
                MaxOccupancy = room.MaxOccupancy.Value,
                PricePerNight = room.PricePerNight.Amount,
            }),
        ];
    }
}
