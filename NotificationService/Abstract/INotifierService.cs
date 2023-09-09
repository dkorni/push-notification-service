namespace NotificationService.Abstract;

public interface INotifierService
{
    Task Start();

    Task Stop();
}