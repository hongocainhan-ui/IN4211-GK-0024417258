using EduHub.Application.Interfaces;
using EduHub.Application.Services;
using EduHub.Domain.Interfaces;
using EduHub.Infrastructure.Notifications;
using EduHub.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Câu 3b & Câu 5d: Đăng ký Dependency Injection
builder.Services.AddSingleton<IEventRepository, InMemoryEventRepository>();
builder.Services.AddSingleton<INotificationServiceFactory, NotificationServiceFactory>();
builder.Services.AddScoped<EventService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "EduHub.API v1");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
