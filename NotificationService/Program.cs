using Microsoft.EntityFrameworkCore;
using NotificationService;
using NotificationService.Abstract;
using NotificationService.Database;
using NotificationService.IoC;
using NotificationService.Services;
using Quartz;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScheduler();
builder.Services.AddFirebase(builder.Configuration);
builder.Services.AddSingleton<INotifierService, NotifierService>();
builder.Services.AddDbContext<DatabaseContext>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();
    context.Database.Migrate();
}

var notifierService = app.Services.GetRequiredService<INotifierService>();
await notifierService.Start();

app.Run();