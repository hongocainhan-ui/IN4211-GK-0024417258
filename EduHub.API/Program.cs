using EduHub.Application.Interfaces;
using EduHub.Application.Services;
using EduHub.Domain.Interfaces;
using EduHub.Infrastructure.Notifications;
using EduHub.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// ===== Câu 3b – Đăng ký EventService và các phụ thuộc trong Program.cs =====
// Câu 5d – Repository dùng AddSingleton (không dùng AddScoped) để dữ liệu còn giữa các request.
builder.Services.AddSingleton<IEventRepository, InMemoryEventRepository>();
builder.Services.AddSingleton<INotificationServiceFactory, NotificationServiceFactory>();
builder.Services.AddScoped<EventService>();

var app = builder.Build();

// Câu 5c – Swagger UI để chạy thử API, mở tại /swagger.
app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "EduHub.API v1");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
