using MediatR;

namespace Hotel.src.Application.Room.Queries.GetAllRooms;

public sealed record GetAllRoomsQuery : IRequest<List<RoomOutputDto>>;
