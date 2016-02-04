using LightenDark.Api.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Předek všech odpovědí ze serveru
    /// </summary>
    public class ResponseBase : AsyncCompletedEventArgs
    {
        /// <summary>
        /// Typ odpovědi ze serveru
        /// </summary>
        [JsonProperty(PropertyName = "t")]
        public virtual ResponseTypes ResponseType { get; set; }

        public ResponseBase()
            : base(null, false, null)
        { }

        public ResponseBase(Exception error, bool cancelled, object userState)
            : base(error, cancelled, userState)
        { }
    }
}
