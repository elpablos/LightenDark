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
    /// Odpoved na pozadavek na operaci v trade
    /// </summary>
    public class ResponseTradeOperation : ResponseBase
    {
        /// <summary>
        /// Id postavy
        /// </summary>
        [JsonProperty(PropertyName = "characterId")]
        public int CharacterId { get; set; }

        /// <summary>
        /// Tvé zbrane
        /// </summary>
        [JsonProperty(PropertyName = "youWeapons")]
        public List<WeaponModel> YourWeapons { get; set; }

        /// <summary>
        /// Tvůj armor
        /// </summary>
        [JsonProperty(PropertyName = "youArmors")]
        public List<ArmorModel> YourArmors { get; set; }

        /// <summary>
        /// Tvé šperky
        /// </summary>
        [JsonProperty(PropertyName = "youJewels")]
        public List<JewelModel> YourJewels { get; set; }

        /// <summary>
        /// Tvůj materiál
        /// </summary>
        [JsonProperty(PropertyName = "youMaterials")]
        public List<MaterialModel> YourMaterials { get; set; }

        /// <summary>
        /// Jeho zbraně
        /// </summary>
        [JsonProperty(PropertyName = "hisWeapons")]
        public List<WeaponModel> HisWeapons { get; set; }

        /// <summary>
        /// Jeho armor
        /// </summary>
        [JsonProperty(PropertyName = "hisArmors")]
        public List<ArmorModel> HisArmors { get; set; }

        /// <summary>
        /// Jeho šperky
        /// </summary>
        [JsonProperty(PropertyName = "hisJewels")]
        public List<JewelModel> HisJewels { get; set; }

        /// <summary>
        /// Jeho materiál
        /// </summary>
        [JsonProperty(PropertyName = "hisMaterials")]
        public List<MaterialModel> HisMaterials { get; set; }
    }
}
