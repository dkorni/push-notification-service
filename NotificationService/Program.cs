using NotificationService.IoC;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScheduler();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();