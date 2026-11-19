using EduHub.Application.Interfaces;
using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Application.Services;

public class EventService
{
    private readonly IEventRepository _eventRepository;
    private readonly INotificationServiceFactory _notificationFactory;

    public EventService(
        IEventRepository eventRepository,
        INotificationServiceFactory notificationFactory)
    {
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        _notificationFactory = notificationFactory ?? throw new ArgumentNullException(nameof(notificationFactory));
    }

    public Event CreateEvent(Guid clubId, string title, DateTime date, int capacity, string channel = "email")
    {
        var ev = Event.Create(clubId, title, date, capacity);
        _eventRepository.Add(ev);

        var notifier = _notificationFactory.Create(channel);
        notifier.Send($"Sự kiện mới: {ev.Title}");
        return ev;
    }

    public IReadOnlyList<Event> GetEventsByClub(Guid clubId)
        => _eventRepository.GetByClubId(clubId);

    public Event? GetEventById(Guid id)
        => _eventRepository.GetById(id);

    public bool DeleteEvent(Guid id)
        => _eventRepository.Delete(id);
}
