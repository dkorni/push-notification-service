using System.Collections.Specialized;
using NotificationService.Abstract;
using NotificationService.Services;
using Quartz;
using Quartz.Spi;

namespace NotificationService.IoC;

public static class SchedulerInstaller
{
    public static IServiceCollection AddScheduler(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IJobFactory, JobFactory>();
        serviceCollection.AddSingleton<ISchedulerService, SchedulerService>();
        serviceCollection.AddSingleton<SendRandomMessageJob>();

        return serviceCollection;
    }
}