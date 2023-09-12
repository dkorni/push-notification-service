using Quartz;
using Quartz.Spi;

namespace NotificationService;

public class JobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;
    public JobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public IJob NewJob(TriggerFiredBundle bundle, 
        IScheduler scheduler)
    {
        var job = _serviceProvider.GetService(bundle.JobDetail.JobType) as IJob;

        if (job == null)
        {
            throw new InvalidOperationException($"Job of type {bundle.JobDetail.JobType} is not resolved");
        }
        
        return job;
    }
    public void ReturnJob(IJob job)
    {
        var disposable = job as IDisposable;
        disposable?.Dispose();
    }
}