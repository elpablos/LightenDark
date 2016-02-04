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
    /// Ciselnik typu brneni
    /// </summary>
    public class ArmorCodeBook
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

        #region Specific

        /// <summary>
        /// Faktor zpomaleni brneni - snizuje sanci na vyhnuti se utoku, zhorsuje strelecke schopnosti¨
        /// Na pohyb jako takovy nema vliv
        /// </summary>
        [JsonProperty(PropertyName = "slowdown")]
        public byte Slowdown { get; set; }


        /// <summary>
        /// Obrana - Pierce
        /// </summary>
        [JsonProperty(PropertyName = "pDef")]
        public double PierceDefense { get; set; }

        /// <summary>
        /// Obrana - Slash
        /// </summary>
        [JsonProperty(PropertyName = "sDef")]
        public double SlashDefense { get; set; }

        /// <summary>
        /// Obrana - Blunt
        /// </summary>
        [JsonProperty(PropertyName = "bDef")]
        public double BluntDefense { get; set; }

        /// <summary>
        /// Obrana - Magic
        /// </summary>
        [JsonProperty(PropertyName = "mDef")]
        public double MagicDefense { get; set; }

        /// <summary>
        /// Obrana - Air
        /// </summary>
        [JsonProperty(PropertyName = "aDef")]
        public double AirDefense { get; set; }

        /// <summary>
        /// Obrana - Fire
        /// </summary>
        [JsonProperty(PropertyName = "fDef")]
        public double FireDefense { get; set; }

        /// <summary>
        /// Obrana - Water
        /// </summary>
        [JsonProperty(PropertyName = "wDef")]
        public double WaterDefense { get; set; }

        /// <summary>
        /// Obrana - Earth
        /// </summary>
        [JsonProperty(PropertyName = "eDef")]
        public double EarthDefense { get; set; }


        /// <summary>
        /// Faktor viditelnosti
        /// </summary>
        [JsonProperty(PropertyName = "stealth")]
        public byte Stealth { get; set; }

        /// <summary>
        /// Faktor blokovani magie
        /// </summary>
        [JsonProperty(PropertyName = "magicBlock")]
        public byte MagicBlock { get; set; }

        /// <summary>
        /// Kategorie (kozene, krouzkove ..)
        /// </summary>
        [JsonProperty(PropertyName = "armorCategoryId")]
        public byte ArmorCategoryID { get; set; }

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
