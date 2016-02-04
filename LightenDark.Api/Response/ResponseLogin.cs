using LightenDark.Api.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LightenDark.Api.Response
{
    /// <summary>
    /// Odpoved na pozadavek na pridani postavy do hry
    /// </summary>
    public class ResponseLogin : ResponseBase
    {
        /// <summary>
        /// Id postavy
        /// </summary>
        [JsonProperty(PropertyName = "characterId")]
        public int CharacterID { get; set; }

        /// <summary>
        /// Objekt postavy ktera se prihlasuje
        /// </summary>
        [JsonProperty(PropertyName = "gameCharacter")]
        public GameCharacterModel GameCharacter { get; set; }

        /// <summary>
        /// Data o postavě
        /// </summary>
        [JsonProperty(PropertyName = "dataTo")]
        public GameCharacterDataModel GameCharacterData { get; set; }

        /// <summary>
        /// Model inventare
        /// </summary>
        [JsonProperty(PropertyName = "inventory")]
        public InventoryModel Inventory { get; set; }

        /// <summary>
        /// Model equipsetu
        /// </summary>
        [JsonProperty(PropertyName = "equipSet")]
        public EquipSetModel EquipmentSet { get; set; }

        /// <summary>
        /// Skillset
        /// </summary>
        [JsonProperty(PropertyName = "skillset")]
        public SkillSetModel Skillset { get; set; }

        /// <summary>
        /// Koordinády X
        /// Stredovy bod okolo ktereho se posila mapa
        /// </summary>
        [JsonProperty(PropertyName = "serverTime")]
        public long ServerTime { get; set; }

        /// <summary>
        /// Koordinády X
        /// Stredovy bod okolo ktereho se posila mapa
        /// </summary>
        [JsonProperty(PropertyName = "xpos")]
        public short Xpos { get; set; }

        /// <summary>
        /// Koordinády Y
        /// Stredovy bod okolo ktereho se posila mapa
        /// </summary>
        [JsonProperty(PropertyName = "ypos")]
        public short Ypos { get; set; }

        /// <summary>
        /// Statické objekty
        /// </summary>
        [JsonProperty(PropertyName = "statics")]
        public List<string> Statics { get; set; }

        /// <summary>
        /// Podkladova mapa celeho sveta
        /// </summary>
        [JsonProperty(PropertyName = "worldMap")]
        public int[] WorldMap { get; set; }

        /// <summary>
        /// Zprava pokud se prihlaseni nepovedlo
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        private string Message { get; set; }
    }
}
