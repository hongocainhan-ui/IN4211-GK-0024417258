using EduHub.API.DTOs;
using EduHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.API.Controllers;

[ApiController]
public class EventsController : ControllerBase
{
    private readonly EventService _eventService;

    public EventsController(EventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet("/api/clubs/{clubId}/events")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetByClub(Guid clubId)
    {
        if (clubId == Guid.Empty)
        {
            return BadRequest(new { error = "ClubId cannot be empty." });
        }

        var events = _eventService.GetEventsByClub(clubId);
        return Ok(events);
    }

    [HttpGet("/api/events/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var ev = _eventService.GetEventById(id);
        if (ev is null)
        {
            return NotFound(new { error = $"Event with id '{id}' was not found." });
        }

        return Ok(ev);
    }

    [HttpPost("/api/clubs/{clubId}/events")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create(Guid clubId, [FromBody] CreateEventRequest request)
    {
        try
        {
            var channel = string.IsNullOrWhiteSpace(request.Channel) ? "email" : request.Channel;
            var created = _eventService.CreateEvent(
                clubId,
                request.Title,
                request.Date,
                request.Capacity,
                channel);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("/api/events/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        var deleted = _eventService.DeleteEvent(id);
        if (!deleted)
        {
            return NotFound(new { error = $"Event with id '{id}' was not found." });
        }

        return NoContent();
    }
}
