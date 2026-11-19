using EduHub.Domain.Entities;

namespace EduHub.Tests;

// ===== Câu 2c – Unit test cho Registration (xUnit) =====
// Đề yêu cầu ít nhất 3 test; file này có 6 test (7 trường hợp chạy).
public class RegistrationTests
{
    // Câu 2c – Luật 1: phiếu mới tạo có trạng thái Pending.
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

    // Câu 2b/2c – Create ném ArgumentException khi một trong hai mã là Guid.Empty.
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

    // Câu 2c – Luật 2: Pending -> Confirmed.
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

    // Câu 2c – Luật 3: Confirmed -> Cancelled.
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

    // ===== Câu 2c – TEST BẮT BUỘC =====
    // Create -> Cancel -> Confirm: dòng 3 ném InvalidOperationException và Status vẫn là Cancelled.
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

    // Câu 2c – Luật 3: phiếu đã huỷ không thay đổi được nữa.
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
