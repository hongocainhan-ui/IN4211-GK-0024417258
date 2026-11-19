namespace EduHub.Application.Interfaces;

// ===== Câu 3b – Factory chọn kênh thông báo theo channel =====
// Câu 3a – OCP: thay cho khối if (channel == "email") ... else if (channel == "sms") (dòng 11–14 của đề).
public interface INotificationServiceFactory
{
    INotificationService Create(string channel);
}
