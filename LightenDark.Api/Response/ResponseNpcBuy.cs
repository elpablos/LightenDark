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
    /// Odpoved na pozadavek na nakup itemu od NPC
    /// </summary>
    public class ResponseNpcBuy : ResponseBase
    {
        /// <summary>
        /// Id postavy, ktera zada
        /// </summary>
        [JsonProperty(PropertyName = "characterId")]
        public int CharacterId { get; set; }

        /// <summary>
        /// Kod itemu
        /// </summary>
        [JsonProperty(PropertyName = "inventory")]
        public InventoryModel Inventory { get; set; }

    }
}
