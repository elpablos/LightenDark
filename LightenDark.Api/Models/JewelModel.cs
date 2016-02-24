using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    public class JewelModel
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
        /// ID jewelu z codebooku
        /// </summary>
        [JsonProperty(PropertyName = "cbJewelId")]
        public int ArmorTypeID { get; set; }

        #region Jewel

        /// <summary>
        /// Hp
        /// </summary>
        [JsonProperty(PropertyName = "hp")]
        public double HitPercent { get; set; }

        /// <summary>
        /// Mp
        /// </summary>
        [JsonProperty(PropertyName = "mp")]
        public double ManaPercent { get; set; }

        /// <summary>
        /// Md
        /// </summary>
        [JsonProperty(PropertyName = "md")]
        public double Md { get; set; }

        #endregion

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

        public override string ToString()
        {
            return string.Format("{0}: {1}", ID, DisplayName);
        }
    }
}
