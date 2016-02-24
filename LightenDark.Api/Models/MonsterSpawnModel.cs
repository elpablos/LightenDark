using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    public class MonsterSpawnModel
    {
        /// <summary>
        /// Identifikator
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        // Umisteni
        [JsonProperty(PropertyName = "x")]
        public int Xpos { get; set; }

        [JsonProperty(PropertyName = "y")]
        public int Ypos { get; set; }

        /// <summary>
        /// Rodina potvor ID
        /// </summary>
        [JsonProperty(PropertyName = "mfId")]
        public int MonsterFamilyID { get; set; }

        /// <summary>
        /// Maximalni pocet naspawnovanych mobu
        /// </summary>
        [JsonProperty(PropertyName = "max")]
        public short Maximum { get; set; }

        /// <summary>
        /// Sila spawnnu - kolik mobu za hodinu se narodi
        /// </summary>
        [JsonProperty(PropertyName = "pwr")]
        public short Power  { get; set; }

        /// <summary>
        /// Jak daleko od spawnu se muze potvora narodit
        /// </summary>
        [JsonProperty(PropertyName = "spR")]
        public byte SpawnRange { get; set; }

        /// <summary>
        /// Dosah aggra
        /// </summary>
        [JsonProperty(PropertyName = "aggR")]
        public short AggroRange { get; set; }

        /// <summary>
        /// Dosah wandering pohybu
        /// </summary>
        [JsonProperty(PropertyName = "wandR")]
        public short WanderRange { get; set; }

        /// <summary>
        /// Dosah jak daleko mob pronasleduje   
        /// </summary>
        [JsonProperty(PropertyName = "purR")]
        public short PursueRange { get; set; }

        /// <summary>
        /// Název
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string DisplayName { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", ID, DisplayName);
        }
    }
}
