using LightenDark.Api.CodeBooks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Odpoved na pozadavek na kodebooky - obsahuje sezamy vsech kategorii, plny je
    /// ale vzdy jen jeden - 1 kategorie = 1 request
    /// </summary>
    public class ResponseCodeBook : ResponseBase
    {
        /// <summary>
        ///  Id postavy
        /// </summary>
        [JsonProperty(PropertyName = "characterId")]
        public int CharacterId { get; set; }

        /// <summary>
        /// Zbraně
        /// </summary>
        [JsonProperty(PropertyName = "weapons")]
        public List<WeaponCodeBook> Weapons { get; set; }

        /// <summary>
        /// Brneni
        /// </summary>
        [JsonProperty(PropertyName = "armors")]
        public List<ArmorCodeBook> Armors { get; set; }

        /// <summary>
        /// Sperky
        /// </summary>
        [JsonProperty(PropertyName = "jewels")]
        public List<JewelCodeBook> Jewels { get; set; }

        /// <summary>
        /// Materialy
        /// </summary>
        [JsonProperty(PropertyName = "materials")]
        public List<MaterialCodeBook> Materials { get; set; }

        /// <summary>
        /// Moby
        /// </summary>
        [JsonProperty(PropertyName = "monsters")]
        public List<MonsterFamilyCodeBook> Monsters { get; set; }
    }
}
