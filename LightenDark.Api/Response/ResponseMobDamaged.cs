using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Zprava s poskozenim moba - treba DOTkou
    /// </summary>
    public class ResponseMobDamaged : ResponseBase
    {
        /// <summary>
        /// ID moba
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        /// <summary>
        /// Damage
        /// </summary>
        [JsonProperty(PropertyName = "d")]
        public int Damage { get; set; }

        /// <summary>
        /// Zbyvajici HP
        /// </summary>
        [JsonProperty(PropertyName = "hp")]
        public int Hp { get; set; }
    }
}
