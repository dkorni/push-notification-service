using Quartz;

namespace NotificationService.Abstract;

public interface ISchedulerService
{
    Task ScheduleJob(IJobDetail jobDetail, ITrigger trigger);
}