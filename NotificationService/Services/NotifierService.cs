using NotificationService.Abstract;
using Quartz;

namespace NotificationService.Services;

public class NotifierService : INotifierService
{
    private readonly ISchedulerService _scheduler;
    private readonly IConfiguration _configuration;

    public NotifierService(ISchedulerService scheduler, IConfiguration configuration)
    {
        _scheduler = scheduler;
        _configuration = configuration;
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

        var intervalStr = _configuration["IntervalInMinutes"];

        if (intervalStr == null)
            throw new InvalidOperationException("Configuration doesn't contain 'IntervalInMinutes'");

        if (!int.TryParse(intervalStr, out int interval))
        {
            throw new InvalidCastException("'IntervalInMinutes' parameter has invalid value");
        }
        
        // Trigger the job to run now, and then repeat every 10 seconds
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity(triggerName, group)
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInMinutes(interval)
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