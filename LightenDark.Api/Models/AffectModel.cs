using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    /// <summary>
    /// Objekt afektu - pro prenos po siti
    /// </summary>
    public class AffectModel
    {
        /// <summary>
        /// Typ afektu
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public short Type { get; set; }

        /// <summary>
        /// Zbyvajici cas
        /// </summary>
        [JsonProperty(PropertyName = "rem")]
        public short RemainTime { get; set; }

        /// <summary>
        /// Level afektu
        /// </summary>
        [JsonProperty(PropertyName = "lvl")]
        public short lvl { get; set; }

    }
}
