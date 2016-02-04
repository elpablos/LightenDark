using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    public class EquipSetModel
    {
        /// <summary>
        /// Zbran - levá ruka
        /// </summary>
        [JsonProperty(PropertyName = "leftHandW")]
        public WeaponModel WeaponLeftHand { get; set; }

        /// <summary>
        /// Armor - levá ruka
        /// </summary>
        [JsonProperty(PropertyName = "leftHandA")]
        public ArmorModel ArmorLeftHand { get; set; }

        /// <summary>
        /// Zbran - pravá ruka
        /// </summary>
        [JsonProperty(PropertyName = "rightHand")]
        public WeaponModel HandRight { get; set; }

        /// <summary>
        /// Helma
        /// </summary>
        [JsonProperty(PropertyName = "helm")]
        public ArmorModel Helm { get; set; }

        /// <summary>
        /// Ramena
        /// </summary>
        [JsonProperty(PropertyName = "arms")]
        public ArmorModel Arms { get; set; }

        /// <summary>
        /// Rukavice
        /// </summary>
        [JsonProperty(PropertyName = "gloves")]
        public ArmorModel Gloves { get; set; }

        /// <summary>
        /// Tělo
        /// </summary>
        [JsonProperty(PropertyName = "Modelrso")]
        public ArmorModel Torso { get; set; }
        /// <summary>
        /// Nohy
        /// </summary>
        [JsonProperty(PropertyName = "legs")]
        public ArmorModel Legs { get; set; }
        /// <summary>
        /// Boty
        /// </summary>
        [JsonProperty(PropertyName = "boots")]
        public ArmorModel Boots { get; set; }

        /// <summary>
        /// Náhrdelník
        /// </summary>
        [JsonProperty(PropertyName = "neck")]
        public JewelModel Neck { get; set; }

        /// <summary>
        /// Naušnice - levá ruka
        /// </summary>
        [JsonProperty(PropertyName = "leftEar")]
        public JewelModel EarLeft { get; set; }

        /// <summary>
        /// Naušnice - pravá
        /// </summary>
        [JsonProperty(PropertyName = "rightEar")]
        public JewelModel EarRight { get; set; }

        /// <summary>
        /// Zápěstí - levá
        /// </summary>
        [JsonProperty(PropertyName = "leftBrace")]
        public JewelModel BraceLeft { get; set; }

        /// <summary>
        /// Zápěstí - pravá
        /// </summary>
        [JsonProperty(PropertyName = "rightBrace")]
        public JewelModel BraceRight { get; set; }

        /// <summary>
        /// Prsten - levá ruka
        /// </summary>
        [JsonProperty(PropertyName = "leftRing")]
        public JewelModel RingLeftHand { get; set; }

        /// <summary>
        /// Prsten - pravá ruka
        /// </summary>
        [JsonProperty(PropertyName = "rightRing")]
        public JewelModel RingRighHand { get; set; }
    }
}
