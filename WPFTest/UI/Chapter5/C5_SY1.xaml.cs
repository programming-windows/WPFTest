
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Utils;

namespace WPFTest.UI.Chapter5
{
    /// <summary>
    /// C2_SY1.xaml 的交互逻辑
    /// </summary>
    public partial class C5_SY1 : ChildPage
    {

        public static Process cmdP;
        public static StreamWriter cmdStreamInput;
        
        public C5_SY1()
        {
            InitializeComponent();

        }

        public C5_SY1(MainWindow parent)
        {
            InitializeComponent();
            this.parentWindow = parent;

        }

        private void ChildPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        

        private void clearComments()
        {
            listBox1.Items.Clear();
            //textBox2.Text = "";
        }

        private void showComment(String comment)
        {
            if (MyStringUtil.isEmpty(comment)) {
                listBox1.Items.Add("");
                return;
            }

            listBox1.Items.Add(comment);
            //textBox2.Text = comment;
        }

     
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            clearComments();

            OutStr.sw = new StreamWriter("dataout.txt");
            OutStr.sw.AutoFlush = true;



            //定义一个火情发生源类对象；
            FireAlarm myFireAlarm = new FireAlarm();
            //定义一个火情处理类对象，并将源类对象作为参数传递给这个对象
            FireHandlerClass myFireHandler1 = new FireHandlerClass(this,myFireAlarm);
            //FireWatcherClass myFireHandle2 = new FireWatcherClass(myFireAlarm);
            //发生一种火情，以事件机制执行
            myFireAlarm.ActivateFireAlarm("Kitchen", 3);
            myFireAlarm.ActivateFireAlarm("Kitchen", 6);

            OutStr.sw.Close();

        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            clearComments();

            OutStr.sw = new StreamWriter("dataout.txt");
            OutStr.sw.AutoFlush = true;

            //定义一个火情发生源类对象；
            FireAlarm myFireAlarm = new FireAlarm();
            //定义一个火情处理类对象，并将源类对象作为参数传递给这个对象
            //FireHandlerClass myFireHandler1 = new FireHandlerClass(myFireAlarm);
            FireWatcherClass myFireHandle2 = new FireWatcherClass(this,myFireAlarm);
            //发生一种火情，以事件机制执行
            myFireAlarm.ActivateFireAlarm("Kitchen", 3);
            myFireAlarm.ActivateFireAlarm("Kitchen", 6);

            OutStr.sw.Close();
        }

        //信息输出类
        static class OutStr
        {
            public static StreamWriter sw;
        }

        //事件参数类
        public class FireEventArgs : EventArgs
        {
            public FireEventArgs(string room, int ferocity)
            {
                this.room = room;
                this.ferocity = ferocity;
            }
            public string room; //火情发生地
            public int ferocity; //火情凶猛程度
        }

        //事件源（发起者）类定义
        public class FireAlarm
        {
            //将火情处理定义为FireEventHandler 代理(delegate) 类型，这个代理声明的事件的参数列表
            public delegate void FireEventHandler(object sender, FireEventArgs fe);
            //定义FireEvent 为FireEventHandler delegate 事件(event) 类型.
            public event FireEventHandler FireEvent;

            //激活事件的方法，创建了FireEventArgs 对象，发起事件，并将事件参数对象传递过去
            public void ActivateFireAlarm(string room, int ferocity)
            {
                FireEventArgs fireArgs = new FireEventArgs(room, ferocity);
                //执行对象事件处理函数指针，必须保证处理函数要和声明代理时的参数列表相同
                FireEvent(this, fireArgs);
            }
        }

        //用于处理事件的类1：FireHandlerClass，这个类定义了实际事件处理代码
        class FireHandlerClass
        {
            private C5_SY1 parent;
            //事件处理类的构造函数使用事件源类作为参数
            public FireHandlerClass(C5_SY1 _parent,FireAlarm fireAlarm)
            {
                parent = _parent;
                //将事件处理的代理(函数指针) 添加到FireAlarm 类的FireEvent 事件中，当事件发生时，
                //就会执行指定的函数；
                fireAlarm.FireEvent += new FireAlarm.FireEventHandler(ExtinguishFire);
            }
            //当起火事件发生时，用于处理火情的事件
    
            void ExtinguishFire(object sender, FireEventArgs fe)
            {
                OutStr.sw.WriteLine(" {0} 对象调用，灭火事件ExtinguishFire 函数.", sender.ToString());
                parent.showComment(sender.ToString() + " 对象调用，灭火事件ExtinguishFire 函数.");
                //根据火情状况，输出不同的信息.
                if (fe.ferocity < 2) { 
                    OutStr.sw.WriteLine(" 火情发生在{0}，主人浇水后火情被扑灭了",fe.room);
                    parent.showComment(" 火情发生在 " + fe.room + "，主人浇水后火情被扑灭了");
                }
                else if (fe.ferocity < 5) {
                    OutStr.sw.WriteLine(" 主人正在使用灭火器处理{0} 火势.", fe.room);
                    parent.showComment(" 主人正在使用灭火器处理 " + fe.room + " 火势.");
                }
                else { 
                    OutStr.sw.WriteLine("{0} 的火情无法控制，主人打119!", fe.room);
                    parent.showComment(fe.room + " 的火情无法控制，主人打119!");
                }
            }
        }

        //用于处理事件的类2：FireWatcherClass
        class FireWatcherClass
        {
            private C5_SY1 parent;

            //事件处理类的构造函数使用事件源类作为参数
            public FireWatcherClass(C5_SY1 _parent, FireAlarm fireAlarm)
            {
                parent = _parent;
                //将事件处理的代理(函数指针) 添加到FireAlarm 类的FireEvent 事件中，当事件发生
                //时，就会执行指定的函数；
                fireAlarm.FireEvent += new FireAlarm.FireEventHandler(WatchFire);
            }
            //当起火事件发生时，用于处理火情的事件
            void WatchFire(object sender, FireEventArgs fe)
            {
                OutStr.sw.WriteLine(" {0} 对象调用，群众发现火情WatchFire 函数.", sender.ToString());
                parent.showComment(sender.ToString() + " 对象调用，群众发现火情WatchFire 函数.");
                //根据火情状况，输出不同的信息.
                if (fe.ferocity < 2) { 
                    OutStr.sw.WriteLine(" 群众察到火情发生在{0}，主人浇水后火情被扑灭了", fe.room);
                    parent.showComment(" 群众察到火情发生在 " + fe.room + "，主人浇水后火情被扑灭了");
                }
                else if (fe.ferocity < 5) { 
                    OutStr.sw.WriteLine(" 群众察到火情发生在{0}，群众帮助主人{0} 火势.", fe.room);
                    parent.showComment(" 群众察到火情发生在 " + fe.room + "，群众帮助主人{0} 火势.");
                }
                else { 
                    OutStr.sw.WriteLine(" 群众无法控制{0} 的火情，消防官兵来到!", fe.room);
                    parent.showComment(" 群众无法控制 " + fe.room + " 的火情，消防官兵来到!");
                }
            }
        }



    }
}
