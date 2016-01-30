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
    ///  Odpoved na pozadavek na pridani levelu
    /// </summary>
    public class ResponseLevelUp : ResponseBase
    {
        /// <summary>
        /// Postava
        /// </summary>
        [JsonProperty(PropertyName = "gameCharacter")]
        public GameCharacterModel Character { get; set; }

        /// <summary>
        /// Data postavy
        /// </summary>
        [JsonProperty(PropertyName = "dataTo")]
        public GameCharacterDataModel CharacterData { get; set; }

        /// <summary>
        /// Skillset
        /// </summary>
        [JsonProperty(PropertyName = "skillset")]
        public SkillSetModel skillset { get; set; }
    }
}
