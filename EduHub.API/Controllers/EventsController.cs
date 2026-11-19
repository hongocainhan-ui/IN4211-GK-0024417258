using EduHub.API.DTOs;
using EduHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.API.Controllers;

// ===== Câu 5c – EventsController cho tài nguyên sự kiện của CLB =====
// Câu 1a – Kế thừa: EventsController kế thừa ControllerBase.
// Câu 5a – Controller nằm ở tầng API (Presentation).
[ApiController]
public class EventsController : ControllerBase
{
    private readonly EventService _eventService;

    // Câu 3b – EventService được DI tiêm vào qua constructor.
    public EventsController(EventService eventService)
    {
        _eventService = eventService;
    }

    // Câu 5c – Lấy danh sách sự kiện của CLB: 200 OK, lỗi 400 Bad Request
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

    // Câu 5c – Xem một sự kiện theo Id: 200 OK, lỗi 404 Not Found
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

    // Câu 5c – Tạo sự kiện mới cho CLB: 201 Created, lỗi 400 Bad Request
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

    // Câu 5c – Xoá một sự kiện: 204 No Content, lỗi 404 Not Found
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
