using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Odpoved na operaci s questem
    /// </summary>
    public class ResponseNpcQuest : ResponseBase
    {
        /// <summary>
        /// Typ odpovedi
        ///  0 = nic
        ///  1 = OK
        ///  2 = KO
        /// </summary>
        [JsonProperty(PropertyName = "responseType")]
        public int Type { get; set; }

        /// <summary>
        /// Textova zprava ktera se zobrazi
        /// </summary>
        [JsonProperty(PropertyName = "responseMessage")]
        public String Message { get; set; }

        /// <summary>
        /// vycet hotovych questu
        /// </summary>
        [JsonProperty(PropertyName = "characterCompleteQuests")]
        public String CharacterCompleteQuests { get; set; }
    }
}
