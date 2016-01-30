using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    public class PlayerGraveModel
    {
        /// <summary>
        /// ID 
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        /// <summary>
        /// Zákonný status 
        /// </summary>
        [JsonProperty(PropertyName = "law")]
        public int LawStatus { get; set; }

        /// <summary>
        /// Jmeno postavy
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Pocet itemu
        /// </summary>
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        /// <summary>
        /// Pozice - x
        /// </summary>
        [JsonProperty(PropertyName = "x")]
        public int XPos { get; set; }

        /// <summary>
        /// Pozice - y
        /// </summary>
        [JsonProperty(PropertyName = "y")]
        public int Ypos { get; set; }

        /// <summary>
        /// Party
        /// </summary>
        [JsonProperty(PropertyName = "party")]
        public int Party { get; set; }
    }
}
