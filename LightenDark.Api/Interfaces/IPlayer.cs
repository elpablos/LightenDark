using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Interfaces
{
    public interface IPlayer
    {
        int ID { get; }

        string DisplayName { get; }

        void MoveDown();

        void MoveUp();

        void MoveLeft();

        void MoveRight();
    }
}
