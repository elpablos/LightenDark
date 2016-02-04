using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    public class SkillSetModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        /// <summary>
        ///  Vlastnik skillsetu
        /// </summary>
        [JsonProperty(PropertyName = "owner")]
        public GameCharacterModel Owner { get; set; }

        [JsonProperty(PropertyName = "changed")]
        public bool Changed { get; set; }

        [JsonProperty(PropertyName = "ownerId")]
        public int OwnerId { get; set; }

        [JsonProperty(PropertyName = "airMagic")]
        public double AirMagic { get; set; }

        [JsonProperty(PropertyName = "alchemy")]
        public double Alchemy { get; set; }

        [JsonProperty(PropertyName = "archery")]
        public double Archery { get; set; }

        [JsonProperty(PropertyName = "armoredCasting")]
        public double ArmoredCasting { get; set; }

        [JsonProperty(PropertyName = "axeFighting")]
        public double AxeFighting { get; set; }

        [JsonProperty(PropertyName = "blacksmithy")]
        public double Blacksmithy { get; set; }

        [JsonProperty(PropertyName = "camping")]
        public double Camping { get; set; }

        [JsonProperty(PropertyName = "craftsmanship")]
        public double Craftsmanship { get; set; }

        [JsonProperty(PropertyName = "cooking")]
        public double Cooking { get; set; }

        [JsonProperty(PropertyName = "daggerFighting")]
        public double DaggerFighting { get; set; }

        [JsonProperty(PropertyName = "earthMagic")]
        public double EarthMagic { get; set; }

        [JsonProperty(PropertyName = "enchanting")]
        public double Enchanting { get; set; }

        [JsonProperty(PropertyName = "fireMagic")]
        public double FireMagic { get; set; }

        [JsonProperty(PropertyName = "fishing")]
        public double Fishing { get; set; }

        [JsonProperty(PropertyName = "healing")]
        public double Healing { get; set; }

        [JsonProperty(PropertyName = "heavyArmor")]
        public double HeavyArmor { get; set; }

        [JsonProperty(PropertyName = "herbalism")]
        public double Herbalism { get; set; }

        [JsonProperty(PropertyName = "hiding")]
        public double Hiding { get; set; }

        [JsonProperty(PropertyName = "hunting")]
        public double Hunting { get; set; }

        [JsonProperty(PropertyName = "lightArmor")]
        public double LightArmor { get; set; }

        [JsonProperty(PropertyName = "lumberjacking")]
        public double Lumberjacking { get; set; }

        [JsonProperty(PropertyName = "macefighting")]
        public double Macefighting { get; set; }

        [JsonProperty(PropertyName = "magery")]
        public double Magery { get; set; }

        [JsonProperty(PropertyName = "magicResistance")]
        public double MagicResistance { get; set; }

        [JsonProperty(PropertyName = "meditation")]
        public double Meditation { get; set; }

        [JsonProperty(PropertyName = "mediumArmor")]
        public double MediumArmor { get; set; }

        [JsonProperty(PropertyName = "mining")]
        public double Mining { get; set; }

        [JsonProperty(PropertyName = "necromancy")]
        public double Necromancy { get; set; }

        [JsonProperty(PropertyName = "parrying")]
        public double Parrying { get; set; }

        [JsonProperty(PropertyName = "polearms")]
        public double Polearms { get; set; }

        [JsonProperty(PropertyName = "scouting")]
        public double Scouting { get; set; }

        [JsonProperty(PropertyName = "shields")]
        public double Shields { get; set; }

        [JsonProperty(PropertyName = "swordsmanship")]
        public double Swordsmanship { get; set; }

        [JsonProperty(PropertyName = "tactics")]
        public double Tactics { get; set; }

        [JsonProperty(PropertyName = "waterMagic")]
        public double WaterMagic { get; set; }

        [JsonProperty(PropertyName = "wrestling")]
        public double Wrestling { get; set; }

    }
}
