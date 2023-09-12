using System.Collections.Specialized;
using NotificationService.Abstract;
using Quartz;
using Quartz.Spi;

namespace NotificationService.Services;

public class SchedulerService : ISchedulerService
{
    private IScheduler _scheduler;
    
    public SchedulerService(IJobFactory jobFactory)
    {
        // you can have base properties
        var properties = new NameValueCollection();
        
        // and override values via builder
        var builder = SchedulerBuilder.Create(properties)
            // default max concurrency is 10
            .UseDefaultThreadPool(x => x.MaxConcurrency = 5)
            // this is the default 
            // .WithMisfireThreshold(TimeSpan.FromSeconds(60))
            .UsePersistentStore(x =>
            {
                // force job data map values to be considered as strings
                // prevents nasty surprises if object is accidentally serialized and then 
                // serialization format breaks, defaults to false
                x.UseProperties = true;
                x.UseClustering();
                // there are other SQL providers supported too 
                x.UseMySql("server=db;uid=root;pwd=example;database=mysql");
                // this requires Quartz.Serialization.Json NuGet package
                x.UseJsonSerializer();
            });
        
        _scheduler = builder.BuildScheduler().Result;
        _scheduler.JobFactory = jobFactory;
        _scheduler.Start();
    }
    
    public async Task ScheduleJob(IJobDetail jobDetail, ITrigger trigger)
    {
        if (!await _scheduler.CheckExists(jobDetail.Key))
        {
            // Tell Quartz to schedule the job using our trigger
            await _scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}