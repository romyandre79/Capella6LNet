using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Capella6LNet
{
  [JsonObject(MemberSerialization.OptIn)]
  public class LanguageListResponse : BaseResponse
  {
    [JsonProperty]
    public List<Language> Rows { get; set; }
  }
}
