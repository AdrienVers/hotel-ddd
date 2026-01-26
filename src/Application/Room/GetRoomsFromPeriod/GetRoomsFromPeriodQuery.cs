using MediatR;

namespace Hotel.src.Application.Room.GetRoomsFromPeriod;

public sealed record GetRoomsFromPeriodQuery(DateOnly? Start, DateOnly? End)
    : IRequest<IReadOnlyList<RoomOutputDto>>;
