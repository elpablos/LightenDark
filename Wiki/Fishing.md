## Jak skript funguje ##
Skript je upraven pro chytání ryb v guard zóně. Jde o pravou stranu, takový to jezero.
Postava chodí po předem definovaných waypointech a rybaří. Jakmile pole vychytá, posune se na další waypoint. Po skončení posledního bodu, začíná cyhtat od znova.

Narozdíl od miningu bylo třeba aby některý políčka vyloženě přeskočil, takže jsem upravil Waypoint třídu o flag skip.

## Použití ##
Před startem je potřeba si stoupnout na pozici 128,89. 
Dále je potřeba aby postava měla v ruce prut na ryby.

## Známé chyby ##
- sem tam se zasekne, to dělá do dementní NPC

```
#!c#

using LightenDark.Api;
using LightenDark.Api.Args;
using LightenDark.Api.Interfaces;
using LightenDark.Api.Models;
using LightenDark.Api.Response;
using LightenDark.Api.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Web.Script.Serialization;

namespace GeminiTester.Scripts
{    
    public class FishingScript : ScriptBase
    {
        private Waipoint[] guardZone = new Waipoint[]
        {
            new Waipoint(129,89),
            new Waipoint(128,89, true),
            new Waipoint(128,88),
            new Waipoint(127,88, true),
            new Waipoint(127,87),
            new Waipoint(127,88, true),
            new Waipoint(125,88, true),
            new Waipoint(125,86, true),
            new Waipoint(126,86),
            new Waipoint(125,86, true),
            new Waipoint(125,85),
            new Waipoint(125,84),
            new Waipoint(125,83, true),
            new Waipoint(126,83),
            new Waipoint(126,82),
            new Waipoint(126,81),
            new Waipoint(126,80),
            new Waipoint(126,79, true),
            new Waipoint(127,79),
            new Waipoint(127,78),
            new Waipoint(127,77),
            new Waipoint(127,76),
            new Waipoint(127,75, true),
            new Waipoint(128,75),
            new Waipoint(128,74),
            new Waipoint(128,73),
            new Waipoint(127,73, true), 
            new Waipoint(127,72),
            new Waipoint(126,72, true), 
            new Waipoint(126,70),
            new Waipoint(126,69),
            new Waipoint(126,68),
            new Waipoint(126,67),
            new Waipoint(126,66),
            new Waipoint(126,65),
            new Waipoint(126,64),
            new Waipoint(125,64, true),
            new Waipoint(125,62, true),
            new Waipoint(128,62),
            new Waipoint(128,61, true), 
            new Waipoint(129,61),
            // a zpět
            new Waipoint(124,61, true),
            new Waipoint(124,82, true),
            new Waipoint(123,82, true),
            new Waipoint(123,89, true),

        };

        public Waipoint[] Waipoints { get; set; }

        public override string DisplayName
        {
            get { return "FishingScript v2.0"; }
        }

        public FishingScript()
        {
            Waipoints = guardZone;
        }

        protected override async Task Run()
        {                 
            int i = 0;
            while (run)
            {
                if (i == Waipoints.Length) i = 0;
                LogMessage("Waipoint " + i);
                
                Waipoint waipoint = Waipoints[i];
                
                // pohyb
                await GoToWaypoint(waipoint);
                
                 if (!waipoint.CanSkip)
                 {
                    // fishing
                    Game.Player.Garthering(GartheringType.Fishing);
                    
                    // zachyceni textove zpravy z chatu
                    ResponseChatMessage msg = null;
                    do
                    {
                        msg = await Game.ResponseWaitBase<ResponseChatMessage>(() => {}, null, "EventChatMessage");
                    } while (run && !msg.Messages.Any(m => m.Message.Contains("There are no more fish nearby")));
                }
                // cekej vterinu
                await Task.Delay(1000);
                i++;
            }
        }

        protected async Task GoToWaypoint(Waipoint point)
        {
            while (run && (Game.Player.Xpos != point.X))
            {
                if (Game.Player.Xpos > point.X)
                {
                    //SendJavascript("moveLeft();");
                    await Game.Player.MoveLeftAsync();
                }
                else
                {
                    //SendJavascript("moveRight();");
                    await Game.Player.MoveRightAsync();
                }
                await Task.Delay(1000);
            }

            while (run && (Game.Player.Ypos != point.Y))
            {
                if (Game.Player.Ypos > point.Y)
                {
                    // SendJavascript("moveUp();");
                    await Game.Player.MoveUpAsync();
                }
                else
                {
                    // SendJavascript("moveDown();");
                    await Game.Player.MoveDownAsync();
                }
                await Task.Delay(1000);
            }
        }
    }

    public class Waipoint
    {
        public int X { get; set; }

        public int Y { get; set; }
        
        public bool CanSkip { get; set; }

        public Waipoint(int x, int y, bool canSkip = false)
        {
            X = x;
            Y = y;
            CanSkip = canSkip;
        }
    }
}
```