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
    /// Odesle GMku data o vsech spawnech
    /// </summary>
    public class ResponseGmAllSpawn : ResponseBase
    {
        /// <summary>
        /// Spawny monster
        /// </summary>
        [JsonProperty(PropertyName = "spawns")]
        public List<MonsterSpawnModel> Spawns { get; set; }
    }
}
