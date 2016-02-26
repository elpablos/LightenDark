using LightenDark.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Interfaces
{
    public interface IWorld
    {
        int[][] WorldMap { get; }

        List<StaticModel> Statics { get; }

        List<NpcModel> Npcs { get; }
    }
}
