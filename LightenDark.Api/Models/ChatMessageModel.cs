using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    /// <summary>
    /// Zprávy do chatu
    /// </summary>
    public class ChatMessageModel
    {
        /// <summary>
        /// Zpráva
        /// </summary>
        [JsonProperty(PropertyName = "content")]
        public string Message { get; set; }

        /// <summary>
        /// Typ zprávy
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public int Type { get; set; }

        /// <summary>
        /// Jmeno autora - pokud je whisper a postava neni na klientu nalezena
        /// </summary>
        [JsonProperty(PropertyName = "au")]
        public string Author { get; set; }
    }
}
