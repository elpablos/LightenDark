using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Zmena HP / MP postavy
    /// </summary>
    public class ResponseCharacterHpMpChanged : ResponseBase
    {
        /// <summary>
        /// ID objektu
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        /// <summary>
        /// HP
        /// </summary>
        [JsonProperty(PropertyName = "hp")]
        public int Hp { get; set; }

        /// <summary>
        /// MP
        /// </summary>
        [JsonProperty(PropertyName = "mp")]
        public int Mp { get; set; }

        /// <summary>
        /// Damage udelene DOTkou
        /// </summary>
        [JsonProperty(PropertyName = "d")]
        public int DOTDamage { get; set; }

        /// <summary>
        /// Law Status
        /// </summary>
        [JsonProperty(PropertyName = "l")]
        public byte LawStatus { get; set; }
    }
}
