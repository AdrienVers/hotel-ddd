using CommandLine;
using Hotel.src.Application.Room;
using Hotel.src.Application.Room.Commands.AddRoom;
using Hotel.src.Application.Room.Queries.GetAllRooms;
using Hotel.src.Application.Room.Queries.GetRoomsFromPeriod;
using Hotel.src.Domain.Room.Values;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.src.Presentation.Room;

[Route("api/rooms")]
[ApiController]
public sealed class RoomController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<RoomOutputDto>>> GetAllAsync()
    {
        var result = await mediator.Send(new GetAllRoomsQuery());
        return Ok(result);
    }

    [HttpGet("available")]
    public async Task<ActionResult<List<RoomOutputDto>>> GetAvailableAsync(
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end
    )
    {
        var result = await mediator.Send(new GetRoomsFromPeriodQuery(start, end));
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<RoomId>> CreateAsync([FromBody] AddRoomCommand command)
    {
        RoomId roomId = await mediator.Send(command);
        return Ok(roomId);
    }
}
