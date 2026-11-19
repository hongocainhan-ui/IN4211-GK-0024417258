namespace EduHub.Domain.Entities;

// ===== Câu 2b – Lớp Registration (phiếu đăng ký sự kiện) =====
public class Registration : BaseEntity
{
    // Câu 2b – Các thuộc tính Id (từ BaseEntity), MemberId, EventId, RegisteredAt, Status
    // chỉ được sửa bên trong lớp (private set).
    public Guid MemberId { get; private set; }
    public Guid EventId { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public RegistrationStatus Status { get; private set; }

    // Câu 2b – Constructor private.
    private Registration(Guid memberId, Guid eventId)
    {
        MemberId = memberId;
        EventId = eventId;
        RegisteredAt = DateTime.UtcNow;
        Status = RegistrationStatus.Pending; // Luật 1: phiếu mới tạo có trạng thái Pending
    }

    // Câu 2b – Factory method Create, ném ArgumentException khi một trong hai mã là Guid.Empty.
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

    // Câu 2b – Luật 2: chỉ phiếu Pending mới được xác nhận, chuyển sang Confirmed.
    public void Confirm()
    {
        if (Status != RegistrationStatus.Pending)
        {
            throw new InvalidOperationException($"Cannot confirm registration in '{Status}' status.");
        }

        Status = RegistrationStatus.Confirmed;
    }

    // Câu 2b – Luật 3: Pending hoặc Confirmed đều huỷ được; phiếu đã huỷ thì không đổi nữa.
    public void Cancel()
    {
        if (Status == RegistrationStatus.Cancelled)
        {
            throw new InvalidOperationException("Registration is already cancelled.");
        }

        Status = RegistrationStatus.Cancelled;
    }
}
