using LightenDark.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Obecna odpoved na pozadavek na akci, obsahuje informativni model akce
    /// </summary>
    public class ResponseCastSpell : ResponseBase
    {
        /// <summary>
        /// Postava - Utocnik
        /// </summary>
        [JsonProperty(PropertyName = "chrAtt")]
        public int CharacterAttack { get; set; }

        /// <summary>
        /// Postava - Cil
        /// </summary>
        [JsonProperty(PropertyName = "chrTgt")]
        public int CharacterTarget { get; set; }

        /// <summary>
        /// Mob - Utocnik
        /// </summary>
        [JsonProperty(PropertyName = "mobAtt")]
        public int mobAttack { get; set; }

        /// <summary>
        /// Mob - Cil
        /// </summary>
        [JsonProperty(PropertyName = "mobTgt")]
        public int MobTarget { get; set; }

        /// <summary>
        /// Id kouzla co jsem zacal carovat
        /// </summary>
        [JsonProperty(PropertyName = "spid")]
        private int SpellID { get; set; }

        /// <summary>
        /// Cas jak dlouho se bude carovat
        /// </summary>
        [JsonProperty(PropertyName = "ctm")]
        private long CastingTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        // Mana toho co caruje
        [JsonProperty(PropertyName = "mp")]
        private int Mana { get; set; }

        /// <summary>
        /// Damage
        /// </summary>
        [JsonProperty(PropertyName = "spells")]
        private List<SpellCastModel> Spells { get; set; }
    }
}
