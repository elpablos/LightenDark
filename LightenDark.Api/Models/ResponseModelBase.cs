using LightenDark.Api.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    public class ResponseModelBase
    {
        [JsonProperty(PropertyName = "type")]
        public ResponseTypes Type { get; set; }
    }
}
