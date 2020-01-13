using Contract;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
  public class SubscribeMessageHandler : IHandleMessages<EventMessage>
  {
    static ILog log = LogManager.GetLogger<SubscribeMessageHandler>();

    public Task Handle(EventMessage message, IMessageHandlerContext context)
    {
      log.Info($"SubcribeMessage has received EventMessage, Id = {message.Id}");
      return Task.CompletedTask;
    }
  }
}
