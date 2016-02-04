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
    /// Response na pozadavek na utok
    /// </summary>
    public class ResponseAttack : ResponseBase
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
        ///  Zda je to melee nebo missile
        /// </summary>
        [JsonProperty(PropertyName = "msl")]
        public bool IsMissile { get; set; }

        /// <summary>
        /// Damage
        /// </summary>
        [JsonProperty(PropertyName = "dmg")]
        public DamageWrapperModel Damage { get; set; }
    }
}
