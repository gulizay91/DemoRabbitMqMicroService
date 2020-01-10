using System;
using System.Collections.Generic;
using System.Text;

namespace Contract
{
  public class BaseMessage
  {
    public DateTime SendTime { get; set; }
    public DateTime PublisheTime { get; set; }
    public DateTime ConsumeTime { get; set; }
    public Guid Id { get; set; }
  }
}
