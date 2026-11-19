namespace EduHub.API.DTOs;

public class CreateEventRequest
{
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.UtcNow.AddDays(7);
    public int Capacity { get; set; } = 50;
    public string Channel { get; set; } = "email";
}
