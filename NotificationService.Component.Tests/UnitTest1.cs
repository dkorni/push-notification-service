using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Abstract;
using NotificationService.IoC;

namespace NotificationService.Component.Tests;

public class Tests
{
    [Test]
    public async Task FirebasePushService_Should_SendNotification()
    {
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();
        
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddFirebase(configuration);
        var provider = serviceCollection.BuildServiceProvider();

        var pushSendService = provider.GetRequiredService<IPushNotificationSendService>();
        var result = await pushSendService.Send("Test", "Test", "all");
        Assert.IsTrue(result);
    }
}