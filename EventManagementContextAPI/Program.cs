using NServiceBus;

namespace EventManagementContextAPI;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseNServiceBus(context =>
            {
                var endpointConfiguration = new EndpointConfiguration("EventManagement");
                var transport = endpointConfiguration.UseTransport<LearningTransport>();
                endpointConfiguration.PurgeOnStartup(true);

                return endpointConfiguration;
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
