using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Zprava s pohybem moba
    /// </summary>
    public class ResponseMobMove : ResponseBase
    {
        /// <summary>
        /// ID moba
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        /// <summary>
        /// Aktualni souradnice a smer x:y:d
        /// </summary>
        [JsonProperty(PropertyName = "p")]
        public string Coord { get; set; }
    }
}
