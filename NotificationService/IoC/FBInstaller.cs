using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using NotificationService.Abstract;
using NotificationService.Services;

namespace NotificationService.IoC;

public static class FBInstaller
{
    public static IServiceCollection AddFirebase(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var credentialFile = "cake-crafring-challenge-firebase-adminsdk-4q4e7-fa6f489484.json";

        if (credentialFile == null)
            throw new InvalidOperationException(
                "Environment variables of current machine doesn't contain 'GOOGLE_APPLICATION_CREDENTIALS' variable");
        
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile(credentialFile),
            ProjectId = "cake-crafring-challenge",
        }
        );
        
        serviceCollection.AddSingleton<IPushNotificationSendService, FirebaseSendService>();
        
        return serviceCollection;
    }
}