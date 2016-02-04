using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    public class WeaponModel
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
        /// ID zbrane z vyctu zbrani
        /// </summary>
        [JsonProperty(PropertyName = "cbWeaponId")]
        public int WeaponTypeID { get; set; }

        /// <summary>
        /// Útok - Pierce
        /// </summary>
        [JsonProperty(PropertyName = "patk")]
        public short PierceAttack { get; set; }

        /// <summary>
        /// Útok - Slash
        /// </summary>
        [JsonProperty(PropertyName = "satk")]
        public short SlashAttack { get; set; }

        /// <summary>
        /// Útok - Blunt
        /// </summary>
        [JsonProperty(PropertyName = "batk")]
        public short BluntAttack { get; set; }

        /// <summary>
        /// Útok - Magic
        /// </summary>
        [JsonProperty(PropertyName = "matk")]
        public short MagicAttack { get; set; }

        /// <summary>
        /// Útok - Air
        /// </summary>
        [JsonProperty(PropertyName = "aatk")]
        public double AirAttack { get; set; }

        /// <summary>
        /// Útok - Fire
        /// </summary>
        [JsonProperty(PropertyName = "fatk")]
        public double FireAttack { get; set; }

        /// <summary>
        /// Útok - Water
        /// </summary>
        [JsonProperty(PropertyName = "watk")]
        public double WaterAttack { get; set; }

        /// <summary>
        /// Útok - Earth
        /// </summary>
        [JsonProperty(PropertyName = "eatk")]
        public double EarthAttack { get; set; }

        /// <summary>
        /// Rychlost
        /// </summary>
        [JsonProperty(PropertyName = "speed")]
        public double Speed { get; set; }

        /// <summary>
        /// Obrana
        /// </summary>
        [JsonProperty(PropertyName = "defense")]
        public int Defense { get; set; }

        /// <summary>
        /// Obtížnost
        /// </summary>
        [JsonProperty(PropertyName = "diff")]
        public int Difficulty { get; set; }

        /// <summary>
        /// Váha
        /// </summary>
        [JsonProperty(PropertyName = "weight")]
        public int Weight { get; set; }

        /// <summary>
        /// Kategorie
        /// </summary>
        [JsonProperty(PropertyName = "cat")]
        public int Category { get; set; }

        /// <summary>
        /// range
        /// </summary>
        [JsonProperty(PropertyName = "range")]
        public int Range { get; set; }

        /// <summary>
        /// Kategorie materiálu
        /// </summary>
        [JsonProperty(PropertyName = "matCat")]
        public int MaterialCategory { get; set; }

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
