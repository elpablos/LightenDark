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

        /// <summary>
        /// Name of script
        /// </summary>
        public override string DisplayName { get { return "TestScript"; } }

        /// <summary>
        /// Main loop
        /// </summary>
        protected override async Task Run()
        {
            //// init
            DateTime start = DateTime.Now;
            ////await Game.Player.CastSpellAsync(Game.Player.ID, 0, (int)SpellType.CreateFood);
            //Game.Player.MoveUpAsync().Wait();
            //LogMessage("Up finished");
            //Game.Player.MoveRightAsync().Wait();
            //LogMessage("Right finished");
            //Game.Player.MoveDownAsync().Wait();
            //LogMessage("Down finished");
            //Game.Player.MoveLeftAsync().Wait();
            //LogMessage("Left finished");

            //var gloves = Game.Player.Inventory.Armors.Where(a => a.ArmorTypeID == 203);
            //if (gloves != null && gloves.Count() > 0)
            //{
            //    LogMessage("Gloves " + gloves.Count());

            //    var ids = gloves.Select(g => g.ID).ToArray();
            //    foreach (var id in ids)
            //    {
            //        string js = string.Format("ws.send('{{\"type\":88,\"itemCode\":10203,\"npcId\":2,\"id\":{0}}}');", id);
            //        Game.SendJavaScript(js);
            //        await Task.Delay(1000);
            //    }
            //}

            int square = 10;

            int xOffset = 120;
            int yOffset = 80;

            List<GridPos> interestingPoints = new List<GridPos>();

            // init grid
            BaseGrid searchGrid = new StaticGrid(square, square);

            for (int x = 0; x < square; x++)
            {
                for (int y = 0; y < square; y++)
                {
                    int type = Game.World.WorldMap[(x + xOffset)][(y + yOffset)];

                    // 2 - voda, 5 - jeskyne
                    if (type >= 1 && type <= 5 && type != 2)
                    {
                        // set walkable
                        searchGrid.SetWalkableAt(x, y, true);

                        // zajimava mista pro fishing (voda ne nejaky strane
                        if (Game.World.WorldMap[(x + xOffset + 1)][y + yOffset] == 2
                            || Game.World.WorldMap[x + xOffset][y + yOffset + 1] == 2
                            || Game.World.WorldMap[x + xOffset - 1][y + yOffset] == 2
                            || Game.World.WorldMap[x + xOffset][y + yOffset -1] == 2
                            )
                        {
                            interestingPoints.Add(new GridPos(x, y));
                        }
                    }
                }
            }

            var statics = Game.World.Statics.Where
            (s =>
                (s.Xpos >= xOffset)
                && (s.Xpos < xOffset + square)
                && (s.Ypos >= yOffset)
                && (s.Ypos < yOffset + square)
            );

            // 126:78:22 - strom (nelze tezit)
            // 123:81:24 - mech(nelze tezit)
            // 119:79:33 - kamen(nelze tezit)
            foreach (var st in statics)
            {
                // set walkable
                searchGrid.SetWalkableAt(st.Xpos - xOffset, st.Ypos - yOffset, false);
            }

            Game.OutputWrite("interestingPoints " + interestingPoints.Count);

            foreach (var interest in interestingPoints)
            {
                Game.OutputWrite(string.Format("Interest [{0}:{1}]", interest.x, interest.y));
                // declare start and end
                GridPos startPos = new GridPos(Game.Player.Xpos -xOffset, Game.Player.Ypos - yOffset);
                GridPos endPos = new GridPos(interest.x, interest.y);
                JumpPointParam jpParam = new JumpPointParam(searchGrid, startPos, endPos, false, true, false);

                // find specific points in path
                List<GridPos> resultPathList = JumpPointFinder.FindPath(jpParam);

                // nelze tam dojit
                if (resultPathList.Count == 0)
                {
                    Game.OutputWrite("CONTINUE");
                    continue;
                }

                // get full path
                resultPathList = JumpPointFinder.GetFullPath(resultPathList);
                HashSet<GridPos> uniquePath = new HashSet<GridPos>(resultPathList);

                Game.OutputWrite(string.Format("GOTO [{0}:{1}]", interest.x, interest.y));

                foreach (var point in uniquePath)
                {
                    await Move(point, xOffset, yOffset, MOVEMENT_TIMEOUT);
                }

                // DO ACTION!
                Game.ShowBubble("Nadpis", "text");
                await Task.Delay(3000);
            }


            // run! :)

            //LogMessage("Start first");

            //await Game.Player.MoveUpAsync();
            //if (!run) return;
            //await Game.Player.MoveUpAsync();
            //if (!run) return;
            //await Game.Player.MoveUpAsync();
            //if (!run) return;

            //LogMessage("Start second");

            //await Game.Player.MoveRightAsync();
            //if (!run) return;
            //await Game.Player.MoveRightAsync();
            //if (!run) return;
            //await Game.Player.MoveRightAsync();
            //if (!run) return;

            //LogMessage("Start third");

            //await Game.Player.MoveDownAsync();
            //if (!run) return;
            //await Game.Player.MoveDownAsync();
            //if (!run) return;
            //await Game.Player.MoveDownAsync();
            //if (!run) return;

            //LogMessage("Start fourth");

            //await Game.Player.MoveLeftAsync();
            //if (!run) return;
            //await Game.Player.MoveLeftAsync();
            //if (!run) return;
            //await Game.Player.MoveLeftAsync();
            if (!run) return;



            //// Game.Player.MoveUp();
            LogMessage("Casting time " + DateTime.Now.Subtract(start).TotalMilliseconds);
        }

        protected async Task Move(GridPos point, int xOffset, int yOffset, int timeout)
        {
            if ((point.x + xOffset) > Game.Player.Xpos)
            {
                await Game.Player.MoveRightAsync(timeout);
            }
            else if ((point.x + xOffset) < Game.Player.Xpos)
            {
                await Game.Player.MoveLeftAsync(timeout);
            }
            else if ((point.y + yOffset) > Game.Player.Ypos)
            {
                await Game.Player.MoveDownAsync(timeout);
            }
            else if ((point.y + yOffset) < Game.Player.Ypos)
            {
                await Game.Player.MoveUpAsync(timeout);
            }
        }


    //    private async Task<ResponseMovement> MoveDown()
    //    {
    //        var tcs = new TaskCompletionSource<ResponseMovement>();
    //        EventHandler<ResponseMovement> handler = null;
    //        handler = (s, e) =>
    //        {
    //            tcs.SetResult(e);
    //            Game.EventMovement -= handler;
    //        };

    //        Game.EventMovement += handler;

    //        await Task.Delay(500);
    //        Game.Player.MoveDown();


    //        return await tcs.Task;
    //    }
    //}

    //public class SampleScript : ScriptBase
    //{
    //    /// <summary>
    //    /// Name of script
    //    /// </summary>
    //    public override string DisplayName { get { return "SampleScript"; } }

    //    /// <summary>
    //    /// Main loop
    //    /// </summary>
    //    protected override void Run()
    //    {
    //        Game.Player.MoveDown();
    //        Sleep(1000);
    //        Game.Player.MoveUp();
    //        Sleep(1000);
    //        Game.Player.MoveLeft();
    //        Sleep(1000);
    //        Game.Player.MoveRight();
    //        Sleep(1000);
    //    }
    //}

    //public class MiningScript : ScriptBase
    //{
    //    private Waipoint[] noobDung = new Waipoint[]
    //    {
    //        new Waipoint(862,75),
    //        new Waipoint(862,76),
    //        new Waipoint(862,76),
    //        new Waipoint(863,76),
    //        new Waipoint(864,76),
    //        new Waipoint(864,75),
    //        new Waipoint(865,75),
    //        new Waipoint(865,76),
    //        new Waipoint(865,77),
    //        new Waipoint(866,77),
    //        new Waipoint(866,76),
    //        new Waipoint(866,75),
    //        new Waipoint(867,75),
    //        new Waipoint(867,76),
    //        new Waipoint(867,77),
    //        new Waipoint(868,77),
    //        new Waipoint(868,76),
    //        new Waipoint(868,75),
    //        new Waipoint(869,75),
    //        new Waipoint(869,74),
    //        new Waipoint(869,76),
    //        new Waipoint(870,76),
    //        new Waipoint(870,75),
    //        new Waipoint(870,74),
    //        new Waipoint(870,73),
    //        new Waipoint(870,72),
    //        new Waipoint(871,72),
    //        new Waipoint(871,73),
    //        new Waipoint(871,74),
    //        new Waipoint(871,75),
    //        new Waipoint(871,76),
    //        new Waipoint(871,73),
    //        new Waipoint(872,73),
    //        new Waipoint(872,72),
    //        new Waipoint(872,71),
    //        new Waipoint(873,72),
    //        new Waipoint(873,71),
    //        new Waipoint(874,71),
    //        // zpet
    //        new Waipoint(872,71),
    //        new Waipoint(872,73),
    //        new Waipoint(870,73),
    //        new Waipoint(870,73),
    //        new Waipoint(870,76),
    //        new Waipoint(862,76),
    //    };

    //    public Waipoint[] Waipoints { get; set; }

    //    private Waipoint PlayerPosition = new Waipoint(0, 0);
    //    private JavaScriptSerializer serializer = new JavaScriptSerializer();

    //    public override string DisplayName
    //    {
    //        get { return "MiningScript - Internal"; }
    //    }

    //    public MiningScript()
    //    {
    //        Waipoints = noobDung;
    //        serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
    //    }

    //    protected override void Game_GameMessage(object sender, GameEventArgs e)
    //    {
    //        LogMessage("Type " + e.Message);
    //        dynamic obj = serializer.Deserialize(e.Message, typeof(object));

    //        // pozice hrace
    //        if (obj.t == 32)
    //        {
    //            PlayerPosition.X = obj.x;
    //            PlayerPosition.Y = obj.y;

    //            LogMessage("Pos " + PlayerPosition.X + " " + PlayerPosition.Y);
    //            ReleaseLock();
    //        }
    //        else if (obj.t == 63 && e.Message.Contains("Ore in this location is depleted"))
    //        {
    //            // vytezeno
    //            ReleaseLock();
    //        }
    //    }

    //    protected override void Run()
    //    {
    //        int i = 0;
    //        while (run)
    //        {
    //            if (i == Waipoints.Length) i = 0;
    //            LogMessage("Waipoint " + i);
    //            GoToWaypoint(Waipoints[i]);

    //            // tezba
    //            SendJavascript("ws.send('{\"type\":66,\"gatherType\":1}');");
    //            WaitLock();

    //            Sleep(1000);
    //            i++;
    //        }
    //    }

    //    protected void GoToWaypoint(Waipoint point)
    //    {
    //        while (run && (PlayerPosition.X != point.X))
    //        {
    //            if (PlayerPosition.X > point.X)
    //            {
    //                SendJavascript("moveLeft();");
    //            }
    //            else
    //            {
    //                SendJavascript("moveRight();");
    //            }

    //            WaitLock(2000);
    //            Sleep(1000);
    //        }

    //        while (run && (PlayerPosition.Y != point.Y))
    //        {
    //            if (PlayerPosition.Y > point.Y)
    //            {
    //                SendJavascript("moveUp();");
    //            }
    //            else
    //            {
    //                SendJavascript("moveDown();");
    //            }

    //            WaitLock(2000);
    //            Sleep(1000);
    //        }
    //    }
    //}

    //public class Waipoint
    //{
    //    public int X { get; set; }

    //    public int Y { get; set; }

    //    public Waipoint(int x, int y)
    //    {
    //        X = x;
    //        Y = y;
    //    }
    //}
}
#endif

}