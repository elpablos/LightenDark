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
    /// Odpoved na hrobecek
    /// </summary>
    public class ResponsePlayerGraves : ResponseBase
    {
        /// <summary>
        /// Co se maji pridat
        /// </summary>
        [JsonProperty(PropertyName = "addedGraves")]
        public List<PlayerGraveModel> AddedGraves { get; set; }

        /// <summary>
        ///   Co se maji odebrat
        /// </summary>
        [JsonProperty(PropertyName = "removedGraves")]
        public List<PlayerGraveModel> RemovedGraves { get; set; }

        /// <summary>
        /// Otevreny hrobecek
        /// </summary>
        [JsonProperty(PropertyName = "openedGrave")]
        public PlayerGraveModel OpenedGrave { get; set; }
    }
}
