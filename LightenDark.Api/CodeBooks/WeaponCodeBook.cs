using LightenDark.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.CodeBooks
{
    /// <summary>
    /// Ciselnik typu zbrani
    /// </summary>
    public class WeaponCodeBook
    {
        /// <summary>
        /// ID typu v databazi
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        ///<summary>
        /// Kategorie materialu
        /// <summary
        [JsonProperty(PropertyName = "category")]
        public int Category { get; set; }

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

        #region Attack

        /// <summary>
        /// Útok - Pierce
        /// </summary>
        [JsonProperty(PropertyName = "pAtk")]
        public int PierceAttack { get; set; }

        /// <summary>
        /// Útok - Slash
        /// </summary>
        [JsonProperty(PropertyName = "sAtk")]
        public int SlashAttack { get; set; }

        /// <summary>
        /// Útok - Blunt
        /// </summary>
        [JsonProperty(PropertyName = "bAtk")]
        public int BluntAttack { get; set; }

        /// <summary>
        /// Útok - Magic
        /// </summary>
        [JsonProperty(PropertyName = "mAtk")]
        public int MagicAttack { get; set; }

        /// <summary>
        /// Útok - Air
        /// </summary>
        [JsonProperty(PropertyName = "aAtk")]
        public int AirAttack { get; set; }

        /// <summary>
        /// Útok - Fire
        /// </summary>
        [JsonProperty(PropertyName = "fAtk")]
        public int FireAttack { get; set; }

        /// <summary>
        /// Útok - Water
        /// </summary>
        [JsonProperty(PropertyName = "wAtk")]
        public int WaterAttack { get; set; }

        /// <summary>
        /// Útok - Earth
        /// </summary>
        [JsonProperty(PropertyName = "eAtk")]
        public int EarthAttack { get; set; }

        /// <summary>;
        /// Rychlost
        /// </summary>
        [JsonProperty(PropertyName = "speed")]
        public int Speed { get; set; }

        /// <summary>
        /// Kategorie (mec, palcat, luk ...)
        /// </summary>
        [JsonProperty(PropertyName = "weaponCategoryId")]
        public byte WeaponCategoryId { get; set; }

        ///<summary>
        /// Obranna zbrane
        ///</summary>
        [JsonProperty(PropertyName = "defense")]
        public int Defense { get; set; }

        #endregion

        /// <summary>
        /// Kategorie materialu
        /// </summary>
        [JsonProperty(PropertyName = "materialCat")]
        public byte MaterialCategory { get; set; }

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

        /// <summary>
        /// Normovana cena za kus
        /// </summary>
        [JsonProperty(PropertyName = "price")]
        public int Price { get; set; }

        ///<summary>
        /// Seznam objektu s pozadavky na vyrobu - donacita a uklada se rucne
        /// </summary>
        [JsonProperty(PropertyName = "craftReqs")]
        public List<CraftRequirementModel> CraftRequirements { get; set; }

        ///<summary>
        /// Zakladni pro craft
        /// </summary>
        [JsonProperty(PropertyName = "craftTime")]
        public int CraftTime { get; set; }
    }
}
