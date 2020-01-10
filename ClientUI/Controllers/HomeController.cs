using System;
using System.Dynamic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Contract;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;

namespace ClientUI.Controllers
{
  public class HomeController : Controller
  {

    IEndpointInstance _endpointInstance;
    static int messagesSent;

    public HomeController(IEndpointInstance endpointInstance)
    {
      _endpointInstance = endpointInstance;
    }

    public IActionResult Index()
    {
      return View();
    }

    //[HttpPost]
    public async Task<IActionResult> Submit(string message = "")
    {
      var Id = Guid.NewGuid();
      message = message ?? "message content";
      var command = new Command { Id = Id, SendTime= DateTime.Now, SendMessage = message };

      // Send the command
      await _endpointInstance.Send(command)
          .ConfigureAwait(false);

      dynamic model = new ExpandoObject();
      model.Id = Id;
      model.Message = message;
      model.MessagesSent = Interlocked.Increment(ref messagesSent);

      return View(model);
    }

  }
}