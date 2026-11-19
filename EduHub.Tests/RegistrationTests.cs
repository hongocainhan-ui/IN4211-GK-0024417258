using EduHub.Domain.Entities;

namespace EduHub.Tests;

public class RegistrationTests
{
    [Fact]
    public void Create_WithValidIds_SetsPendingStatus()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        var eventId = Guid.NewGuid();

        // Act
        var r = Registration.Create(memberId, eventId);

        // Assert
        Assert.NotEqual(Guid.Empty, r.Id);
        Assert.Equal(memberId, r.MemberId);
        Assert.Equal(eventId, r.EventId);
        Assert.Equal(RegistrationStatus.Pending, r.Status);
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public void Create_WithEmptyGuid_ThrowsArgumentException(bool emptyMemberId, bool emptyEventId)
    {
        // Arrange
        var memberId = emptyMemberId ? Guid.Empty : Guid.NewGuid();
        var eventId = emptyEventId ? Guid.Empty : Guid.NewGuid();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Registration.Create(memberId, eventId));
    }

    [Fact]
    public void Confirm_WhenPending_ChangesStatusToConfirmed()
    {
        // Arrange
        var r = Registration.Create(Guid.NewGuid(), Guid.NewGuid());

        // Act
        r.Confirm();

        // Assert
        Assert.Equal(RegistrationStatus.Confirmed, r.Status);
    }

    [Fact]
    public void Cancel_WhenConfirmed_ChangesStatusToCancelled()
    {
        // Arrange
        var r = Registration.Create(Guid.NewGuid(), Guid.NewGuid());
        r.Confirm();

        // Act
        r.Cancel();

        // Assert
        Assert.Equal(RegistrationStatus.Cancelled, r.Status);
    }

    [Fact]
    public void Confirm_AfterCancel_ThrowsInvalidOperationExceptionAndKeepsCancelledStatus()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        var eventId = Guid.NewGuid();

        // Act & Assert (Tình huống bắt buộc Câu 2c)
        var r = Registration.Create(memberId, eventId);
        r.Cancel();

        Assert.Throws<InvalidOperationException>(() => r.Confirm());
        Assert.Equal(RegistrationStatus.Cancelled, r.Status);
    }

    [Fact]
    public void Cancel_WhenAlreadyCancelled_ThrowsInvalidOperationException()
    {
        // Arrange
        var r = Registration.Create(Guid.NewGuid(), Guid.NewGuid());
        r.Cancel();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => r.Cancel());
        Assert.Equal(RegistrationStatus.Cancelled, r.Status);
    }
}
