using LightenDark.Api.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Interfaces
{
    public interface IGame
    {
        /// <summary>
        /// DarkenLight world
        /// </summary>
        IWorld World { get; }

        /// <summary>
        /// DarkenLight player
        /// </summary>
        IPlayer Player { get; }

        /// <summary>
        /// Direct send of JavaScript message 
        /// </summary>
        /// <param name="message"></param>
        [Obsolete]
        void SendJavaScript(string message);

        /// <summary>
        /// Raw messages from server
        /// </summary>
        [Obsolete]
        event EventHandler<GameEventArgs> GameMessage;

        /// <summary>
        /// Write message to console
        /// </summary>
        /// <param name="message"></param>
        void OutputWrite(string message);
    }
}
