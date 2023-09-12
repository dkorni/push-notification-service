using NotificationService;
using NotificationService.Abstract;
using NotificationService.IoC;
using NotificationService.Services;
using Quartz;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScheduler();
builder.Services.AddFirebase(builder.Configuration);
builder.Services.AddSingleton<INotifierService, NotifierService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

var notifierService = app.Services.GetRequiredService<INotifierService>();
await notifierService.Start();

app.Run();