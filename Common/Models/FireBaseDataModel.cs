using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
  public class FireBaseDataModel
  {
    public Guid Id { get; set; }
    public string MessageUser { get; set; }
    public string MessageContent { get; set; }
    public DateTime MessageDate { get; set; }
  }
}
