using LightenDark.Api.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    public class ResponseModelBase
    {
        [JsonProperty(PropertyName = "t")]
        public virtual ResponseTypes Type { get; set; }
    }
}
