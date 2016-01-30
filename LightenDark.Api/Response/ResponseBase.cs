using LightenDark.Api.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Předek všech odpovědí ze serveru
    /// </summary>
    public class ResponseBase
    {
        /// <summary>
        /// Typ odpovědi ze serveru
        /// </summary>
        [JsonProperty(PropertyName = "t")]
        public virtual ResponseTypes ResponseType { get; set; }
    }
}
