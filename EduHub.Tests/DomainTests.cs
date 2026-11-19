using EduHub.Domain.Entities;
using EduHub.Infrastructure.Notifications;

namespace EduHub.Tests;

// ===== Test bổ sung cho Câu 1b (Member) và Câu 3b (Event, NotificationServiceFactory) =====
public class DomainTests
{
    // Câu 1b – Email hợp lệ tạo được Member.
    [Fact]
    public void Member_Create_WithValidEmail_ReturnsMember()
    {
        // Act
        var member = Member.Create("student@dthu.edu.vn");

        // Assert
        Assert.NotEqual(Guid.Empty, member.Id);
        Assert.Equal("student@dthu.edu.vn", member.Email);
    }

    // Câu 1b – Email rỗng hoặc thiếu '@' bị từ chối.
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("invalid-email-without-at")]
    public void Member_Create_WithInvalidEmail_ThrowsArgumentException(string invalidEmail)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Member.Create(invalidEmail));
    }

    // Câu 3b – Event.Create từ chối tiêu đề rỗng và sức chứa <= 0.
    [Theory]
    [InlineData("", 10)]
    [InlineData("Workshop", 0)]
    [InlineData("Workshop", -5)]
    public void Event_Create_WithInvalidTitleOrCapacity_ThrowsArgumentException(string title, int capacity)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            Event.Create(Guid.NewGuid(), title, DateTime.UtcNow.AddDays(1), capacity));
    }

    // Câu 3b – Factory chọn đúng lớp theo channel.
    [Theory]
    [InlineData("email", typeof(EmailNotificationService))]
    [InlineData("sms", typeof(SmsNotificationService))]
    public void NotificationServiceFactory_Create_ReturnsMatchingChannelService(string channel, Type expectedType)
    {
        // Arrange
        var factory = new NotificationServiceFactory();

        // Act
        var service = factory.Create(channel);

        // Assert
        Assert.IsType(expectedType, service);
    }
}
