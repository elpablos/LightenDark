using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Zprava s daty o cizi postave
    /// </summary>
    public class ResponseCharacterData : ResponseBase
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        /// <summary>
        /// Aktualni souradnice - x
        /// </summary>
        [JsonProperty(PropertyName = "x")]
        public short XPos { get; set; }

        /// <summary>
        /// Aktualni souradnice - y
        /// </summary>
        [JsonProperty(PropertyName = "y")]
        public short YPos { get; set; }

        /// <summary>
        /// Rasa postavy
        /// </summary>
        [JsonProperty(PropertyName = "race")]
        public short Race { get; set; }

        /// <summary>
        /// Povolani postavy
        /// </summary>
        [JsonProperty(PropertyName = "gameClass")]
        public short GameClass { get; set; }

        /// <summary>
        /// HP
        /// </summary>
        [JsonProperty(PropertyName = "hp")]
        public short Hp { get; set; }

        /// <summary>
        ///  Maximalni HP
        /// </summary>
        [JsonProperty(PropertyName = "mhp")]
        public short MaximumHp { get; set; }

        /// <summary>
        ///  Pokud je 1 - znamena to odstranit postavu ze seznamu
        /// </summary>
        [JsonProperty(PropertyName = "r")]
        public byte RemoveFromList { get; set; }


        /// <summary>
        ///  Název?
        /// </summary>
        [JsonProperty(PropertyName = "nm")]
        public string DisplayName { get; set; }

        /// <summary>
        ///  Level
        /// </summary>
        [JsonProperty(PropertyName = "lvl")]
        public byte Level { get; set; }

        /// <summary>
        ///  Status zakona
        /// </summary>
        [JsonProperty(PropertyName = "law")]
        public byte Law { get; set; }

        /// <summary>
        ///  Vybaveni - armor - Torso
        /// </summary>
        [JsonProperty(PropertyName = "tor")]
        public string ArmorTorso { get; set; }

        /// <summary>
        ///  Vybaveni - armor - Leg
        /// </summary>
        [JsonProperty(PropertyName = "leg")]
        public string ArmorLegs { get; set; }

        /// <summary>
        ///  Vybaveni - armor - Helm
        /// </summary>
        [JsonProperty(PropertyName = "hel")]
        public string ArmorHelm { get; set; }

        /// <summary>
        ///  Vybaveni - armor - Gloves
        /// </summary>
        [JsonProperty(PropertyName = "glo")]
        public string ArmorGloves { get; set; }

        /// <summary>
        ///  Vybaveni - armor - Boots
        /// </summary>
        [JsonProperty(PropertyName = "boo")]
        public string ArmorBoots { get; set; }

        /// <summary>
        ///  Vybaveni - armor - Arms
        /// </summary>
        [JsonProperty(PropertyName = "arm")]
        public string ArmorArms { get; set; }

        /// <summary>
        ///  Vybaveni - armor - Shield
        /// </summary>
        [JsonProperty(PropertyName = "shld")]
        public string ArmorShield { get; set; }

        /// <summary>
        ///  Vybaveni - zbran - typ
        /// </summary>
        [JsonProperty(PropertyName = "wpnType")]
        public int WeaponType { get; set; }

        /// <summary>
        ///  Vybaveni - zbran - material
        /// </summary>
        [JsonProperty(PropertyName = "wpnMat")]
        public int WeaponMaterial { get; set; }

        /// <summary>
        ///  Vybaveni - zbran - ID
        /// </summary>
        [JsonProperty(PropertyName = "wpnId")]
        public int WeaponID { get; set; }

        /// <summary>
        /// Cloak 
        /// !zajímave json pojmenovani - neck!
        /// </summary>
        [JsonProperty(PropertyName = "neck")]
        public int Cloak { get; set; }
    }
}
