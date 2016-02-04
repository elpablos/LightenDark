using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Request
{
    /// <summary>
    /// Utok
    /// </summary>
    public class RequestAutoAttack : RequestModelBase
    {
        /// <summary>
        /// ID moba
        /// </summary>
        [JsonProperty(PropertyName = "mobId")]
        public int MobID { get; set; }

        /// <summary>
        /// ID postavy
        /// </summary>
        [JsonProperty(PropertyName = "chrId")]
        public int CharacterID { get; set; }
    }
}
