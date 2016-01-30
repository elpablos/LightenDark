using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    ///  Odpoved na request rikajici, ze je worldsave
    /// </summary>
    public class ResponseWorldSave : ResponseBase
    {
        /// <summary>
        /// Stav
        /// 1 = zacal
        /// 2 = skoncil
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public int State { get; set; }
    }
}
