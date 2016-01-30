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
    /// Zmena afektu postavy
    /// </summary>
    public class ResponseAffectChange : ResponseBase
    {
        /// <summary>
        /// Přidané afekty
        /// </summary>
        [JsonProperty(PropertyName = "added")]
        public List<AffectModel> Added { get; set; }

        /// <summary>
        /// Odstraněné afekty
        /// </summary>
        [JsonProperty(PropertyName = "removed")]
        public List<AffectModel> Removed { get; set; }

        /// <summary>
        /// Změněné afekty
        /// </summary>
        [JsonProperty(PropertyName = "changed")]
        public List<AffectModel> Changed { get; set; }
    }
}
