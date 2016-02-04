using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Zprava s daty o mobovi
    /// </summary>
    public class ResponseMobData : ResponseBase
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
        /// HP
        /// </summary>
        [JsonProperty(PropertyName = "hp")]
        public short Hp { get; set; }

        /// <summary>
        ///  Maximalni HP
        /// </summary>
        [JsonProperty(PropertyName = "mhp")]
        public short MaximumHp { get; set; }

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

        /// <summary>
        /// Summoner
        /// </summary>
        [JsonProperty(PropertyName = "smn")]
        public int Summoner { get; set; }

        /// <summary>
        /// Master
        /// </summary>
        [JsonProperty(PropertyName = "mst")]
        public int Master { get; set; }
    }
}
