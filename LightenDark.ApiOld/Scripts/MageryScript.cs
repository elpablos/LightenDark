using LightenDark.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace LightenDark.Api.Scripts
{
    public class MageryScript : AbstractScript
    {
        private JavaScriptSerializer serializer = new JavaScriptSerializer();

        private int manaMax;
        private int actualMana;
        private int playerId;

        public override string ScriptName
        {
            get { return "MageryScript"; }
        }

        public MageryScript()
        {
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
        }

        protected override void Core_MessageIncome(object sender, Args.MessageEventArgs e)
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
        /// 
        /// </summary>
        protected override void Run()
        {
            while (run)
            {
                // kouzli jídlo
                while (actualMana > 20 || actualMana == 0)
                {
                    // vykouzli jídlo
                    SendJavascript("ws.send('{\"type\":59,\"chrTgt\":135,\"mobTgt\":0,\"spell\":1}');");
                    Sleep(3500);
                    //WaitLock();
                }

                // zapni meditaci
                SendJavascript("ws.send('{\"type\":20,\"actType\":1}');");

                // čekej do naplneni many
                while (actualMana < manaMax)
                {
                    WaitLock(3000);
                    Sleep(1000);
                }
            }
        }
    }
}
