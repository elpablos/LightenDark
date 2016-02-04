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
    ///  Zprava s updatem hrobecku
    /// </summary>
    public class ResponsePlayerGraveChanged : ResponseBase
    {
        /// <summary>
        /// ID moba
        /// </summary>
        [JsonProperty(PropertyName = "changedGrave")]
        public PlayerGraveModel PlayerGrave { get; set; }

        /// <summary>
        /// ID a kod polozky
        /// </summary>
        [JsonProperty(PropertyName = "itemId")]
        public int ItemID { get; set; }

        /// <summary>
        /// kod polozky
        /// </summary>
        [JsonProperty(PropertyName = "itemCode")]
        public int ItemCode { get; set; }
    }
}
