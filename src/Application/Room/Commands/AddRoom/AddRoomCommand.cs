using Hotel.src.Domain.Room.Values;
using MediatR;

namespace Hotel.src.Application.Room.Commands.AddRoom;

public sealed record AddRoomCommand(int Number, int MaxOccupancy, decimal PricePerNight)
    : IRequest<RoomId>;
