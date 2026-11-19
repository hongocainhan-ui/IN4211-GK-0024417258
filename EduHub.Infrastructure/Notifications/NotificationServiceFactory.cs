using EduHub.Application.Interfaces;

namespace EduHub.Infrastructure.Notifications;

// ===== Câu 3b – Factory chọn kênh thông báo theo channel (mẫu Factory) =====
// Câu 3a – OCP: thêm kênh mới chỉ cần thêm một dòng ở đây, EventService không phải sửa.
public class NotificationServiceFactory : INotificationServiceFactory
{
    public INotificationService Create(string channel)
    {
        if (string.IsNullOrWhiteSpace(channel))
        {
            throw new ArgumentException("Notification channel cannot be empty.", nameof(channel));
        }

        return channel.Trim().ToLowerInvariant() switch
        {
            "email" => new EmailNotificationService(),
            "sms" => new SmsNotificationService(),
            _ => throw new ArgumentException($"Unsupported notification channel: '{channel}'.", nameof(channel))
        };
    }
}
