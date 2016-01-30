using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Změna informaci o hraci
    /// </summary>
    public class ResponsePlayerInfo : ResponseBase
    {
        /// <summary>
        /// Data o jmenech postav a souradnicich
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public List<String> Data { get; set; }
    }
}
