namespace EduHub.Domain.Entities;

// ===== Câu 2b – Enum RegistrationStatus =====
// Ba trạng thái của phiếu đăng ký (xem sơ đồ trạng thái ở Câu 2a trong TRA-LOI.md).
public enum RegistrationStatus
{
    Pending,    // Mới tạo, chờ xác nhận
    Confirmed,  // Đã xác nhận
    Cancelled   // Đã huỷ, không đổi được nữa
}
