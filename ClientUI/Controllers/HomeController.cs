using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Contract;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ClientUI.Controllers
{
  public class HomeController : Controller
  {
    
    private readonly IRequestClient<Command, Event> _requestClient;

    public HomeController(IRequestClient<Command, Event> requestClient)
    {
      _requestClient = requestClient;
    }

    public IActionResult Index()
    {
      return View();
    }

    //[HttpPut("{id}")]
    public async Task<IActionResult> Submit()//(string id, CancellationToken cancellationToken)
    {
      try
      {
        //Event result = await _requestClient.Request(new { SendMessage = id.ToString() }, cancellationToken);
        Event result = await _requestClient.Request(new { SendMessage = "123" });
        return Accepted(result.ReceiveMessage);
      }
      catch (RequestTimeoutException exception)
      {
        return StatusCode((int)HttpStatusCode.RequestTimeout);
      }
    }

  }
}