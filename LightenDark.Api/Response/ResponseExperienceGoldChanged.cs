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
    /// Zmena Exp/gold - pri zabiti moba
    /// </summary>
    public class ResponseExperienceGoldChanged : ResponseBase
    {
        /// <summary>
        /// Exp
        /// </summary>
        [JsonProperty(PropertyName = "exp")]
        public int exp { get; set; }

        /// <summary>
        /// Model se zlataky pokud se zmenila stavajci hromadka
        /// </summary>
        [JsonProperty(PropertyName = "chg")]
        public MaterialModel ChangeGold { get; set; }

        /// <summary>
        /// Model se zlataky pokud je uplne nova hromadka
        /// </summary>
        [JsonProperty(PropertyName = "ag")]
        public MaterialModel NewGold { get; set; }

        /// <summary>
        /// Pocet zlataku kolik se pridavalo
        /// </summary>
        [JsonProperty(PropertyName = "g")]
        public int AmountGold { get; set; }
    }
}
