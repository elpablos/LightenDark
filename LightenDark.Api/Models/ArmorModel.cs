using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    public class ArmorModel
    {
        /// <summary>
        /// Identifikator konkretni instance
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        /// <summary>
        /// Aktualni durabilita
        /// </summary>
        [JsonProperty(PropertyName = "dur")]
        public int Durability { get; set; }

        /// <summary>
        /// Maximální durabilita
        /// </summary>
        [JsonProperty(PropertyName = "mdur")]
        public int MaximumDurability { get; set; }

        /// <summary>
        ///  Level enchantu (1 - 20)
        /// </summary>
        [JsonProperty(PropertyName = "ench")]
        public byte EnchantLevel { get; set; }

        /// <summary>
        /// Kategorie
        /// </summary>
        [JsonProperty(PropertyName = "cat")]
        public int Category { get; set; }


        /// <summary>
        /// Kategorie materiálu
        /// </summary>
        [JsonProperty(PropertyName = "matCat")]
        public int MaterialCategory { get; set; }

        /// <summary>
        /// ID zbrane z vyctu zbroji
        /// </summary>
        [JsonProperty(PropertyName = "cbArmorId")]
        public int ArmorTypeID { get; set; }

        #region Defence

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

        /// <summary>
        /// Obrana - Air
        /// </summary>
        [JsonProperty(PropertyName = "adef")]
        public double AirDefense { get; set; }

        /// <summary>
        /// Obrana - Fire
        /// </summary>
        [JsonProperty(PropertyName = "fdef")]
        public double FireDefense { get; set; }

        /// <summary>
        /// Obrana - Water
        /// </summary>
        [JsonProperty(PropertyName = "wdef")]
        public double WaterDefense { get; set; }

        /// <summary>
        /// Obrana - Earth
        /// </summary>
        [JsonProperty(PropertyName = "edef")]
        public double EarthDefense { get; set; }

        #endregion

        /// <summary>
        /// Zpomalení
        /// </summary>
        [JsonProperty(PropertyName = "sd")]
        public short SlowDown { get; set; }

        /// <summary>
        /// Zpomalení kouzel
        /// </summary>
        [JsonProperty(PropertyName = "mb")]
        public short CastingPenalityPROBABLY { get; set; }

        /// <summary>
        /// Obtížnost
        /// </summary>
        [JsonProperty(PropertyName = "diff")]
        public short Difficulty { get; set; }

        ///// <summary>
        ///// Váha
        ///// </summary>
        //[JsonProperty(PropertyName = "weight")]
        //public int Weight { get; set; }

        /// <summary>
        /// Název
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Enchanty
        /// </summary>
        [JsonProperty(PropertyName = "enchants")]
        public List<EnchantmentModel> Enchants { get; set; }

        /// <summary>
        /// Remaining Enchant Points
        /// </summary>
        [JsonProperty(PropertyName = "rep")]
        public int RemainEnchantPoints { get; set; }

        /// <summary>
        /// Craft time
        /// </summary>
        [JsonProperty(PropertyName = "ctm")]
        public int CraftingTime { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        [JsonProperty(PropertyName = "prc")]
        public int Price { get; set; }
    }
}
