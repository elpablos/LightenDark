using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Response na pozadavek na hunting
    /// </summary>
    public class ResponseHunting : ResponseBase
    {
        /// <summary>
        /// Utocnik
        /// </summary>
        [JsonProperty(PropertyName = "att")]
        public int Attacker { get; set; }

        /// <summary>
        /// Cil
        /// </summary>
        [JsonProperty(PropertyName = "tgt")]
        public int Target { get; set; }
    }
}
