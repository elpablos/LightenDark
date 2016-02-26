using LightenDark.Api;
using LightenDark.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Web.Script.Serialization;
using LightenDark.Api.Args;
using LightenDark.Api.Types;
using LightenDark.Api.Response;
using EpPathFinding.cs;

namespace GeminiTester.Scripts
{
#if DEBUG

    /// <summary>
    /// 1 - tráva
    /// 2 - voda
    /// 3 - skála
    /// 4 - jeskyne
    /// 5 - cesta
    /// 6 - wasteland
    /// 7 - vulcanic
    /// 8 - podzemi
    /// 
    /// </summary>
    public class TestScript : ScriptBase
    {
        private const int MOVEMENT_TIMEOUT = 2000;

        private const int GARTHER_TIMEOUT = 60000;

        /// <summary>
        /// Name of script
        /// </summary>
        public override string DisplayName { get { return "Garther script"; } }

        /// <summary>
        /// Main loop
        /// </summary>
        protected override async Task Run()
        {
            // definice 
            int xStart = 90;
            int yStart = 65;

            int xEnd = 130;
            int yEnd = 92;

            bool canHarvestHerb = true;
            bool canFishing = true;
            bool canLumberJacking = true;

            //// init
            DateTime start = DateTime.Now;

            #region grid map

            // init grid
            NodePool nodePool = new NodePool();

            for (int x = xStart; x < xEnd; x++)
            {
                for (int y = yStart; y < yEnd; y++)
                {
                    int type = Game.World.WorldMap[x][y];
                    // neni vodda nebo skala
                    if (type != 2 && type != 3)
                    {
                        // set walkable
                        nodePool.SetNode(x, y, true);
                    }
                }
            }

            // vytahnu prekazky v okoli
            var statics = Game.World.Statics.Where
            (s =>
                (s.Xpos >= xStart)
                && (s.Xpos < xEnd)
                && (s.Ypos >= yStart)
                && (s.Ypos < yEnd)
                && s.TypeId != 7 // vstup do jeskyne
                && s.TypeId != 8 // vstup do jeskyne
                && s.TypeId != 30 // kameni na zemi
                && s.TypeId != 31 // kameni na zemi
                && s.TypeId != 37 // kameni na zemi
                && s.TypeId != 38 // kameni na zemi
                && s.TypeId != 39 // kameni na zemi
                && s.TypeId != 40 // kameni na zemi
                && s.TypeId != 55 // ker
                && s.TypeId != 56 // ker
                && s.TypeId != 62 // maly ohen
                && s.TypeId != 70 // kyticky (herb)
                && s.TypeId != 71 // kyticky (herb)
                && s.TypeId != 72 // kyticky (herb)
            );

            // 124:84 - prekazka!

            // 126:78:22 - strom (nelze tezit)
            // 123:81:24 - mech(nelze tezit)
            // 119:79:33 - kamen(nelze tezit)
            foreach (var st in statics)
            {
                // set walkable
                nodePool.SetNode(st.Xpos, st.Ypos, false);
            }

            // npc
            foreach (var npc in Game.World.Npcs)
            {
                // set walkable
                nodePool.SetNode(npc.XPos, npc.YPos, false);
            }

            // grid
            BaseGrid searchGrid = new PartialGridWPool(nodePool, new GridRect(xStart, yStart, xEnd, yEnd));

            // nechodit pres rohy + heuristika EUCLIDEAN
            JumpPointParam jpParam = new JumpPointParam(searchGrid, false, false, false, HeuristicMode.EUCLIDEAN);

            #endregion

            var gartherFullList = new List<GartherPoint>();
            List<GartherPoint> gartheringList = null;

            #region Herbs

            // vytahnu prekazky v okoli
            var herbs = Game.World.Statics.Where
            (s =>
                (s.Xpos >= xStart)
                && (s.Xpos < xEnd)
                && (s.Ypos >= yStart)
                && (s.Ypos < yEnd)
                && (s.TypeId == 70 // kyticky (herb)
                    || s.TypeId == 71 // kyticky (herb)
                    || s.TypeId == 72 // kyticky (herb)
                )
            );

            // kyticky
            if (canHarvestHerb)
            {
                foreach (var herb in herbs)
                {
                    var point = new GartherPoint();
                    point.X = herb.Xpos;
                    point.Y = herb.Ypos;
                    point.Type = GartheringType.Harvest;

                    gartherFullList.Add(point);
                }
            }

            #endregion

            #region Fishing

            // rybicky
            if (canFishing)
            {
                for (int x = searchGrid.gridRect.minX; x <= searchGrid.gridRect.maxX; x++)
                {
                    for (int y = searchGrid.gridRect.minY; y <= searchGrid.gridRect.maxY; y++)
                    {
                        // pouze pokud tam lze vlézt
                        if (searchGrid.IsWalkableAt(x, y))
                        {
                            // zajimava mista pro fishing (voda ne nejaky strane)
                            if ((Game.World.WorldMap[(x + 1)][y] == 2
                                || Game.World.WorldMap[x][y + 1] == 2
                                || Game.World.WorldMap[x - 1][y] == 2
                                || Game.World.WorldMap[x][y - 1] == 2
                                ))
                            {
                                var point = new GartherPoint();
                                point.X = x;
                                point.Y = y;
                                point.Type = GartheringType.Fishing;
                                gartherFullList.Add(point);
                            }
                        }
                    }
                }
            }

            #endregion

            #region Lumber

            if (canLumberJacking)
            {
                // vytahnu prekazky v okoli
                var trees = Game.World.Statics.Where
                (s =>
                    (s.Xpos >= xStart)
                    && (s.Xpos < xEnd)
                    && (s.Ypos >= yStart)
                    && (s.Ypos < yEnd)
                    && (s.TypeId == 20 // strom
                        || s.TypeId == 25 // fruity 
                    )
                );

                GridPos pos;
                GridPos startPos = new GridPos(Game.Player.Xpos, Game.Player.Ypos);

                foreach (var tree in trees)
                {
                    // +1, 0
                    pos = new GridPos(tree.Xpos + 1, tree.Ypos);
                    if (searchGrid.IsWalkableAt(pos))
                    {
                        jpParam.Reset(startPos, pos);
                        // find specific points in path
                        List<GridPos> resultPathList = JumpPointFinder.FindPath(jpParam);
                        if (resultPathList.Count != 0)
                        {
                            var point = new GartherPoint();
                            point.X = pos.x;
                            point.Y = pos.y;
                            point.Type = GartheringType.Lumber;
                            gartherFullList.Add(point);

                            continue;
                        }
                    }
                    // -1, 0
                    pos = new GridPos(tree.Xpos - 1, tree.Ypos);
                    if (searchGrid.IsWalkableAt(pos))
                    {
                        jpParam.Reset(startPos, pos);
                        // find specific points in path
                        List<GridPos> resultPathList = JumpPointFinder.FindPath(jpParam);
                        if (resultPathList.Count != 0)
                        {
                            var point = new GartherPoint();
                            point.X = pos.x;
                            point.Y = pos.y;
                            point.Type = GartheringType.Lumber;
                            gartherFullList.Add(point);

                            continue;
                        }
                    }
                    // 0, +1
                    pos = new GridPos(tree.Xpos, tree.Ypos + 1);
                    if (searchGrid.IsWalkableAt(pos))
                    {
                        jpParam.Reset(startPos, pos);
                        // find specific points in path
                        List<GridPos> resultPathList = JumpPointFinder.FindPath(jpParam);
                        if (resultPathList.Count != 0)
                        {
                            var point = new GartherPoint();
                            point.X = pos.x;
                            point.Y = pos.y;
                            point.Type = GartheringType.Lumber;
                            gartherFullList.Add(point);

                            continue;
                        }
                    }
                    // 0, -1
                    pos = new GridPos(tree.Xpos, tree.Ypos - 1);
                    if (searchGrid.IsWalkableAt(pos))
                    {
                        jpParam.Reset(startPos, pos);
                        // find specific points in path
                        List<GridPos> resultPathList = JumpPointFinder.FindPath(jpParam);
                        if (resultPathList.Count != 0)
                        {
                            var point = new GartherPoint();
                            point.X = pos.x;
                            point.Y = pos.y;
                            point.Type = GartheringType.Lumber;
                            gartherFullList.Add(point);

                            continue;
                        }
                    }
                }
            }



            #endregion

            #region Garther

            Game.OutputWrite("interestingPoints " + gartherFullList.Count);

            while (run && gartherFullList.Count > 0)
            {
                gartheringList = new List<GartherPoint>(gartherFullList);
                GartherPoint next = null;
                while (run && gartheringList.Count > 0)
                {
                    // serazeni dle blizkosti k postave
                    int playerX = Game.Player.Xpos;
                    int playerY = Game.Player.Ypos;
                    var manhattan = new Func<GartherPoint, int>((p) => { return Math.Abs(p.X - playerX) + Math.Abs(p.Y - playerY); });
                    next = gartheringList.OrderBy(manhattan).FirstOrDefault();

                    var point = new GridPos(next.X, next.Y);
                    Game.OutputWrite(string.Format("{2} [{0}:{1}]", point.x, point.y, next.Type));
                    // declare start and end
                    GridPos startPos = new GridPos(Game.Player.Xpos, Game.Player.Ypos);

                    jpParam.Reset(startPos, point);

                    // find specific points in path
                    List<GridPos> resultPathList = JumpPointFinder.FindPath(jpParam);

                    // nelze tam dojit
                    if (resultPathList.Count != 0)
                    {
                        // get full path
                        var fullPath = JumpPointFinder.GetFullPath(resultPathList);

                        ResponseChatMessage msg = null;

                        try
                        {
                            foreach (var p in fullPath)
                            {
                                await GoToGridPos(p, MOVEMENT_TIMEOUT);
                            }

                            Game.OutputWrite(string.Format("DO {0}", next.Type));

                            // do fishing
                            if (next.Type == GartheringType.Fishing)
                            {
                                Game.Player.Garthering(next.Type);
                                // zachyceni textove zpravy z chatu
                                do
                                {
                                    msg = await Game.ResponseWaitBase<ResponseChatMessage>(() => { }, null, "EventChatMessage", GARTHER_TIMEOUT);
                                } while (run && !msg.Messages.Any(m => m.Message.Contains("There are no more fish nearby")));
                            }
                            // do harvest
                            else if (next.Type == GartheringType.Harvest)
                            {
                                Game.Player.Garthering(next.Type);
                                // zachyceni textove zpravy z chatu
                                do
                                {
                                    msg = await Game.ResponseWaitBase<ResponseChatMessage>(() => { }, null, "EventChatMessage", GARTHER_TIMEOUT);
                                } while (run && !msg.Messages.Any(m => m.Message.Contains("There are no more useful flowers remaining")));
                            }
                            // do harvest
                            else if (next.Type == GartheringType.Mining)
                            {
                                Game.Player.Garthering(next.Type);
                                // zachyceni textove zpravy z chatu
                                do
                                {
                                    msg = await Game.ResponseWaitBase<ResponseChatMessage>(() => { }, null, "EventChatMessage", GARTHER_TIMEOUT);
                                } while (run && !msg.Messages.Any(m => m.Message.Contains("Ore in this location is depleted")));
                            }
                            // do lumberjacking
                            else if (next.Type == GartheringType.Lumber)
                            {
                                Game.Player.Garthering(next.Type);
                                // zachyceni textove zpravy z chatu
                                do
                                {
                                    msg = await Game.ResponseWaitBase<ResponseChatMessage>(() => { }, null, "EventChatMessage", GARTHER_TIMEOUT);
                                } while (run && !msg.Messages.Any(m => m.Message.Contains("Surrounding woods are depleted")));
                            }
                      
                            // SHOW BUBBLE
                            // Game.ShowBubble("Nadpis", "text");
                        }
                        catch (TaskCanceledException)
                        {
                            Game.OutputWrite("TIMEOUT");
                        }
                    }
                    else
                    {
                        Game.OutputWrite("SKIP - way not found");
                    }

                    await Task.Delay(1000);
                    gartheringList.Remove(next);
                }
            }

            #endregion

            LogMessage("Casting time " + DateTime.Now.Subtract(start).TotalMilliseconds);
        }

