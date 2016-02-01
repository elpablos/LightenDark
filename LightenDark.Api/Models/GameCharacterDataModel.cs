using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    public class GameCharacterDataModel
    {
        /// <summary>
        /// Id postavy
        /// </summary>
        [JsonProperty(PropertyName = "charId")]
        public int CharacterID { get; set; }

        /// <summary>
        /// Obrana - na blízko
        /// </summary>
        [JsonProperty(PropertyName = "meleeDefense")]
        public short MeleeDefense { get; set; }

        /// <summary>
        /// Obrana - na dálku
        /// </summary>
        [JsonProperty(PropertyName = "missileDefense")]
        public short MissileDefense { get; set; }

        /// <summary>
        /// Obrana - Pierce
        /// </summary>
        [JsonProperty(PropertyName = "pdef")]
        public short PierceDefense { get; set; }

        /// <summary>
        /// Obrana - Slash
        /// </summary>
        [JsonProperty(PropertyName = "sdef")]
        public short SlashDefense { get; set; }

        /// <summary>
        /// Obrana - Blunt
        /// </summary>
        [JsonProperty(PropertyName = "bdef")]
        public short BluntDefense { get; set; }

        /// <summary>
        /// Obrana - Magic
        /// </summary>
        [JsonProperty(PropertyName = "mdef")]
        public short MagicDefense { get; set; }

        /// <summary>
        /// Obrana - Air
        /// </summary>
        [JsonProperty(PropertyName = "adef")]
        public short AirDefense { get; set; }

        /// <summary>
        /// Obrana - Fire
        /// </summary>
        [JsonProperty(PropertyName = "fdef")]
        public short FireDefense { get; set; }

        /// <summary>
        /// Obrana - Water
        /// </summary>
        [JsonProperty(PropertyName = "wdef")]
        public short WaterDefense { get; set; }

        /// <summary>
        /// Obrana - Earth
        /// </summary>
        [JsonProperty(PropertyName = "edef")]
        public short EarthDefense { get; set; }

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
        public short AirAttack { get; set; }

        /// <summary>
        /// Útok - Fire
        /// </summary>
        [JsonProperty(PropertyName = "fatk")]
        public short FireAttack { get; set; }

        /// <summary>
        /// Útok - Water
        /// </summary>
        [JsonProperty(PropertyName = "watk")]
        public short WaterAttack { get; set; }

        /// <summary>
        /// Útok - Earth
        /// </summary>
        [JsonProperty(PropertyName = "eatk")]
        public short EarthAttack { get; set; }

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
        /// IDcka ukoncenych questu oddelene dvojteckou
        /// </summary>
        [JsonProperty(PropertyName = "completeQuests")]
        public string CompleteQuests { get; set; }

        /// <summary>
        /// Celkovy magic block
        /// </summary>
        [JsonProperty(PropertyName = "magicBlock")]
        public short MagicBlock { get; set; }
    }
}
