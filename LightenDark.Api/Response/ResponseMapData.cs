using LightenDark.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Odpoved na data mapy
    /// </summary>
    public class ResponseMapData : ResponseBase
    {
        /// <summary>
        /// Chat
        /// </summary>
        [JsonProperty(PropertyName = "messages")]
        public List<ChatMessageModel> Messages { get; set; }

        /// <summary>
        /// Koordinády X
        /// Stredovy bod okolo ktereho se posila mapa
        /// </summary>
        [JsonProperty(PropertyName = "xpos")]
        public short Xpos { get; set; }

        /// <summary>
        /// Koordinády Y
        /// Stredovy bod okolo ktereho se posila mapa
        /// </summary>
        [JsonProperty(PropertyName = "ypos")]
        public short Ypos { get; set; }

        /// <summary>
        /// static objekty
        /// </summary>
        [JsonProperty(PropertyName = "statics")]
        public List<string> Statics { get; set; }

        /// <summary>
        /// Itemy na zemi
        /// </summary>
        [JsonProperty(PropertyName = "orphanItems")]
        public List<string> OrphanItems { get; set; }

        /// <summary>
        /// Hrobecky
        /// </summary>
        [JsonProperty(PropertyName = "playerGraves")]
        public List<PlayerGraveModel> PlayerGraves { get; set; }
    }
}
