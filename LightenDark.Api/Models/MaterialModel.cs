using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    /// <summary>
    /// Model materialu
    /// </summary>
    public class MaterialModel
    {
        /// <summary>
        /// Identifikator konkretni instance
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        /// <summary>
        /// Počet
        /// </summary>
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        /// <summary>
        ///  ID materialu z vyctu materialu
        /// </summary>
        [JsonProperty(PropertyName = "cbMaterialId")]
        public int MaterialTypeID { get; set; }

        /// <summary>
        /// Název
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Popis
        /// </summary>
        [JsonProperty(PropertyName = "desc")]
        public string Description { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", ID, DisplayName);
        }
    }
}
