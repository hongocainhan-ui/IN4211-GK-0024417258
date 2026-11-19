namespace EduHub.Domain.Entities;

public class Registration : BaseEntity
{
    public Guid MemberId { get; private set; }
    public Guid EventId { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public RegistrationStatus Status { get; private set; }

    private Registration(Guid memberId, Guid eventId)
    {
        MemberId = memberId;
        EventId = eventId;
        RegisteredAt = DateTime.UtcNow;
        Status = RegistrationStatus.Pending;
    }

    public static Registration Create(Guid memberId, Guid eventId)
    {
        if (memberId == Guid.Empty)
        {
            throw new ArgumentException("MemberId cannot be empty.", nameof(memberId));
        }

        if (eventId == Guid.Empty)
        {
            throw new ArgumentException("EventId cannot be empty.", nameof(eventId));
        }

        return new Registration(memberId, eventId);
    }

    public void Confirm()
    {
        if (Status != RegistrationStatus.Pending)
        {
            throw new InvalidOperationException($"Cannot confirm registration in '{Status}' status.");
        }

        Status = RegistrationStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status == RegistrationStatus.Cancelled)
        {
            throw new InvalidOperationException("Registration is already cancelled.");
        }

        Status = RegistrationStatus.Cancelled;
    }
}
