using MediatR;

namespace Hotel.src.Application.Room.GetAllRooms;

public sealed record GetAllRoomsQuery : IRequest<List<RoomOutputDto>>;
