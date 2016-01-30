using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Číselník statických položek?
    /// </summary>
    public class ResponseMapStaticCodeBook : ResponseBase
    {
        /// <summary>
        ///  Seznam statických položek
        /// </summary>
        [JsonProperty(PropertyName = "staticCbs")]
        public List<string> StaticCodeBooks { get; set; }
    }
}
