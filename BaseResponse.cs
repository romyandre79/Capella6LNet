using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Capella6LNet
{
  [JsonObject(MemberSerialization.OptIn)]
  public class BaseResponse
  {
    [JsonProperty]
    public int IsError { get; set; }
    [JsonProperty]
    public string Msg { get; set; }
    [JsonProperty]
    public int Total { get; set; }
  }
}
