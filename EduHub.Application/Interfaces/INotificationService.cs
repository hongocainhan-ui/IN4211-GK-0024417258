namespace EduHub.Application.Interfaces;

// ===== Câu 3b – Interface INotificationService =====
// Câu 3a – DIP: thay cho new EmailSender() / new SmsSender() (dòng 12, 14 của đề).
// Câu 1a – Đa hình: EmailNotificationService và SmsNotificationService cùng cài interface này.
public interface INotificationService
{
    void Send(string message);
}
