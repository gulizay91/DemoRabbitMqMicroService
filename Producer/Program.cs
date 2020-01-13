using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Producer
{
  class Program
  {
    static async Task Main()
    {
      Console.Title = "ProducerMessage";

      var endpointConfiguration = new EndpointConfiguration("SendMessage.Endpoint");

      //endpointConfiguration.UseTransport<LearningTransport>();
      var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
      transport.UseConventionalRoutingTopology();
      transport.ConnectionString("host=localhost");
      endpointConfiguration.EnableInstallers();

      endpointConfiguration.SendFailedMessagesTo("error");
      endpointConfiguration.AuditProcessedMessagesTo("audit");

      //endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl");
      //var metrics = endpointConfiguration.EnableMetrics();
      //metrics.SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromMilliseconds(500));

      var endpointInstance = await Endpoint.Start(endpointConfiguration)
          .ConfigureAwait(false);

      Console.WriteLine("Press Enter to exit.");
      Console.ReadLine();

      await endpointInstance.Stop()
          .ConfigureAwait(false);
    }
  }
}
