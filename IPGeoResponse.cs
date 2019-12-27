using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Capella6LNet
{
  [JsonObject(MemberSerialization.OptIn)]
  public class IPGeoResponse
  {
    [JsonProperty]
    public string status { get; set; }
    [JsonProperty]
    public string country { get; set; }
    [JsonProperty]
    public string countryCode { get; set; }
    [JsonProperty]
    public string region { get; set; }
    [JsonProperty]
    public string regionName { get; set; }
    [JsonProperty]
    public string city { get; set; }
    [JsonProperty]
    public string zip { get; set; }
    [JsonProperty]
    public double lat { get; set; }
    [JsonProperty]
    public double lon { get; set; }
    [JsonProperty]
    public string timezone { get; set; }
    [JsonProperty]
    public string isp { get; set; }
    [JsonProperty]
    public string org { get; set; }
    [JsonProperty]
    public string @as { get; set; }
    [JsonProperty]
    public string query { get; set; }
  }
}
