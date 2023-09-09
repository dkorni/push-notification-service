using NotificationService.Abstract;
using Quartz;

namespace NotificationService.Services;

public class NotifierService : INotifierService
{
    private readonly IScheduler _scheduler;

    public NotifierService(IScheduler scheduler)
    {
        _scheduler = scheduler;
    }
    
    public Task Start()
    {
        // define the job and tie it to our HelloJob class
        IJobDetail job = JobBuilder.Create<HelloJob>()
            .WithIdentity("job1", "group1")
            .Build();

        // Trigger the job to run now, and then repeat every 10 seconds
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("trigger1", "group1")
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInMinutes(5)
                .RepeatForever())
            .Build();

        // Tell Quartz to schedule the job using our trigger
        return _scheduler.ScheduleJob(job, trigger);
    }

    public Task Stop()
    {
        return _scheduler.Shutdown();
    }
}