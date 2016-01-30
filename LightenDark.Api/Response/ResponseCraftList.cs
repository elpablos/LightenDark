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
    ///  Odpoved na pozadavek na seznam pro crafting
    /// </summary>
    public class ResponseCraftList : ResponseBase
    {
        /// <summary>
        /// Typ seznamu
        /// </summary>
        [JsonProperty(PropertyName = "listType")]
        public int ListType { get; set; }

        /// <summary>
        /// Craftlist - zbrane
        /// </summary>
        [JsonProperty(PropertyName = "weaponList")]
        public List<BuyCraftItemModel> WeaponList { get; set; }

        /// <summary>
        /// Craftlist - armor
        /// </summary>
        [JsonProperty(PropertyName = "armorList")]
        public List<BuyCraftItemModel> ArmorList { get; set; }

        /// <summary>
        /// Craftlist - material
        /// </summary>
        [JsonProperty(PropertyName = "materialList")]
        public List<BuyCraftItemModel> MaterialList { get; set; }

    }
}
