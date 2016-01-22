using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightenDark.Api.Interfaces
{
    public interface IScript
    {
        ICore Core { get; set; }

        void Start();

        void Stop();
    }
}
