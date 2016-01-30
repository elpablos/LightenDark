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
    /// Odpoved na pozadavek na seznam pro nakupovani
    /// </summary>
    public class ResponseBuyList : ResponseBase
    {
        /// <summary>
        /// Typ seznamu
        /// </summary>
        [JsonProperty(PropertyName = "listType")]
        public int ListType { get; set; }

        /// <summary>
        /// BuyList - zbrane
        /// </summary>
        [JsonProperty(PropertyName = "weaponList")]
        public List<BuyCraftItemModel> WeaponList { get; set; }

        /// <summary>
        /// BuyList - armor
        /// </summary>
        [JsonProperty(PropertyName = "armorList")]
        public List<BuyCraftItemModel> ArmorList { get; set; }

        /// <summary>
        /// BuyList - material
        /// </summary>
        [JsonProperty(PropertyName = "materialList")]
        public List<BuyCraftItemModel> MaterialList { get; set; }

        /// <summary>
        /// BuyList - sperky
        /// </summary>
        [JsonProperty(PropertyName = "jewelList")]
        public List<BuyCraftItemModel> JewelList { get; set; }
    }
}
