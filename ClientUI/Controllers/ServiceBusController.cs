using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Common.Models;
using Newtonsoft.Json;

namespace ClientUI.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class ServiceBusController : ControllerBase
  {
    IEndpointInstance _endpointInstance;
    static int messagesSent;
    IFirebaseConfig config = new FirebaseConfig
    {
      AuthSecret = "L1zUs0oNlIPmcAxPxg1NhHql2BEIxygTzqH8zoyl",
      BasePath = "https://chat-75632.firebaseio.com/"
    };

    IFirebaseClient client;

    public ServiceBusController(IEndpointInstance endpointInstance)
    {
      _endpointInstance = endpointInstance;
      client = new FireSharp.FirebaseClient(config);
    }

    [HttpGet]
    public async Task<object> Submit(string message = "")
    {
      var Id = Guid.NewGuid();
      message = string.IsNullOrEmpty(message) ? "message content" : message;
      var command = new CommandMessage { Id = Id, SendTime = DateTime.Now, SendMessage = message };

      // Send the command
      await _endpointInstance.Send(command)
          .ConfigureAwait(false);

      dynamic model = new ExpandoObject();
      model.Id = Id;
      model.Message = message;
      model.MessagesSent = Interlocked.Increment(ref messagesSent);

      return model;
    }

    [HttpGet]
    public async Task<IEnumerable<FireBaseDataModel>> GetMessages()
    {
      if(client == null)
        client = new FireSharp.FirebaseClient(config);
      List<FireBaseDataModel> resultList = new List<FireBaseDataModel>();

      if (client != null)
      {
        var response = await client.GetTaskAsync("Messages");
        var resultJson = response.Body;
        dynamic dynJson = JsonConvert.DeserializeObject(resultJson);
        foreach (var item in dynJson)
        {
          var nodeItem = item.Value;
          var data = new FireBaseDataModel
          {
            Id = nodeItem.Id,
            MessageContent = nodeItem.MessageContent,
            MessageUser = nodeItem.MessageUser,
            MessageDate = nodeItem.MessageDate
          };

          resultList.Add(data);
        }
      }

      return resultList.OrderByDescending(r => r.MessageDate).Take(10).AsEnumerable();
    }
  }
}
