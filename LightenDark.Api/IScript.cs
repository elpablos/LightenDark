using LightenDark.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api
{
    /// <summary>
    /// Interface for every script in LightenDark
    /// </summary>
    public interface IScript
    {
        /// <summary>
        /// Name of script
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Whole game
        /// </summary>
        IGame Game { get; set; }

        /// <summary>
        /// Main loop
        /// </summary>
        void Start();

        /// <summary>
        /// Stop the loop
        /// </summary>
        void Stop(bool isForce = false);
    }
}
