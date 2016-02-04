using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Zprava s nabidkou co je casove omezena
    /// </summary>
    public class ResponseOffer : ResponseBase
    {
        /// <summary>
        /// Id objektu nabidky
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        /// <summary>
        /// Id postavy co nabizi
        /// </summary>
        [JsonProperty(PropertyName = "srcId")]
        public int CharacterOfferID { get; set; }

        /// <summary>
        ///  Typ nabidky
        ///  1 = Ressurection
        ///  2 = Party
        /// </summary>
        [JsonProperty(PropertyName = "off")]
        public int OfferType { get; set; }

        /// <summary>
        /// Doba trvani nabidky
        /// </summary>
        [JsonProperty(PropertyName = "dur")]
        public int Duration { get; set; }
    }
}
