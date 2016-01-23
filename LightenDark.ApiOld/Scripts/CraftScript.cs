using LightenDark.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightenDark.Api.Scripts
{
    public class CraftScript : AbstractScript
    {
        public override string ScriptName
        {
            get { return "CraftScript"; }
        }

        protected override void Core_MessageIncome(object sender, Args.MessageEventArgs e)
        {
            if (e.Type == 63 && e.Message.Contains("\"type\":1") &&
                (e.Message.Contains("You have failed to craft")
                || e.Message.Contains("You have successfuly craft")
                || e.Message.Contains("You dont have enough")
                ))
            {
                if (e.Message.Contains("You dont have enough"))
                {
                    Stop();
                }
                ReleaseLock();
            }
        }

        protected override void Run()
        {
            while (run)
            {
                SendJavascript("ws.send('{\"type\":28,\"itemCode\":10089,\"npcId\":2}');");
                WaitLock();
            }
        }
    }
}
