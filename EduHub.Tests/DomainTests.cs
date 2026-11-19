using EduHub.Domain.Entities;
using EduHub.Infrastructure.Notifications;

namespace EduHub.Tests;

public class DomainTests
{
    [Fact]
    public void Member_Create_WithValidEmail_ReturnsMember()
    {
        // Act
        var member = Member.Create("student@dthu.edu.vn");

        // Assert
        Assert.NotEqual(Guid.Empty, member.Id);
        Assert.Equal("student@dthu.edu.vn", member.Email);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("invalid-email-without-at")]
    public void Member_Create_WithInvalidEmail_ThrowsArgumentException(string invalidEmail)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Member.Create(invalidEmail));
    }

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
