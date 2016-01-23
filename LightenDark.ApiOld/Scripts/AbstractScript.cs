using LightenDark.Api.Enums;
using LightenDark.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LightenDark.Api.Scripts
{
    public abstract class AbstractScript : IScript
    {
        #region Fields

        private object obj = new object();
        private Thread thread;

        protected bool run;

        #endregion

        #region Properties

        public ICore Core { get; set; }

        public abstract string ScriptName { get; }

        #endregion

        #region Public methods

        public void Start()
        {
            Core.MessageIncome += Core_MessageIncome;
            thread = new Thread(new ThreadStart(AbstractRun));

            lock (obj)
            {
                run = true;
            }
            thread.Start();
        }

        public void Stop()
        {
            BeforeStop();

            Core.MessageIncome -= Core_MessageIncome;
            lock (obj)
            {
                run = false;
            }
            ReleaseLock();
        }

        #endregion

        #region Main loop

        protected abstract void Run();

        protected virtual void BeforeStop() { }

        protected virtual void Core_MessageIncome(object sender, Args.MessageEventArgs e) { }

        private void AbstractRun()
        {
            LogMessage(string.Format("start skriptu '{0}'", ScriptName));

            Run();

            LogMessage(string.Format("konec skriptu '{0}'", ScriptName));

        }

        #endregion

        #region protected methods

        protected void SendJavascript(string script)
        {
            Core.JavacriptAction(script);
        }

        protected void LogMessage(string msg)
        {
            Core.LogNewAction(ApplicationMessageType.Console, msg);
        }

        #endregion

        #region Locking

        protected void WaitLock()
        {
            lock (obj) Monitor.Wait(obj);
        }

        protected void WaitLock(int timeout)
        {
            lock (obj) Monitor.Wait(obj, timeout);
        }

        protected void ReleaseLock()
        {
            lock (obj) Monitor.Pulse(obj);
        }

        protected void Sleep(int time)
        {
            Thread.Sleep(time);
        }

        #endregion
    }
}
