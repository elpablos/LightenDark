using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Kdyz byl poskozen item
    /// </summary>
    public class ResponseItemDamaged : ResponseBase
    {
        /// <summary>
        /// ID itemu
        /// </summary>
        [JsonProperty(PropertyName = "itemId")]
        public int ID { get; set; }

        /// <summary>
        /// Typ
        /// 1 - Zbran
        /// 2 - Armor
        /// 3 - Jewel
        /// </summary>
        [JsonProperty(PropertyName = "itemType")]
        public byte itemType { get; set; }

        /// <summary>
        /// Nova durabilita
        /// </summary>
        [JsonProperty(PropertyName = "newDur")]
        public int Durability { get; set; }

        /// <summary>
        /// Max durabilita - meni se pri opravovani
        /// </summary>
        [JsonProperty(PropertyName = "mdur")]
        public int MaximumDurability { get; set; }

        /// <summary>
        /// Zda se opravovalo
        /// </summary>
        [JsonProperty(PropertyName = "r")]
        public bool IsRepair { get; set; }
    }
}
