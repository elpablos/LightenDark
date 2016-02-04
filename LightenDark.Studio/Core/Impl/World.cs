using System.Collections.Generic;
using LightenDark.Api.CodeBooks;
using LightenDark.Api.Interfaces;
using LightenDark.Api.Models;

namespace LightenDark.Studio.Core.Impl
{
    public class World : IWorld
    {
        #region Fields

        private Game game;

        #endregion

        #region Properties

        public int[] WorldMap { get; private set; }
        public short Xpos { get; private set; }
        public short Ypos { get; private set; }
        public List<string> Statics { get; private set; }
        public List<string> StaticCodeBooks { get; private set; }
        public List<ArmorCodeBook> Armors { get; private set; }
        public List<JewelCodeBook> Jewels { get; private set; }
        public List<MaterialCodeBook> Materials { get; private set; }
        public List<MonsterFamilyCodeBook> Monsters { get; private set; }
        public List<WeaponCodeBook> Weapons { get; private set; }
        public List<string> OrphanItems { get; private set; }
        public List<PlayerGraveModel> PlayerGraves { get; private set; }

        #endregion

        public World(Game game)
        {
            this.game = game;
            game.EventLogin += Game_EventLogin;
            game.EventMobMove += Game_EventMobMove;
            game.EventStaticObjectChange += Game_EventStaticObjectChange;
            game.EventMapStaticCodeBook += Game_EventMapStaticCodeBook;
            game.EventCodeBook += Game_EventCodeBook;
            game.EventMobData += Game_EventMobData;
            game.EventMapData += Game_EventMapData;
            game.EventNpcData += Game_EventNpcData;
        }

        private void Game_EventNpcData(object sender, Api.Response.ResponseNpcData e)
        {
            // e.
        }

        private void Game_EventMapData(object sender, Api.Response.ResponseMapData e)
        {
            Statics = e.Statics;
            Xpos = e.Xpos;
            Ypos = e.Ypos;
            OrphanItems = e.OrphanItems;
            PlayerGraves = e.PlayerGraves;
        }

        private void Game_EventMobData(object sender, Api.Response.ResponseMobData e)
        {
            // e.
        }

        private void Game_EventCodeBook(object sender, Api.Response.ResponseCodeBook e)
        {
            Armors = e.Armors;
            Jewels = e.Jewels;
            Materials = e.Materials;
            Monsters = e.Monsters;
            Weapons = e.Weapons;
        }

        private void Game_EventMapStaticCodeBook(object sender, Api.Response.ResponseMapStaticCodeBook e)
        {
            StaticCodeBooks = e.StaticCodeBooks;
        }

        private void Game_EventStaticObjectChange(object sender, Api.Response.ResponseStaticObjectChange e)
        {
            // TODO
        }

        private void Game_EventMobMove(object sender, Api.Response.ResponseMobMove e)
        {
            // TOOD
        }

        private void Game_EventLogin(object sender, Api.Response.ResponseLogin e)
        {
            WorldMap = e.WorldMap;
            Xpos = e.Xpos;
            Ypos = e.Ypos;
            Statics = e.Statics;
        }
    }
}