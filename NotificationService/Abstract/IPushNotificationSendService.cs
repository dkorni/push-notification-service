namespace NotificationService.Abstract;

public interface IPushNotificationSendService
{
    Task<bool> Send(string title, string body, string topic);
}