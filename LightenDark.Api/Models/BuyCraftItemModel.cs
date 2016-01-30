using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    /// <summary>
    /// Objekt s id materialu, poctem materialu, id a typ produktu pro nez je ho potreba
    /// TODO UPRAVIT! toto je spatne!
    /// </summary>
    public class BuyCraftItemModel
    {
        /// <summary>
        /// ID v databazi
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        /// <summary>
        /// ID produktu
        /// </summary>
        [JsonProperty(PropertyName = "pid")]
        private int PID { get; set; }

        /// <summary>
        /// Typ produktu
        /// 1 - zbran
        /// 2 - zbroj
        /// 3 - jewel
        /// 4 - material(nejaky pokrocilejsi)
        /// </summary>
        [JsonProperty(PropertyName = "ptp")]
        private int ProductType { get; set; }

        /// <summary>
        /// ID materialu
        /// </summary>
        [JsonProperty(PropertyName = "matId")]
        private int MaterialID { get; set; }

        /// <summary>
        /// pocet materialu
        /// </summary>
        [JsonProperty(PropertyName = "matCnt")]
        private int MaterialCount { get; set; }

        /// <summary>
        /// Název
        /// </summary>
        [JsonProperty(PropertyName = "matNm")]
        private string DisplayName { get; set; }
    }
}
