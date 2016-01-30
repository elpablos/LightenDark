using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Enchanty
    /// </summary>
    public class ResponsePowerEnchant : ResponseBase
    {
        /// <summary>
        /// ID itemu
        /// </summary>
        [JsonProperty(PropertyName = "itemId")]
        public int ID { get; set; }

        /// <summary>
        /// Typ
        /// 1 - Zbran
        /// 2 - Armor
        /// 3 - Jewel
        /// </summary>
        [JsonProperty(PropertyName = "itemType")]
        public byte itemType { get; set; }

        /// <summary>
        /// New power
        /// </summary>
        [JsonProperty(PropertyName = "newPower")]
        public int NewPower { get; set; }

        /// <summary>
        /// Název
        /// </summary>
        [JsonProperty(PropertyName = "newName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Útok - Pierce
        /// </summary>
        [JsonProperty(PropertyName = "patk")]
        public int PierceAttack { get; set; }

        /// <summary>
        /// Útok - Slash
        /// </summary>
        [JsonProperty(PropertyName = "satk")]
        public int SlashAttack { get; set; }

        /// <summary>
        /// Útok - Blunt
        /// </summary>
        [JsonProperty(PropertyName = "batk")]
        public int BluntAttack { get; set; }

        /// <summary>
        /// Útok - Magic
        /// </summary>
        [JsonProperty(PropertyName = "matk")]
        public int MagicAttack { get; set; }

        /// <summary>
        /// Obrana - Pierce
        /// </summary>
        [JsonProperty(PropertyName = "pdef")]
        public double PierceDefense { get; set; }

        /// <summary>
        /// Obrana - Slash
        /// </summary>
        [JsonProperty(PropertyName = "sdef")]
        public double SlashDefense { get; set; }

        /// <summary>
        /// Obrana - Blunt
        /// </summary>
        [JsonProperty(PropertyName = "bdef")]
        public double BluntDefense { get; set; }

        /// <summary>
        /// Obrana - Magic
        /// </summary>
        [JsonProperty(PropertyName = "mdef")]
        public double MagicDefense { get; set; }
    }
}
