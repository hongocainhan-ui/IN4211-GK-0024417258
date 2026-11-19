namespace EduHub.Domain.Entities;

// ===== Câu 1a – Kế thừa (Inheritance) =====
// Lớp cha trừu tượng chứa Id dùng chung. Member, Event, Registration kế thừa lớp này.
// Câu 1a – Trừu tượng hoá: abstract nên không tạo trực tiếp được bằng new BaseEntity().
public abstract class BaseEntity
{
    // Câu 1a – Đóng gói: chỉ lớp con mới gán được Id.
    public Guid Id { get; private protected set; } = Guid.NewGuid();
}
