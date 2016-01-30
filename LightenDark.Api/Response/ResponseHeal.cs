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
    /// Response na healing - lahvi/bandagi/kouzlem
    /// </summary>
    public class ResponseHeal : ResponseBase
    {
        /// <summary>
        /// Charakter zdroj
        /// </summary>
        [JsonProperty(PropertyName = "chrSrc")]
        public int chrSrc { get; set; }

        /// <summary>
        /// Mog target
        /// </summary>
        [JsonProperty(PropertyName = "mobTgt")]
        public int mobTgt { get; set; }

        /// <summary>
        /// Charakter target
        /// </summary>
        [JsonProperty(PropertyName = "chrTgt")]
        public int Charactertarget { get; set; }

        /// <summary>
        /// Zda je to heal bandagi
        /// </summary>
        [JsonProperty(PropertyName = "band")]
        public bool IsBandage { get; set; }

        /// <summary>
        /// Kolik se vylecili
        /// </summary>
        [JsonProperty(PropertyName = "hp")]
        public short Hp { get; set; }

        /// <summary>
        /// HP cile po vyleceni
        /// </summary>
        [JsonProperty(PropertyName = "tgtHp")]
        public int TargetHp { get; set; }

        /// <summary>
        /// Bandage co mam v baglu - pro update
        /// </summary>
        [JsonProperty(PropertyName = "bandages")]
        public List<MaterialModel> bandages { get; set; }
    }
}
