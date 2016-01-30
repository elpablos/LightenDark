using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Chat s NPC
    /// </summary>
    public class ResponseNpcChat : ResponseBase
    {
        /// <summary>
        ///  Odpovedi NPCcka
        /// </summary>
        [JsonProperty(PropertyName = "chatData")]
        public List<string> ChatData { get; set; }
    }
}
