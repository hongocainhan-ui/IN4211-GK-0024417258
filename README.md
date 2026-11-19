# EduHub – Bài kiểm tra giữa học phần IN4211 (Đề số 02)

- **Học phần**: Phát triển phần mềm hướng đối tượng (`IN4211`)
- **Họ và tên**: Hồ Ngọc Ái Nhân
- **MSSV**: `0024417258`
- **Nhánh làm bài**: `midterm/0024417258`

EduHub là Web API (.NET 10) quản lý sự kiện của câu lạc bộ, tổ chức theo Clean Architecture. Câu trả lời lý thuyết nằm trong [`TRA-LOI.md`](TRA-LOI.md). Trong code, mỗi phần trả lời đều có ghi chú `// Câu ...` ở phía trên.

## Cấu trúc solution

| Project | Tầng | Nội dung chính |
|---|---|---|
| `EduHub.Domain` | Domain (lõi) | `BaseEntity`, `Member`, `Registration`, `RegistrationStatus`, `Event`, `IEventRepository` |
| `EduHub.Application` | Application | `EventService`, `INotificationService`, `INotificationServiceFactory` |
| `EduHub.Infrastructure` | Infrastructure | `InMemoryEventRepository`, `EmailNotificationService`, `SmsNotificationService`, `NotificationServiceFactory` |
| `EduHub.API` | Presentation | `EventsController`, `Program.cs` (đăng ký DI), Swagger |
| `EduHub.Tests` | Kiểm thử | xUnit + Moq: `RegistrationTests`, `EventServiceTests`, `DomainTests` |

Chiều tham chiếu: `API → Application, Infrastructure` · `Infrastructure → Application, Domain` · `Application → Domain` · `Domain` không tham chiếu project nào.

## Cách chạy

Yêu cầu: .NET SDK 10.

```bash
dotnet build
dotnet test
dotnet run --project EduHub.API --launch-profile http
```

Mở Swagger tại <http://localhost:5230/swagger>.

## API sự kiện

| Chức năng | Phương thức và route | Thành công | Lỗi |
|---|---|---|---|
| Lấy danh sách sự kiện của CLB | `GET /api/clubs/{clubId}/events` | `200 OK` | `400 Bad Request` |
| Xem một sự kiện theo Id | `GET /api/events/{id}` | `200 OK` | `404 Not Found` |
| Tạo sự kiện mới cho CLB | `POST /api/clubs/{clubId}/events` | `201 Created` | `400 Bad Request` |
| Xoá một sự kiện | `DELETE /api/events/{id}` | `204 No Content` | `404 Not Found` |

Ví dụ body cho `POST`:

```json
{ "title": "Workshop OOP", "date": "2026-12-01T09:00:00Z", "capacity": 50, "channel": "email" }
```

## Vị trí bài làm theo từng câu

| Câu | Nơi trả lời |
|---|---|
| 1a | `TRA-LOI.md` |
| 1b | `TRA-LOI.md`, `EduHub.Domain/Entities/Member.cs` |
| 2a | `TRA-LOI.md` (sơ đồ Mermaid) |
| 2b | `EduHub.Domain/Entities/RegistrationStatus.cs`, `Registration.cs` |
| 2c | `EduHub.Tests/RegistrationTests.cs` |
| 3a | `TRA-LOI.md` |
| 3b | `Event.cs`, `IEventRepository.cs`, `InMemoryEventRepository.cs`, `INotificationService.cs`, `NotificationServiceFactory.cs`, `EventService.cs`, `Program.cs` |
| 3c | `EduHub.Tests/EventServiceTests.cs` |
| 4a | Lịch sử commit trên nhánh `midterm/0024417258` và Pull Request vào `main` |
| 4b | `TRA-LOI.md` |
| 5a, 5b | `TRA-LOI.md`, các tệp `.csproj` |
| 5c | `EduHub.API/Controllers/EventsController.cs`, ảnh trong `MinhChung/` |
| 5d | `TRA-LOI.md`, `EduHub.API/Program.cs` |

## Minh chứng chạy Swagger

| Lần gọi | Ảnh |
|---|---|
| Thành công: `POST` trả `201 Created` | [`MinhChung/thanh-cong-post-201.jpg`](MinhChung/thanh-cong-post-201.jpg) |
| Lỗi: `GET` trả `404 Not Found` | [`MinhChung/loi-get-404.png`](MinhChung/loi-get-404.png) |
