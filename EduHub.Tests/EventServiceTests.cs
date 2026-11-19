using EduHub.Application.Interfaces;
using EduHub.Application.Services;
using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;
using Moq;

namespace EduHub.Tests;

// ===== Câu 3c – Unit test cho EventService, dùng Moq =====
// Giả lập IEventRepository và INotificationService (factory giả trả về notification giả).
public class EventServiceTests
{
    private readonly Mock<IEventRepository> _mockRepository = new();
    private readonly Mock<INotificationService> _mockNotificationService = new();
    private readonly Mock<INotificationServiceFactory> _mockFactory = new();
    private readonly EventService _service;

    public EventServiceTests()
    {
        _mockFactory
            .Setup(f => f.Create(It.IsAny<string>()))
            .Returns(_mockNotificationService.Object);

        _service = new EventService(_mockRepository.Object, _mockFactory.Object);
    }

    // ===== Câu 3c – TEST BẮT BUỘC =====
    // Tạo sự kiện hợp lệ thì Add và Send đều được gọi đúng một lần (Times.Once).
    [Fact]
    public void CreateEvent_WithValidInput_CallsAddAndSendOnce()
    {
        // Arrange
        var clubId = Guid.NewGuid();
        var title = "Workshop Clean Architecture";
        var date = new DateTime(2026, 11, 20, 9, 0, 0, DateTimeKind.Utc);
        var capacity = 100;

        // Act
        var createdEvent = _service.CreateEvent(clubId, title, date, capacity, "email");

        // Assert
        Assert.Equal(clubId, createdEvent.ClubId);
        Assert.Equal(title, createdEvent.Title);
        Assert.Equal(capacity, createdEvent.Capacity);

        _mockRepository.Verify(r => r.Add(It.Is<Event>(e => e.Title == title && e.ClubId == clubId)), Times.Once);
        _mockNotificationService.Verify(n => n.Send(It.Is<string>(msg => msg.Contains(title))), Times.Once);
    }

    // Câu 3b – Factory nhận đúng channel "sms".
    [Fact]
    public void CreateEvent_WithSmsChannel_AsksFactoryForSmsNotifier()
    {
        // Act
        _service.CreateEvent(Guid.NewGuid(), "Seminar", DateTime.UtcNow.AddDays(1), 30, "sms");

        // Assert
        _mockFactory.Verify(f => f.Create("sms"), Times.Once);
    }

    // Câu 3b – Tiêu đề rỗng: ném lỗi, không lưu và không gửi thông báo.
    [Fact]
    public void CreateEvent_WithEmptyTitle_ThrowsArgumentExceptionAndDoesNotCallDependencies()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            _service.CreateEvent(Guid.NewGuid(), "", DateTime.UtcNow.AddDays(1), 50, "email"));

        _mockRepository.Verify(r => r.Add(It.IsAny<Event>()), Times.Never);
        _mockNotificationService.Verify(n => n.Send(It.IsAny<string>()), Times.Never);
    }
}
