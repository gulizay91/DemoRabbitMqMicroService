﻿using System.Dynamic;
using System.Threading;
using System;
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

    [HttpGet]
    public ActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Submit()
    {
      var Id = Guid.NewGuid();
      var message = "message content";
      var command = new CommandMessage { Id = Id, SendTime = DateTime.Now, SendMessage = message };

      // Send the command
      await _endpointInstance.Send(command)
          .ConfigureAwait(false);

      dynamic model = new ExpandoObject();
      model.Id = Id;
      model.Message = message;
      model.MessagesSent = Interlocked.Increment(ref messagesSent);

      return View(model);
    }

    public string Test()
    {
      return "test";
    }

  }
}