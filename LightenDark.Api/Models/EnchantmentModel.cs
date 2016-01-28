using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    public class EnchantmentModel
    {
        /// <summary>
        /// Id postavy
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        /// <summary>
        /// Název
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Sila enchantu
        /// </summary>
        [JsonProperty(PropertyName = "power")]
        public double Power { get; set; }

        /// <summary>
        /// Level enchantu
        /// </summary>
        [JsonProperty(PropertyName = "level")]
        public byte Level { get; set; }

        /// <summary>
        /// Barva
        /// </summary>
        [JsonProperty(PropertyName = "color")]
        public byte Color { get; set; }
    }
}
