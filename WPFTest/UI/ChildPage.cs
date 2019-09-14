using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;
using System.Windows.Controls;

namespace WPFTest.UI
{
    public class ChildPage: Page, IChildEvents
    {

        public MainWindow parentWindow;

#pragma warning disable 

        public event ChildEventHandler WaitIconEvent;
        public event ChildEventHandler NextEvent;
        public event ChildEventHandler QuitEvent;

#pragma warning restore 
        
        protected virtual void FireQuitEvent(object e)
        {
            //实例化委托事件，并发起调用
            ChildEventHandler handler = QuitEvent;
            if (handler != null)
                handler(e);
        }

        protected virtual void FireNextEvent(object e)
        {
            ChildEventHandler handler = NextEvent;
            if (handler != null)
                handler(e);
        }

        protected virtual void FireWaitIconEvent(object e)
        {
            ChildEventHandler handler = WaitIconEvent;
            if (handler != null)
                handler(e);
        }

    }
}
