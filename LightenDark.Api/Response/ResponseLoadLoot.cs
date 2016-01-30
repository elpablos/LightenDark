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
    /// Odpoved na pozadavek na load lootu
    /// </summary>
    public class ResponseLoadLoot : ResponseBase
    {
        /// <summary>
        /// Id postavy
        /// </summary>
        [JsonProperty(PropertyName = "characterId")]
        public int CharacterId { get; set; }

        /// <summary>
        /// TO inventare - je to sice banka, ale to TOcko se na to hodi
        /// </summary>
        [JsonProperty(PropertyName = "inventory")]
        public InventoryModel Inventory { get; set; }

        /// <summary>
        /// Flag privatniho lootu
        /// </summary>
        [JsonProperty(PropertyName = "privLoot")]
        public bool PrivateLoot { get; set; }
    }
}
