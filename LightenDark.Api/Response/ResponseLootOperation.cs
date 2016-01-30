using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Pozadavek na operaci s lootem
    /// </summary>
    public class ResponseLootOperation : ResponseBase
    {
        /// <summary>
        /// Id postavy
        /// </summary>
        [JsonProperty(PropertyName = "characterId")]
        public int CharacterID { get; set; }

        /// <summary>
        /// Kod itemu
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        /// <summary>
        /// ID u nestackovatelneho itemu
        /// </summary>
        [JsonProperty(PropertyName = "itemId")]
        public int ItemID { get; set; }

        /// <summary>
        /// true - z banky
        /// false - do banky
        /// </summary>
        [JsonProperty(PropertyName = "withdraw")]
        public bool IsWithdraw { get; set; }

        /// <summary>
        /// Pocet co presouvam -1 = vse
        /// </summary>
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        /// <summary>
        /// Vysledek operace
        /// -1 = chyba
        /// 0 = zadny novy item
        /// > 0 = id noveho itemu
        /// </summary>
        [JsonProperty(PropertyName = "result")]
        public int result { get; set; }
    }
}
