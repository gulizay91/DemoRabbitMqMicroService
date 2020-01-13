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

namespace ClientUI.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class ServiceBusController : ControllerBase
  {
    IEndpointInstance _endpointInstance;
    static int messagesSent;

    public ServiceBusController(IEndpointInstance endpointInstance)
    {
      _endpointInstance = endpointInstance;
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
  }
}