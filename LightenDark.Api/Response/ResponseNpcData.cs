using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Zprava s daty o NPC
    /// </summary>
    public class ResponseNpcData : ResponseBase
    {
        /// <summary>
        /// ID moba
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

        /// <summary>
        ///  Pokud je 1 - znamena to odstranit moba ze seznamu
        /// </summary>
        [JsonProperty(PropertyName = "r")]
        public byte RemoveFromList { get; set; }

        /// <summary>
        ///  Název?
        /// </summary>
        [JsonProperty(PropertyName = "nm")]
        public string DisplayName { get; set; }

        /// <summary>
        ///  Level
        /// </summary>
        [JsonProperty(PropertyName = "lvl")]
        public byte Level { get; set; }

        /// <summary>
        /// HP
        /// </summary>
        [JsonProperty(PropertyName = "tp")]
        public short Type { get; set; }
    }
}
