using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    ///  Zprava s daty party
    /// </summary>
    public class ResponsePartyData : ResponseBase
    {
        /// <summary>
        /// Id party (0 = zadna)
        /// </summary>
        [JsonProperty(PropertyName = "partyId")]
        public int PartyID { get; set; }

        /// <summary>
        /// Vudce
        /// </summary>
        [JsonProperty(PropertyName = "leaderId")]
        public int LeaderID { get; set; }

        /// <summary>
        /// Jmena clenu party vcetne ID
        /// </summary>
        [JsonProperty(PropertyName = "memberData")]
        public List<string> memberData { get; set; }
    }
}
