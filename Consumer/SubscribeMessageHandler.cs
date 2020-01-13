using Contract;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Common.Models;

namespace Consumer
{
  public class SubscribeMessageHandler : IHandleMessages<EventMessage>
  {
    static ILog log = LogManager.GetLogger<SubscribeMessageHandler>();
    IFirebaseConfig config = new FirebaseConfig
    {
      AuthSecret = "L1zUs0oNlIPmcAxPxg1NhHql2BEIxygTzqH8zoyl",
      BasePath = "https://chat-75632.firebaseio.com/"
    };

    IFirebaseClient client;

    public async Task Handle(EventMessage message, IMessageHandlerContext context)
    {
      log.Info($"SubcribeMessage has received EventMessage, Id = {message.Id}");

      var result = await SaveMessage(message);
      
      //return Task.CompletedTask;
    }

    public async Task<FireBaseDataModel> SaveMessage(EventMessage message)
    {
      client = new FireSharp.FirebaseClient(config);
      FireBaseDataModel result = new FireBaseDataModel();
      if (client == null)
      {
        log.Info($"Firebase RealTime DB connection is not established ");
      }
      else
      {
        var data = new FireBaseDataModel
        {
          Id = message.Id,
          MessageUser = "sender user",
          MessageContent = message.ReceiveMessage,
          MessageDate = message.SendTime
        };

        SetResponse response = await client.SetTaskAsync("Messages/" + data.Id, data);
        result = response.ResultAs<FireBaseDataModel>();
      }

      return result;
    }
  }
}
