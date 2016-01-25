using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    public class ResponseLogin : ResponseModelBase
    {
        [JsonProperty(PropertyName = "characterId")]
        public int CharacterID { get; set; }
    }
}
