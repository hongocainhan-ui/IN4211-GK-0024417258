using EduHub.Application.Interfaces;

namespace EduHub.Infrastructure.Notifications;

// ===== Câu 3b – SmsNotificationService (chỉ in ra Console) =====
// Câu 1a – Đa hình: cùng cài INotificationService với EmailNotificationService.
public class SmsNotificationService : INotificationService
{
    public void Send(string message)
        => Console.WriteLine($"[SMS] {message}");
}
