using LightenDark.Api.Interfaces;

namespace LightenDark.Studio.Core.Impl
{
    public class World : IWorld
    {
        private Game game;

        public World(Game game)
        {
            this.game = game;
        }
    }
}