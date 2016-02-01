using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    public class SpellCastModel
    {
        /// <summary>
        /// Magicke damage
        /// </summary>
        [JsonProperty(PropertyName = "magic")]
        public short MagicDamage { get; set; }

        /// <summary>
        /// Elementalni damage
        /// </summary>
        [JsonProperty(PropertyName = "ele")]
        public short ElementDamage { get; set; }

        /// <summary>
        /// Typ damage elementu F A W E
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public byte Type { get; set; }

        /// <summary>
        /// Flag zda to byl critical
        /// </summary>
        [JsonProperty(PropertyName = "crit")]
        public bool IsCritical { get; set; }

        /// <summary>
        /// Aktualni a maximalni HP cile po damage, mana toho kdo caruje
        /// </summary>
        [JsonProperty(PropertyName = "hp")]
        public int HP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "maxHp")]
        public int MaximumHP { get; set; }

        /// <summary>
        ///  Aktualni HP utocnika po damage
        /// </summary>
        [JsonProperty(PropertyName = "srcHp")]
        public int SourceHP { get; set; }

        /// <summary>
        ///  maximalni HP utocnika po damage
        /// </summary>
        [JsonProperty(PropertyName = "srcMaxHp")]
        public int SourceMaxHP { get; set; }

        /// <summary>
        /// Healnute sve HP
        /// </summary>
        [JsonProperty(PropertyName = "hcst")]
        public short SelfHealHP { get; set; }

        /// <summary>
        /// Healnute cilove HP
        /// </summary>
        [JsonProperty(PropertyName = "htgt")]
        public short TargetHealHP { get; set; }

        /// <summary>
        /// Spell ID
        /// </summary>
        [JsonProperty(PropertyName = "spellId")]
        public short SpellID { get; set; }

        /// <summary>
        // Vysledek
        /// 1- Vycarovano a povedlo
        /// 2- Vycarovano ale nepovedlo
        /// 3- Vubec se necarovalo
        /// </summary>
        [JsonProperty(PropertyName = "cr")]
        public byte CastingState { get; set; }

        /// <summary>
        /// AOE cile postava
        /// </summary>
        [JsonProperty(PropertyName = "cht")]
        public int Character { get; set; }

        /// <summary>
        /// AOE cile mob
        /// </summary>
        [JsonProperty(PropertyName = "mbt")]
        public int Mob { get; set; }

    }
}
