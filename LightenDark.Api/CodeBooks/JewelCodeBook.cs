using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.CodeBooks
{
    /// <summary>
    ///  Ciselnik typu jewelu
    /// </summary>
    public class JewelCodeBook
    {
        /// <summary>
        /// ID typu v databazi
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        ///<summary>
        /// Hmotnost jednotky materialu
        /// <summary
        [JsonProperty(PropertyName = "weight")]
        public int Weight { get; set; }

        ///<summary>
        /// Obrazek
        /// <summary
        [JsonProperty(PropertyName = "icon")]
        public byte[] Icon { get; set; }

        ///<summary>
        /// Obrazek jako imageIcon
        ///<summary
        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        #region Specific

        ///<summary>
        /// Magicka odolnost sperku
        ///</summary>
        [JsonProperty(PropertyName = "mDef")]
        public int MagicDefense { get; set; }

        /// <summary>
        /// Pozice
        /// </summary>
        [JsonProperty(PropertyName = "position")]
        public string Position { get; set; }

        ///<summary>
        /// Bonus HP
        ///</summary>
        [JsonProperty(PropertyName = "hpBonus")]
        public int HpBonus { get; set; }

        ///<summary>
        /// Bonus MP
        ///</summary>
        [JsonProperty(PropertyName = "mpBonus")]
        public int MpBonus { get; set; }

        ///<summary>
        /// Magicka odolnost sperku
        ///</summary>
        [JsonProperty(PropertyName = "durability")]
        public int Durability { get; set; }

        #endregion

        ///<summary>
        /// Nazev
        ///</summary>
        [JsonProperty(PropertyName = "name")]
        public string DisplayName { get; set; }

        ///<summary>
        /// Textovy popis
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        ///<summary>
        /// Narocnost na pouziti
        ///</summary>
        [JsonProperty(PropertyName = "diff")]
        public byte Difficulty { get; set; }

        ///<summary>
        /// Zakladni pro craft
        /// </summary>
        [JsonProperty(PropertyName = "craftTime")]
        public int CraftTime { get; set; }
    }
}
