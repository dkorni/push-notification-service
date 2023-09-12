using NotificationService.Abstract;
using Quartz;

namespace NotificationService.Services;

public class NotifierService : INotifierService
{
    private readonly ISchedulerService _scheduler;

    public NotifierService(ISchedulerService scheduler)
    {
        _scheduler = scheduler;
    }
    
    public async Task Start()
    {
        var jobName = "SendRandomMessageJob";
        var group = "group1";
        var triggerName = "SendRandomMessageJobTrigger";
        
        // define the job and tie it to our HelloJob class
        IJobDetail job = JobBuilder.Create<SendRandomMessageJob>()
            .WithIdentity(jobName, group)
            .Build();
        
        // Trigger the job to run now, and then repeat every 10 seconds
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity(triggerName, group)
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInMinutes(5)
                .RepeatForever())
            .Build();

        await _scheduler.ScheduleJob(job, trigger);
    }

    public Task Stop()
    {
        // return _scheduler.Shutdown();
        return Task.CompletedTask;
    }
}