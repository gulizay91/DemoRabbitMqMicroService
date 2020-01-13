using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract
{
  public class EventMessage : BaseMessage, IEvent
  {
    public string ReceiveMessage { get; set; }
  }
}
