## Jak skript funguje ##
Skript neustále kouzlí jídlo na postavu a když mu dojde mana, začne meditovat do plna.

## Použití ##
Před startem je potřeba svléknout postavu a nebo pozměnit timer na kouzlení.

## Kód ##

```
#!c#
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

namespace GeminiTester.Scripts
{
    /// <summary>
    /// TODO: PRED SPUSTENIM SI ZMEN chrTgt na svoje ID v metode "Run"
    /// Dale je potreba svleknout postavu do naha, protoze pak je casting do 3 sec a nebo ten casting prodlouzit!
    /// </summary>
    public class MageryScript : ScriptBase
    {
        private JavaScriptSerializer serializer = new JavaScriptSerializer();

        private int manaMax;
        private int actualMana;
        private int playerId;

        public override string DisplayName
        {
            get { return "MageryScript v1.0"; }
        }

        public MageryScript()
        {
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
        }

        protected override void Game_GameMessage(object sender, GameEventArgs e)
        {
            dynamic obj = serializer.Deserialize(e.Message, typeof(object));
            if (obj.t != null)
            {
                // celkový status
                if (obj.t == 14)
                {
                    if (obj.chrTo != null)
                    {
                        // max mana
                        if (manaMax <= 0)
                        {
                            manaMax = obj.chrTo.maxMp;
                        }
                        // playerId
                        if (playerId <= 0)
                        {
                            playerId = obj.chrTo.id;
                        }

                        // actual mana
                        actualMana = obj.chrTo.mana;
                        ReleaseLock();
                    }
                }
                // aktuální změna
                else if (obj.t == 10 && obj.id == playerId)
                {
                    // actual mana
                    actualMana = obj.mp;
                    ReleaseLock();
                }
            }
        }

        /// <summary>
        /// TODO: PRED SPUSTENIM SI ZMEN chrTgt na svoje ID :)
        /// </summary>
        protected override void Run()
        {
            while (run)
            {
                // kouzli jídlo
                while (run && (actualMana > 20 || actualMana == 0))
                {
                    // vykouzli jídlo
                    LogMessage("Kouzlim jidlo");
                    SendJavascript("ws.send('{\"type\":59,\"chrTgt\":' + window.gameCharacter.id + ',\"mobTgt\":0,\"spell\":1}');");
                    Sleep(3500);
                    //WaitLock();
                }

                // zapni meditaci
                LogMessage("Medituji");
                SendJavascript("ws.send('{\"type\":20,\"actType\":2}');"); // resting (nutno stat vedle ohne)
                SendJavascript("ws.send('{\"type\":20,\"actType\":1}');"); // vlastni meditace

                // čekej do naplneni many
                while (run && (actualMana < manaMax))
                {
                    WaitLock(3000);
                    Sleep(1000);
                }
            }
        }
    }
}
```