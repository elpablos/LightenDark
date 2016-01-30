using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Zprava od GM
    /// </summary>
    public class ResponseGmMessage : ResponseBase
    {
        /// <summary>
        /// Dulezitost
        /// </summary>
        [JsonProperty(PropertyName = "severity")]
        public int Severity { get; set; }

        /// <summary>
        /// Zpráva
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
