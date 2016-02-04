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
        private int MaterialTypeID { get; set; }

        /// <summary>
        /// Název
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        private String DisplayName { get; set; }

        /// <summary>
        /// Popis
        /// </summary>
        [JsonProperty(PropertyName = "desc")]
        private String Description { get; set; }
    }
}
