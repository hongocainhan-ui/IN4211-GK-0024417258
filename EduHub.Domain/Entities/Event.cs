namespace EduHub.Domain.Entities;

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
