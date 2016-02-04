using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Odpoved na pozadavek na pohyb
    /// </summary>
    public class ResponseMovement : ResponseBase
    {
        /// <summary>
        /// ID postavy co se hybe
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        /// <summary>
        /// Aktualni souradnice - x
        /// </summary>
        [JsonProperty(PropertyName = "x")]
        public short XPos { get; set; }

        /// <summary>
        /// Aktualni souradnice - y
        /// </summary>
        [JsonProperty(PropertyName = "y")]
        public short YPos { get; set; }
    }
}
