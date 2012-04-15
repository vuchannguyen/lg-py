using System;
using System.Collections.Generic;
using System.Timers;
using System.ComponentModel;

namespace Gilwell
{
    public class Startup
    {
        public delegate void TestEventHandler();
        public event TestEventHandler WaitStart;

        protected System.Timers.Timer WaitingTimer = new System.Timers.Timer();
        protected ISynchronizeInvoke SyncObject;

        public Startup(ISynchronizeInvoke SyncObject)
        {
            this.SyncObject = SyncObject;
            WaitingTimer.Elapsed += new ElapsedEventHandler(Tick);
            WaitingTimer.Interval = 3000;
        }

        protected delegate void TickDelegate(object sender, ElapsedEventArgs e);
        private void Tick(object sender, ElapsedEventArgs e)
        {
            if (SyncObject.InvokeRequired)
                SyncObject.BeginInvoke(new TickDelegate(Tick), new object[] { sender, e });
            else
                WaitStart();
        }

        public void Go()
        {
            WaitingTimer.AutoReset = false;
            WaitingTimer.Enabled = true;
        }
    }
}
