using LightenDark.Api.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Request
{
    public abstract class RequestModelBase
    {
        [JsonProperty(PropertyName = "type")]
        public RequestTypes Type { get; set; }
    }
}
