using System.Collections.Specialized;
using Quartz;

namespace NotificationService.IoC;

public static class SchedulerInstaller
{
    public static IServiceCollection AddScheduler(this IServiceCollection serviceCollection)
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
                x.UseSqlServer("my connection string");
                // this requires Quartz.Serialization.Json NuGet package
                x.UseJsonSerializer();
            })
            // job initialization plugin handles our xml reading, without it defaults are used
            // requires Quartz.Plugins NuGet package
            .UseXmlSchedulingConfiguration(x =>
            {
                x.Files = new[] { "~/quartz_jobs.xml" };
                // this is the default
                x.FailOnFileNotFound = true;
                // this is not the default
                x.FailOnSchedulingError = true;
            });
        
        var scheduler = builder.BuildScheduler().Result;

        scheduler.Start().Wait();

        serviceCollection.AddSingleton(scheduler);
        return serviceCollection;
    }
}