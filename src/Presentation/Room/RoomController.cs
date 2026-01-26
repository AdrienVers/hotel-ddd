using Hotel.src.Application.Abstractions;
using Hotel.src.Application.Room;
using Hotel.src.Application.Room.AddRoom;
using Hotel.src.Application.Room.GetAllRooms;
using Hotel.src.Application.Room.GetRoomsFromPeriod;
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
        var result = await mediator.Send(command);

        return result.Match(
            onSuccess: roomId => Ok(roomId),
            onFailure: error =>
                error.Type switch
                {
                    Error.ErrorType.Validation => BadRequest(new { error.Code, error.Message }),
                    Error.ErrorType.Conflict => Conflict(new { error.Code, error.Message }),
                    Error.ErrorType.NotFound => NotFound(new { error.Code, error.Message }),
                    _ => StatusCode(500, new { error.Code, error.Message }),
                }
        );
    }
}
