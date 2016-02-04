using LightenDark.Api.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Args
{
    public class BoundEventArgs
    {
        public BoundType BoundType { get; }

        public string Message { get; }

        public BoundEventArgs(BoundType boundType, string message)
        {
            BoundType = boundType;
            Message = message;
        }
    }
}
