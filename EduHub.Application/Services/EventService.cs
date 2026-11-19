using EduHub.Application.Interfaces;
using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Application.Services;

// ===== Câu 3b – EventService sau khi refactor =====
// Câu 5a – EventService nằm ở tầng Application.
public class EventService
{
    private readonly IEventRepository _eventRepository;
    private readonly INotificationServiceFactory _notificationFactory;

    // Câu 3b – Nhận các phụ thuộc qua constructor, không còn new lớp cụ thể (sửa lỗi DIP).
    public EventService(
        IEventRepository eventRepository,
        INotificationServiceFactory notificationFactory)
    {
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        _notificationFactory = notificationFactory ?? throw new ArgumentNullException(nameof(notificationFactory));
    }

    public Event CreateEvent(Guid clubId, string title, DateTime date, int capacity, string channel = "email")
    {
        // Câu 3b – Kiểm tra dữ liệu giao cho Event.Create (sửa lỗi SRP).
        var ev = Event.Create(clubId, title, date, capacity);

        // Câu 3b – Lưu qua IEventRepository thay vì SqlConnection.
        _eventRepository.Add(ev);

        // Câu 3b – Factory chọn kênh, không còn if/else (sửa lỗi OCP).
        var notifier = _notificationFactory.Create(channel);
        notifier.Send($"Sự kiện mới: {ev.Title}");
        return ev;
    }

    // Câu 5c – Các hàm phục vụ EventsController.
    public IReadOnlyList<Event> GetEventsByClub(Guid clubId)
        => _eventRepository.GetByClubId(clubId);

    public Event? GetEventById(Guid id)
        => _eventRepository.GetById(id);

    public bool DeleteEvent(Guid id)
        => _eventRepository.Delete(id);
}
