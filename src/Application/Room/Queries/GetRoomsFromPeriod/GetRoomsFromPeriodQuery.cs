using MediatR;

namespace Hotel.src.Application.Room.Queries.GetRoomsFromPeriod;

public sealed record GetRoomsFromPeriodQuery(DateOnly? Start, DateOnly? End)
    : IRequest<IReadOnlyList<RoomOutputDto>>;
