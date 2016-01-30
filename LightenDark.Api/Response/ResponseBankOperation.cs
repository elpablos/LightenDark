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
    /// Operace v bance vyber/vklad
    /// </summary>
    public class ResponseBankOperation : ResponseBase
    {
        /// <summary>
        /// Vklad zbrane do banky
        /// </summary>
        [JsonProperty(PropertyName = "weaponIn")]
        public WeaponModel WeaponIn { get; set; }

        /// <summary>
        /// Vyber zbrane z banky
        /// </summary>
        [JsonProperty(PropertyName = "weaponOut")]
        public WeaponModel WeaponOut { get; set; }

        /// <summary>
        /// Vklad armoru do banky
        /// </summary>
        [JsonProperty(PropertyName = "armorIn")]
        public ArmorModel ArmorIn { get; set; }

        /// <summary>
        /// Vyber armoru z banky
        /// </summary>
        [JsonProperty(PropertyName = "armorOut")]
        public ArmorModel ArmorOut { get; set; }

        /// <summary>
        /// Vklad sperku do banky
        /// </summary>
        [JsonProperty(PropertyName = "jewelIn")]
        public JewelModel JewelIn { get; set; }

        /// <summary>
        /// Vyber sperku z banky
        /// </summary>
        [JsonProperty(PropertyName = "jewelOut")]
        public JewelModel JewelOut { get; set; }

    }
}
