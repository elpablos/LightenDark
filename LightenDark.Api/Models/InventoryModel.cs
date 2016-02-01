using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    /// <summary>
    /// Model pro inventar - batoh
    /// </summary>
    public class InventoryModel
    {
        /// <summary>
        /// Id postavy
        /// </summary>
        [JsonProperty(PropertyName = "characterId")]
        public int CharacterID { get; set; }

        /// <summary>
        /// Id postavy
        /// </summary>
        [JsonProperty(PropertyName = "inventoryId")]
        public int InventoryID { get; set; }

        /// <summary>
        /// Zbraně
        /// </summary>
        [JsonProperty(PropertyName = "weapons")]
        public List<WeaponModel> Weapons { get; set; }

        /// <summary>
        /// Brnění
        /// </summary>
        [JsonProperty(PropertyName = "armors")]
        public List<ArmorModel> Armors { get; set; }

        /// <summary>
        /// Šperky
        /// </summary>
        [JsonProperty(PropertyName = "jewels")]
        public List<JewelModel> Jewels { get; set; }

        /// <summary>
        /// Materiál
        /// </summary>
        [JsonProperty(PropertyName = "materials")]
        public List<MaterialModel> Materials { get; set; }
    }
}
