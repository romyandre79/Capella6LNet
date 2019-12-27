using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Capella6LNet
{
  [JsonObject(MemberSerialization.OptIn)]
  public class MenuAccessListResponse : BaseResponse
  {
    [JsonProperty]
    public List<MenuAccess> Rows { get; set; }
  }
}
