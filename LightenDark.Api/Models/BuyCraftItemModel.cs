using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    /// <summary>
    /// Objekt s id materialu, poctem materialu, id a typ produktu pro nez je ho potreba
    /// </summary>
    public class BuyCraftItemModel
    {
        /// <summary>
        /// Kod itemu
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        /// <summary>
        /// Název
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        [JsonProperty(PropertyName = "price")]
        public int Price { get; set; }

        /// <summary>
        /// Obtížnost
        /// </summary>
        [JsonProperty(PropertyName = "diff")]
        public short Difficulty { get; set; }

        /// <summary>
        /// ? kod itemu ?
        /// </summary>
        [JsonProperty(PropertyName = "ic")]
        public int ItemCode { get; set; }

        /// <summary>
        /// ID ciselniku
        /// </summary>
        [JsonProperty(PropertyName = "cbId")]
        public int CodeBookID { get; set; }

        /// <summary>
        /// Craft time
        /// </summary>
        [JsonProperty(PropertyName = "ctm")]
        public int CraftingTime { get; set; }

        #region Attack

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

        #endregion

        #region Defense

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
        /// Obrana
        /// </summary>
        [JsonProperty(PropertyName = "defense")]
        public int Defense { get; set; }

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


    }
}
