using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract
{
  public class CommandMessage : BaseMessage, ICommand
  {
    public string SendMessage { get; set; }
  }
}
