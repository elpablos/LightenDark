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
    /// Augment
    /// </summary>
    public class ResponseAugment : ResponseBase
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
        /// Zda se povedlo
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        /// <summary>
        /// Model zbrane
        /// </summary>
        [JsonProperty(PropertyName = "weaponTO")]
        public WeaponModel Weapon { get; set; }

        /// <summary>
        /// Model armoru
        /// </summary>
        [JsonProperty(PropertyName = "armorTO")]
        public ArmorModel Armor { get; set; }
    }
}
