using Hotel.src.Application.Abstractions;
using Hotel.src.Domain.Room.Values;
using MediatR;

namespace Hotel.src.Application.Room.AddRoom;

public sealed record AddRoomCommand(int Number, int MaxOccupancy, decimal PricePerNight)
    : IRequest<Result<RoomId>>;
