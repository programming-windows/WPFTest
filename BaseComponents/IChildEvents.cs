using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    //声明委托类型
    public delegate void ChildEventHandler(object arg);

    public  interface IChildEvents
    {

        //声明委托事件的对象
        event ChildEventHandler NextEvent;

        event ChildEventHandler QuitEvent;

        event ChildEventHandler WaitIconEvent;
    }


}
