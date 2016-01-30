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
    ///  Mačtení dat banky
    /// </summary>
    public class ResponseLoadBank : ResponseBase
    {
        /// <summary>
        ///  Id postavy
        /// </summary>
        [JsonProperty(PropertyName = "characterId")]
        public int CharacterId { get; set; }

        /// <summary>
        /// Zbrane
        /// </summary>
        [JsonProperty(PropertyName = "weapons")]
        public List<WeaponModel> Weapons { get; set; }

        /// <summary>
        /// Armor
        /// </summary>
        [JsonProperty(PropertyName = "armors")]
        public List<ArmorModel> Armors { get; set; }

        /// <summary>
        /// Sperky
        /// </summary>
        [JsonProperty(PropertyName = "jewels")]
        public List<JewelModel> Jewels { get; set; }

        /// <summary>
        /// Materialy
        /// </summary>
        [JsonProperty(PropertyName = "materials")]
        public List<MaterialModel> Materials { get; set; }
    }
}
