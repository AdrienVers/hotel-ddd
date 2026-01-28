using Hotel.src.Application.Abstractions;
using Hotel.src.Domain.Room.Values;
using MediatR;

namespace Hotel.src.Application.Room.RemoveRoom;

public sealed record RemoveRoomCommand(Guid RoomId) : IRequest<Result<RoomId>>;
