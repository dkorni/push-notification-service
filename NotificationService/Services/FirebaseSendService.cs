using FirebaseAdmin.Messaging;
using NotificationService.Abstract;

namespace NotificationService.Services;

public class FirebaseSendService : IPushNotificationSendService
{
    public async Task<bool> Send(string title, string body, string topic)
    {
        // See documentation on defining a message payload.
        var message = new Message()
        {
            Notification =new Notification()
            {
                Title = "Cake crafting challenge",
                Body = body
            },
            Topic = topic
        };
        
        // Send a message to the device corresponding to the provided
        // registration token.
        string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        return !string.IsNullOrEmpty(response);
    }
}