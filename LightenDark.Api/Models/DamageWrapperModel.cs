using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    /// <summary>
    /// Objekt s informacemi o udelene damage
    /// </summary>
    public class DamageWrapperModel
    {

        /// <summary>
        /// Fyzicke damage
        /// </summary>
        [JsonProperty(PropertyName = "ph")]
        public short PhysicalDamage { get; set; }

        /// <summary>
        /// Elementalni damage
        /// </summary>
        [JsonProperty(PropertyName = "ele")]
        public short ElementDamage { get; set; }

        /// <summary>
        /// Typ damage elementu F A W E
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public byte Type { get; set; }

        /// <summary>
        ///  Flag zda to byl critical
        /// </summary>
        [JsonProperty(PropertyName = "crit")]
        public bool IsCritical { get; set; }

        /// <summary>
        /// Aktualni a maximalni HP cile po damage
        /// </summary>
        [JsonProperty(PropertyName = "hp")]
        public int HP { get; set; }

        /// <summary>
        /// Aktualni a maximalni HP utocnika po damage
        /// </summary>
        [JsonProperty(PropertyName = "srcHp")]
        public int SourceHP { get; set; }

        /// <summary>
        /// Vydrainovane HP
        /// </summary>
        [JsonProperty(PropertyName = "dr")]
        public short DrainHP { get; set; }

        /// <summary>
        /// Zda kreslim sip
        /// </summary>
        [JsonProperty(PropertyName = "arr")]
        public int IsArrow { get; set; }

        /// <summary>
        /// Id afektu pokud se utokem nejaky vytvoril
        /// </summary>
        [JsonProperty(PropertyName = "aff")]
        public int Affect { get; set; }
    }
}
