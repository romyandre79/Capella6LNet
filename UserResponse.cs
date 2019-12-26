using System.Collections.Generic;
using Newtonsoft.Json;

namespace Capella6LNet
{
  [JsonObject(MemberSerialization.OptIn)]
  public class UserResponse : BaseResponse
  {
    [JsonProperty]
    public User Rows { get; set; }
  }
}
