﻿using LightenDark.Api;
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

namespace GeminiTester.Scripts
{
#if DEBUG

    public class TestScript : ScriptBase
    {
        private JavaScriptSerializer serializer = new JavaScriptSerializer();

        /// <summary>
        /// Name of script
        /// </summary>
        public override string DisplayName { get { return "TestScript"; } }

        public TestScript()
        {
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
        }

        /// <summary>
        /// Main loop
        /// </summary>
        protected override void Run()
        {
        }

        protected override void Game_GameMessage(object sender, GameEventArgs e)
        {
            LogMessage("Type " + e.Message);
            dynamic obj = serializer.Deserialize(e.Message, typeof(object));
            if (obj == null)
            {
                LogMessage("obj is null");
            }
            else
            {
                LogMessage("obj is not null");
                LogMessage("t" + obj.t);
            }

        }
    }

    public class SampleScript : ScriptBase
    {
        /// <summary>
        /// Name of script
        /// </summary>
        public override string DisplayName { get { return "SampleScript"; } }

        /// <summary>
        /// Main loop
        /// </summary>
        protected override void Run()
        {
            Game.Player.MoveDown();
            Sleep(1000);
            Game.Player.MoveUp();
            Sleep(1000);
            Game.Player.MoveLeft();
            Sleep(1000);
            Game.Player.MoveRight();
            Sleep(1000);
        }
    }

    public class MiningScript : ScriptBase
    {
        private Waipoint[] noobDung = new Waipoint[]
        {
            new Waipoint(862,75),
            new Waipoint(862,76),
            new Waipoint(862,76),
            new Waipoint(863,76),
            new Waipoint(864,76),
            new Waipoint(864,75),
            new Waipoint(865,75),
            new Waipoint(865,76),
            new Waipoint(865,77),
            new Waipoint(866,77),
            new Waipoint(866,76),
            new Waipoint(866,75),
            new Waipoint(867,75),
            new Waipoint(867,76),
            new Waipoint(867,77),
            new Waipoint(868,77),
            new Waipoint(868,76),
            new Waipoint(868,75),
            new Waipoint(869,75),
            new Waipoint(869,74),
            new Waipoint(869,76),
            new Waipoint(870,76),
            new Waipoint(870,75),
            new Waipoint(870,74),
            new Waipoint(870,73),
            new Waipoint(870,72),
            new Waipoint(871,72),
            new Waipoint(871,73),
            new Waipoint(871,74),
            new Waipoint(871,75),
            new Waipoint(871,76),
            new Waipoint(871,73),
            new Waipoint(872,73),
            new Waipoint(872,72),
            new Waipoint(872,71),
            new Waipoint(873,72),
            new Waipoint(873,71),
            new Waipoint(874,71),
            // zpet
            new Waipoint(872,71),
            new Waipoint(872,73),
            new Waipoint(870,73),
            new Waipoint(870,73),
            new Waipoint(870,76),
            new Waipoint(862,76),
        };

        public Waipoint[] Waipoints { get; set; }

        private Waipoint PlayerPosition = new Waipoint(0, 0);
        private JavaScriptSerializer serializer = new JavaScriptSerializer();

        public override string DisplayName
        {
            get { return "MiningScript - Internal"; }
        }

        public MiningScript()
        {
            Waipoints = noobDung;
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
        }

        protected override void Game_GameMessage(object sender, GameEventArgs e)
        {
            LogMessage("Type " + e.Message);
            dynamic obj = serializer.Deserialize(e.Message, typeof(object));

            // pozice hrace
            if (obj.t == 32)
            {
                PlayerPosition.X = obj.x;
                PlayerPosition.Y = obj.y;

                LogMessage("Pos " + PlayerPosition.X + " " + PlayerPosition.Y);
                ReleaseLock();
            }
            else if (obj.t == 63 && e.Message.Contains("Ore in this location is depleted"))
            {
                // vytezeno
                ReleaseLock();
            }
        }

        protected override void Run()
        {
            int i = 0;
            while (run)
            {
                if (i == Waipoints.Length) i = 0;
                LogMessage("Waipoint " + i);
                GoToWaypoint(Waipoints[i]);

                // tezba
                SendJavascript("ws.send('{\"type\":66,\"gatherType\":1}');");
                WaitLock();

                Sleep(1000);
                i++;
            }
        }

        protected void GoToWaypoint(Waipoint point)
        {
            while (run && (PlayerPosition.X != point.X))
            {
                if (PlayerPosition.X > point.X)
                {
                    SendJavascript("moveLeft();");
                }
                else
                {
                    SendJavascript("moveRight();");
                }

                WaitLock(2000);
                Sleep(1000);
            }

            while (run && (PlayerPosition.Y != point.Y))
            {
                if (PlayerPosition.Y > point.Y)
                {
                    SendJavascript("moveUp();");
                }
                else
                {
                    SendJavascript("moveDown();");
                }

                WaitLock(2000);
                Sleep(1000);
            }
        }
    }

    public class Waipoint
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Waipoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
#endif
}