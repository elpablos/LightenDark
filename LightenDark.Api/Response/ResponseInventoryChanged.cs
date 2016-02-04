using LightenDark.Api.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Zmena dat inventare
    /// </summary>
    public class ResponseInventoryChanged : ResponseBase
    {
        /// <summary>
        /// Přidané zbraně
        /// </summary>
        [JsonProperty(PropertyName = "wadded")]
        public List<WeaponModel> WeaponAdded { get; set; }

        /// <summary>
        /// Odstraněné zbraně
        /// </summary>
        [JsonProperty(PropertyName = "wremoved")]
        public List<WeaponModel> WeaponRemoved { get; set; }

        /// <summary>
        /// Přidaná zbroj
        /// </summary>
        [JsonProperty(PropertyName = "aadded")]
        public List<ArmorModel> ArmorAdded { get; set; }

        /// <summary>
        /// Odebraná zbroj
        /// </summary>
        [JsonProperty(PropertyName = "aremoved")]
        public List<ArmorModel> ArmorRemoved { get; set; }


        /// <summary>
        /// Pridany sperk
        /// </summary>
        [JsonProperty(PropertyName = "jadded")]
        public List<JewelModel> JewelAdded { get; set; }

        /// <summary>
        /// Odebrany sperk
        /// </summary>
        [JsonProperty(PropertyName = "jremoved")]
        public List<JewelModel> JewelRemoved { get; set; }

        /// <summary>
        /// Přidaný material
        /// </summary>
        [JsonProperty(PropertyName = "madded")]
        public List<MaterialModel> MaterialAdded { get; set; }

        /// <summary>
        /// Odstraněný material
        /// </summary>
        [JsonProperty(PropertyName = "mremoved")]
        public List<MaterialModel> MaterialRemoved { get; set; }

        /// <summary>
        /// Změněný material
        /// </summary>
        [JsonProperty(PropertyName = "mchanged")]
        public List<MaterialModel> MaterialChanged { get; set; }

        /// <summary>
        /// Equipset
        /// </summary>
        [JsonProperty(PropertyName = "equipSet")]
        public EquipSetModel EquipSet { get; set; }

        /// <summary>
        /// Data postavy
        /// </summary>
        [JsonProperty(PropertyName = "chrData")]
        public GameCharacterDataModel GameCharacterData { get; set; }

        /// <summary>
        /// Postava
        /// </summary>
        [JsonProperty(PropertyName = "chrTo")]
        public GameCharacterModel GameCharacter { get; set; }

        /// <summary>
        /// Zda byl predmět sebrán
        /// </summary>
        [JsonProperty(PropertyName = "picked")]
        public bool IsPicked { get; set; }

        /// <summary>
        /// Zda byl predmět koupen 
        /// </summary>
        [JsonProperty(PropertyName = "bought")]
        public bool IsBought { get; set; }
    }
}
