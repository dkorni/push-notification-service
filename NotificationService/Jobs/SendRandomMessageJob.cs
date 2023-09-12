using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using NotificationService.Abstract;
using Quartz;

namespace NotificationService;

public class SendRandomMessageJob : IJob
{
    private readonly IPushNotificationSendService _notifierService;

    public SendRandomMessageJob(IPushNotificationSendService notifierService)
    {
        _notifierService = notifierService;
    }
    
    private static string[] messages = new[]
    {
        "Let's make new cakes 🍰!",

        "🎂 New cakes are waiting for you!",

        "Start playing and make the most delicious cake 🥮!",

        "Visitors are waiting for your cakes 🍽🍰!",

        "Complete quests and get rewards 🏆!",

        "How about making a new pie 🍰?",

        "⭐️ Play and open the star chest 🔑!",

        "You have new orders 🍰🍰🍰! Let's make cakes!",

        "Collect ingredients and get new cakes 🎂!",

        "Collect special ingredients and get a bonus 🤑!"
    };
    
    public async Task Execute(IJobExecutionContext context)
    {
        var random = new Random();
        var randomIndex = random.Next(0, messages.Length - 1);
        var randomMessage = messages[randomIndex];
        string messageTitle = "Cake crafting challenge";
        string topic = "all";

        // Send a message to the device corresponding to the provided   
        // registration token.
        await _notifierService.Send(messageTitle, randomMessage, topic);
    }
}