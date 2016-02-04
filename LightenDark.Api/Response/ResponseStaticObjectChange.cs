using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Zmena statickeho objektu
    /// </summary>
    public class ResponseStaticObjectChange : ResponseBase
    {
        /// <summary>
        /// ID objektu
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        /// <summary>
        /// Pozice X
        /// </summary>
        [JsonProperty(PropertyName = "x")]
        public int XPos { get; set; }

        /// <summary>
        /// Pozice Y
        /// </summary>
        [JsonProperty(PropertyName = "y")]
        public int YPos { get; set; }

        /// <summary>
        /// Pokud je typ -1, tak se objekt smaze
        /// </summary>
        [JsonProperty(PropertyName = "staticType")]
        public int StaticType { get; set; }

        /// <summary>
        /// Change = priznak zda se objekt menil(1) nebo pridaval(0)
        /// </summary>
        [JsonProperty(PropertyName = "change")]
        public int Change { get; set; }



    }
}
