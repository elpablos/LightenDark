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
    /// Změna ve skillsetu
    /// </summary>
    public class ResponseSkillSetChanged : ResponseBase
    {
        /// <summary>
        /// SkillSet
        /// </summary>
        [JsonProperty(PropertyName = "skillset")]
        public SkillSetModel skillset { get; set; }

        /// <summary>
        /// Data postavy
        /// </summary>
        [JsonProperty(PropertyName = "chrData")]
        public GameCharacterDataModel chrData { get; set; }

        /// <summary>
        /// Postava
        /// </summary>
        [JsonProperty(PropertyName = "chrTo")]
        public GameCharacterModel chrTo { get; set; }

    }
}
