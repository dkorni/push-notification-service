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
        "The cake is about to get cold, get your batch 🍰",

        "... dear diary, there are no words to express how many new orders we have and how little time we have in the game...✍",

        "What are we waiting for? We've accumulated enough ingredients, it's time to do the deed! 👨🏼‍🍳",

        "We urgently need your help or we're going to lose this order! ",

        "Complete quests and get rewards 🏆!",

        "Hurry up and collect all the stars while they're still in stock ⭐️⭐️⭐️",

        "⭐️ There are so many cakes, we can't do it without you 🧑‍🍳👩🏻‍🍳👨🏾‍🍳!",

        "It's time to remind myself, I'm the \"Cake crafting challenge\" and I can wait a long time but I don't want to, let's play soon 👀🍰",

        "Our customers are already waiting for their orders, let's hurry up! 🏃🏻‍♂️🏃🏽‍♀️",

        "Just a little more and I can't stand it 😡😡😡😡😡 and eat it all myself!!!",

        "Just a little more and we'll get it all together 🥑🍑🍏🍩 don't quit halfway through"
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