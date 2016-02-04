using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Odpoved na itemy na zemi
    /// Osiřelé předměty
    /// </summary>
    public class ResponseOrphanItems : ResponseBase
    {
        /// <summary>
        ///  Itemy co se maji pridat
        /// </summary>
        [JsonProperty(PropertyName = "addedItems")]
        private List<string> AddedItems { get; set; }

        /// <summary>
        ///  Itemy co se maji odebrat
        /// </summary>
        [JsonProperty(PropertyName = "removedItems")]
        private List<string> RemovedItems { get; set; }
    }
}
