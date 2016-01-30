using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Odpoved se zpravou
    /// </summary>
    public class ResponseSendMessage : ResponseBase
    {
        /// <summary>
        /// ID postavy ktere poslala
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        /// <summary>
        /// Zprava
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Message { get; set; }

        /// <summary>
        /// ID NPC pokud message rika NPC
        /// </summary>
        [JsonProperty(PropertyName = "npc")]
        public int Npc { get; set; }

        /// <summary>
        /// ID Moba pokud message rika MOB
        /// </summary>
        [JsonProperty(PropertyName = "m")]
        public int Mob { get; set; }

        /// <summary>
        /// Typ zpravy
        /// P = public
        /// W = whisper
        /// A = Party
        /// T = Trade
        /// </summary>
        [JsonProperty(PropertyName = "tp")]
        public string MessageType { get; set; }

        /// <summary>
        /// Jmeno autora - pokud je whisper a postava neni na klientu nalezena
        /// </summary>
        [JsonProperty(PropertyName = "au")]
        public string Author { get; set; }
    }
}
