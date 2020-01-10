using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract
{
  public class Command : BaseMessage, ICommand
  {
    public string SendMessage { get; set; }
  }
}
