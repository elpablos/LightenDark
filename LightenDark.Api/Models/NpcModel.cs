using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    public class NpcModel
    {
        /// <summary>
        /// ID moba
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Aktualni souradnice - x
        /// </summary>
        public short XPos { get; set; }

        /// <summary>
        /// Aktualni souradnice - y
        /// </summary>
        public short YPos { get; set; }

        /// <summary>
        ///  Název?
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///  Level
        /// </summary>
        public byte Level { get; set; }

        /// <summary>
        /// HP
        /// </summary>
        public short Type { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
