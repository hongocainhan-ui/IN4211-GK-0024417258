namespace EduHub.Domain.Entities;

// ===== Câu 1b – Lớp Member đã đóng gói =====
// Câu 1a – Đóng gói + Kế thừa: Member kế thừa BaseEntity, mọi thuộc tính chỉ sửa được bên trong lớp.
public class Member : BaseEntity
{
    // Câu 1b – Sửa vấn đề 1: setter là private, bên ngoài không gán được m.Email = "abc".
    public string Email { get; private set; }

    // Câu 1b – Sửa vấn đề 2: constructor private, bên ngoài không gọi được new Member().
    private Member(string email)
    {
        Email = email;
    }

    // Câu 1b – Factory method: kiểm tra email không rỗng và có ký tự '@'.
    public static Member Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
        {
            throw new ArgumentException("Email must not be empty and must contain '@'.", nameof(email));
        }

        return new Member(email.Trim());
    }
}