        /// <summary>
        /// Pohyb
        /// </summary>
        /// <param name="p"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        protected async Task GoToGridPos(GridPos p, int timeout)
        {
            // korekce
            GridPos point = p;

            while (run && (Game.Player.Xpos != point.x))
            {
                if (Game.Player.Xpos > point.x)
                {
                    await Game.Player.MoveLeftAsync(timeout);
                }
                else
                {
                    await Game.Player.MoveRightAsync(timeout);
                }
                await Task.Delay(200);
            }

            while (run && (Game.Player.Ypos != point.y))
            {
                if (Game.Player.Ypos > point.y)
                {
                    await Game.Player.MoveUpAsync(timeout);
                }
                else
                {
                    await Game.Player.MoveDownAsync(timeout);
                }
                await Task.Delay(200);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GartherPoint
    {
        public GartheringType Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    /// <summary>
    /// Prodej rukavic
    /// </summary>
    public class SellScript : ScriptBase
    {

        /// <summary>
        /// Name of script
        /// </summary>
        public override string DisplayName { get { return "SellScript"; } }

        /// <summary>
        /// Main loop
        /// </summary>
        protected override async Task Run()
        {
            //// init
            DateTime start = DateTime.Now;

            var gloves = Game.Player.Inventory.Armors.Where(a => a.ArmorTypeID == 203);
            if (gloves != null && gloves.Count() > 0)
            {
                LogMessage("Gloves " + gloves.Count());

                var ids = gloves.Select(g => g.ID).ToArray();
                foreach (var id in ids)
                {
                    string js = string.Format("ws.send('{{\"type\":88,\"itemCode\":10203,\"npcId\":2,\"id\":{0}}}');", id);
                    Game.SendJavaScript(js);
                    await Task.Delay(1000);
                }
            }
        }
    }

    /// <summary>
    /// Pohyb v kruhu
    /// </summary>
    public class MovementScript : ScriptBase
    {
        private const int MOVEMENT_TIMEOUT = 2000;

        /// <summary>
        /// Name of script
        /// </summary>
        public override string DisplayName { get { return "MovementScript"; } }

        /// <summary>
        /// Main loop
        /// </summary>
        protected override async Task Run()
        {
            //// init
            DateTime start = DateTime.Now;

            // run! :)

            LogMessage("Start first");

            await Game.Player.MoveUpAsync();
            if (!run) return;
            await Game.Player.MoveUpAsync();
            if (!run) return;
            await Game.Player.MoveUpAsync();
            if (!run) return;

            LogMessage("Start second");

            await Game.Player.MoveRightAsync();
            if (!run) return;
            await Game.Player.MoveRightAsync();
            if (!run) return;
            await Game.Player.MoveRightAsync();
            if (!run) return;

            LogMessage("Start third");

            await Game.Player.MoveDownAsync();
            if (!run) return;
            await Game.Player.MoveDownAsync();
            if (!run) return;
            await Game.Player.MoveDownAsync();
            if (!run) return;

            LogMessage("Start fourth");

            await Game.Player.MoveLeftAsync();
            if (!run) return;
            await Game.Player.MoveLeftAsync();
            if (!run) return;
            await Game.Player.MoveLeftAsync();
            if (!run) return;



            //// Game.Player.MoveUp();
            LogMessage("Casting time " + DateTime.Now.Subtract(start).TotalMilliseconds);
        }
    }

#endif

}