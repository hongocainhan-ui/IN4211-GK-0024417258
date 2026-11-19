namespace EduHub.Application.Interfaces;

public interface INotificationServiceFactory
{
    INotificationService Create(string channel);
}
