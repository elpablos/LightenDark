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
    /// Ciselnik materialu
    /// </summary>
    public class MaterialCodeBook
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

        ///<summary>
        /// Flag zda je item consumable
        ///</summary>
        [JsonProperty(PropertyName = "consumable")]
        public bool IsConsumable { get; set; }

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
