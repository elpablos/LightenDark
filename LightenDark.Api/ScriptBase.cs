using LightenDark.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LightenDark.Api
{
    /// <summary>
    /// Base script class
    /// </summary>
    public abstract class ScriptBase : IScript
    {
        #region Fields

        private object obj = new object();
        private Thread thread;

        protected bool run;

        #endregion

        #region Properties

        public IGame Game { get; set; }

        public abstract string DisplayName { get; }

        #endregion

        #region Public methods

        public void Start()
        {
            Game.GameMessage += Game_GameMessage;
            // Game.CancelTokenSource.Token.
            thread = new Thread(new ThreadStart(AbstractRun));

            //Task t = new Task(AbstractRun);
            //t.Start();

            lock (obj)
            {
                run = true;
            }
            thread.Start();
        }

        public void Stop(bool isForce)
        {
            if (thread != null && thread.IsAlive)
            {
                BeforeStop();

                Game.GameMessage -= Game_GameMessage;
                lock (obj)
                {
                    run = false;
                }
                ReleaseLock();
                // Game.CancelTokenSource.Cancel(false);
                if (isForce)
                {
                    thread.Abort();
                }
            }
        }

        #endregion

        #region Main loop

        protected abstract void Run();

        protected virtual void BeforeStop() { }

        protected virtual void Game_GameMessage(object sender, Args.GameEventArgs e) { }

        private void AbstractRun()
        {
            LogMessage(string.Format("start skriptu '{0}'", DisplayName));

            Run();

            LogMessage(string.Format("konec skriptu '{0}'", DisplayName));

        }

        #endregion

        #region protected methods

        protected void SendJavascript(string script)
        {
            Game.SendJavaScript(script);
        }

        protected void LogMessage(string msg)
        {
            Game.OutputWrite(msg);
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
