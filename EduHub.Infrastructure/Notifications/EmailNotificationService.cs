using EduHub.Application.Interfaces;

namespace EduHub.Infrastructure.Notifications;

// ===== Câu 3b – EmailNotificationService (chỉ in ra Console) =====
// Câu 1a – Đa hình: cùng cài INotificationService với SmsNotificationService.
public class EmailNotificationService : INotificationService
{
    public void Send(string message)
        => Console.WriteLine($"[EMAIL] {message}");
}
