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
    /// Odpoved na pozadavek na pridani postavy do hry
    /// </summary>
    public class ResponseGmInit : ResponseBase
    {
        /// <summary>
        /// Spawny monster
        /// </summary>
        [JsonProperty(PropertyName = "spawns")]
        public List<MonsterSpawnModel> Spawns { get; set; }
    }
}
