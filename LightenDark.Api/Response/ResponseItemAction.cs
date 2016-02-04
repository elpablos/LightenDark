using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Obecna odpoved na pozadavek na akci s predmetem
    /// </summary>
    public class ResponseItemAction : ResponseBase
    {
        /// <summary>
        ///  Id postavy, ktera zada
        /// </summary>
        [JsonProperty(PropertyName = "characterId")]
        public int CharacterId { get; set; }

        /// <summary>
        ///  Ciselnikovy kod predmetu
        /// </summary>
        [JsonProperty(PropertyName = "itemCode")]
        public int ItemCode { get; set; }

        /// <summary>
        ///  ID predmetu
        /// </summary>
        [JsonProperty(PropertyName = "itemId")]
        public int ItemID { get; set; }

        /// <summary>
        /// Cislo akce
        /// 1 - drop
        /// 2 - split
        /// 3 - stack
        /// </summary>
        [JsonProperty(PropertyName = "actionNr")]
        public int ActionNumber { get; set; }

        /// <summary>
        /// Hodnota (u ruznych akci ma ruzny vyznam)
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public int Value { get; set; }

        /// <summary>
        /// Hodnota2 (u ruznych akci ma ruzny vyznam)
        /// </summary>
        [JsonProperty(PropertyName = "value2")]
        public int Value2 { get; set; }
    }
}
