using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    /// <summary>
    /// Objekt postavy ktera se prihlasuje
    /// </summary>
    public class GameCharacterModel
    {
        /// <summary>
        /// Identifikátor
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        /// <summary>
        /// Uzivatel kteremu patri postava
        /// </summary>
        [JsonProperty(PropertyName = "user")]
        public int User { get; set; }

        /// <summary>
        /// Jmeno
        /// </summary>
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Prijmeni
        /// </summary>
        [JsonProperty(PropertyName = "surname")]
        public string SurName { get; set; }

        /// <summary>
        /// ID rasy z databaze
        /// </summary>
        [JsonProperty(PropertyName = "raceData")]
        public byte RaceData { get; set; }

        /// <summary>
        /// ID povolani z databaze
        /// </summary>
        [JsonProperty(PropertyName = "classData")]
        public byte ClassData { get; set; }

        /// <summary>
        /// Aktualni mana
        /// </summary>
        [JsonProperty(PropertyName = "mana")]
        public int Mana { get; set; }

        /// <summary>
        /// Aktualni HP
        /// </summary>
        [JsonProperty(PropertyName = "hitpoints")]
        public int HitPoints { get; set; }

        /// <summary>
        /// Aktualni level
        /// </summary>
        [JsonProperty(PropertyName = "level")]
        public byte Level { get; set; }

        /// <summary>
        /// Aktualni expy
        /// </summary>
        [JsonProperty(PropertyName = "experience")]
        public int Experience { get; set; }

        /// <summary>
        /// ID bank boxu
        /// </summary>
        [JsonProperty(PropertyName = "bankBoxId")]
        public int BankBoxID { get; set; }

        /// <summary>
        /// Stav online/offline/fighting/exhausted
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Síla
        /// </summary>
        [JsonProperty(PropertyName = "str")]
        public short Strength { get; set; }

        /// <summary>
        /// Obratnost
        /// </summary>
        [JsonProperty(PropertyName = "dex")]
        public short Dexterity { get; set; }

        /// <summary>
        ///Odolnost
        /// </summary>
        [JsonProperty(PropertyName = "vit")]
        public short Vitality { get; set; }

        /// <summary>
        /// Síla mysli
        /// </summary>
        [JsonProperty(PropertyName = "will")]
        public short Willpower { get; set; }

        /// <summary>
        /// Inteligence
        /// </summary>
        [JsonProperty(PropertyName = "intel")]
        public short intel { get; set; }

        /// <summary>
        /// Moudrost
        /// </summary>
        [JsonProperty(PropertyName = "wis")]
        public short Wisdom { get; set; }

        /// <summary>
        /// Maximum HP
        /// </summary>
        [JsonProperty(PropertyName = "maxHp")]
        public int MaximumHitPoints { get; set; }

        /// <summary>
        /// Maximum many
        /// </summary>
        [JsonProperty(PropertyName = "maxMp")]
        public int MaximumMana { get; set; }

        /// <summary>
        /// Koordinády X
        /// </summary>
        [JsonProperty(PropertyName = "xpos")]
        public short Xpos { get; set; }

        /// <summary>
        /// Koordinády Y
        /// </summary>
        [JsonProperty(PropertyName = "ypos")]
        public short Ypos { get; set; }

        /// <summary>
        /// Zakonny status
        /// </summary>
        [JsonProperty(PropertyName = "lawStatus")]
        public byte LawStatus { get; set; }

        /// <summary>
        /// Cas pro pohyb
        /// </summary>
        [JsonProperty(PropertyName = "moveTime")]
        public int MoveTime { get; set; }

        /// <summary>
        /// Cas pro autoattack melee
        /// </summary>
        [JsonProperty(PropertyName = "meleeAttTime")]
        public int MeleeAttackTime { get; set; }

        /// <summary>
        /// Cas pro autoattack range
        /// </summary>
        [JsonProperty(PropertyName = "missileAttTime")]
        public int MissileAttackTime { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1} {2}", ID, FirstName, SurName);
        }
    }
}
