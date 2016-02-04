using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Odpoved s nazvem akce co postava vykonava
    /// </summary>
    public class ResponseCharacterAction : ResponseBase
    {
        /// <summary>
        ///  Typ akce co postava vykonava
        /// </summary>
        [JsonProperty(PropertyName = "actId")]
        public int ActionType { get; set; }

        /// <summary>
        ///  ID postavy
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        /// <summary>
        ///  Text zpravy
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// ID efektu co se ma vyvolat
        ///  1 - item gathered
        ///  2 - action completed sucess
        ///  3 - action completed failed
        ///  4 - potion drink
        ///  5 - spell
        /// </summary>
        [JsonProperty(PropertyName = "eff")]
        public int Effect { get; set; }
    }
}
