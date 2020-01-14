using Contract;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Producer
{
  public class PublishMessageHandler : IHandleMessages<CommandMessage>
  {
    static ILog log = LogManager.GetLogger<PublishMessageHandler>();
    static Random random = new Random();

    public Task Handle(CommandMessage message, IMessageHandlerContext context)
    {
      log.Info($"Sending CommandMessage, Id = {message.Id}");

      // This is normally where some business logic would occur

      var sendMessage = new EventMessage
      {
        Id = message.Id,
        PublishMessage = message.SendMessage,
        SendTime = message.SendTime,
        PublisheTime = DateTime.Now
      };

      log.Info($"Publishing EventMessage, Id = {message.Id}");

      return context.Publish(sendMessage);
    }

  }
}
