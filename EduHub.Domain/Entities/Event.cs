namespace EduHub.Domain.Entities;

// ===== Câu 3b – Entity Event =====
// Câu 3a – SRP: việc kiểm tra dữ liệu sự kiện (dòng 6–7 của đề) được tách khỏi EventService, chuyển vào đây.
// Câu 5a – Event nằm ở tầng Domain.
public class Event : BaseEntity
{
    public Guid ClubId { get; private set; }
    public string Title { get; private set; }
    public DateTime Date { get; private set; }
    public int Capacity { get; private set; }

    private Event(Guid clubId, string title, DateTime date, int capacity)
    {
        ClubId = clubId;
        Title = title;
        Date = date;
        Capacity = capacity;
    }

    // Câu 3b – Event.Create(...) kiểm tra tiêu đề không rỗng và sức chứa lớn hơn 0.
    public static Event Create(Guid clubId, string title, DateTime date, int capacity)
    {
        if (clubId == Guid.Empty)
        {
            throw new ArgumentException("ClubId cannot be empty.", nameof(clubId));
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Event title cannot be empty.", nameof(title));
        }

        if (capacity <= 0)
        {
            throw new ArgumentException("Event capacity must be greater than zero.", nameof(capacity));
        }

        return new Event(clubId, title.Trim(), date, capacity);
    }
}
