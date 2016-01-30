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
    /// Odpoved na chat
    /// </summary>
    public class ResponseChatMessage : ResponseBase
    {
        /// <summary>
        /// Chat
        /// </summary>
        [JsonProperty(PropertyName = "messages")]
        public List<ChatMessageModel> Messages { get; set; }
    }
}
