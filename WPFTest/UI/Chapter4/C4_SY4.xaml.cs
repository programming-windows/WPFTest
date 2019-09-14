
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
using System.Threading;

namespace WPFTest.UI.Chapter4
{
    /// <summary>
    /// C2_SY1.xaml 的交互逻辑
    /// </summary>
    public partial class C4_SY4 : ChildPage
    {

       
        public C4_SY4()
        {
            InitializeComponent();
           
        }

        public C4_SY4(MainWindow parent)
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

        //定义回调
        private delegate void updateDelegate(string comment);
        public void update(string comment)
        {
            //showComment((string)comment);
            if (!listBox1.Dispatcher.CheckAccess())
            {
                //声明，并实例化回调
                updateDelegate d = update;
                //使用回调
                listBox1.Dispatcher.Invoke(d, comment);
            }
            else
            {
                showComment(comment);
            }

        }

        //定义回调
        private delegate void setTextValueCallBack(String comment);
        //声明回调
        private setTextValueCallBack setCallBack;


        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            clearComments();

            //获取正在运行的线程
            Thread thread = Thread.CurrentThread;
            //设置线程的名字
            thread.Name = "主线程";
            //获取当前线程的唯一标识符
            int id = thread.ManagedThreadId;
            //获取当前线程的状态
            System.Threading.ThreadState state = thread.ThreadState;
            //获取当前线程的优先级
            ThreadPriority priority = thread.Priority;
            string strMsg = string.Format("Thread ID:{0}\n" + "Thread Name:{1}\n" +
                "Thread State:{2}\n" + "Thread Priority:{3}\n", id, thread.Name,
                state, priority);

            Console.WriteLine(strMsg);
            //Console.ReadKey();

            showComment(strMsg);

        }

        /// <summary>
        /// 创建无参的方法
        /// </summary>
        void Thread1()
        {
            string strMsg = "这是无参的方法";
            Console.WriteLine(strMsg);
            update(strMsg);

            //使用回调
            //listBox1.Dispatcher.Invoke(setCallBack, comment);
        }

        class ThreadTest
        {
            private C4_SY4 parent;
            public ThreadTest(C4_SY4 _parent)
            {
                parent = _parent;
            }

            public void Thread2()
            {
                string strMsg = "这是一个个实例方法";
                Console.WriteLine(strMsg);
                //showComment(strMsg);
                parent.update(strMsg);
            }
        }

        void Thread5(object obj)
        {
            Console.WriteLine(obj);
            update((string)obj);
        }


        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            clearComments();

            //实例化回调
            setCallBack = new setTextValueCallBack(showComment);

            //创建无参的线程
            Thread thread1 = new Thread(new ThreadStart(Thread1));
            //调用Start方法执行线程
            thread1.Start();

            //创建ThreadTest类的一个实例
            ThreadTest test = new ThreadTest(this);
            //调用test实例的MyThread方法
            Thread thread2 = new Thread(new ThreadStart(test.Thread2));
            //启动线程
            thread2.Start();

            //通过匿名委托创建
            Thread thread3 = new Thread(delegate () { update("我是通过匿名委托创建的线程"); });
            thread3.Start();
            //通过Lambda表达式创建
            Thread thread4 = new Thread(() => update("我是通过Lambda表达式创建的委托"));
            thread4.Start();


            //通过ParameterizedThreadStart创建线程
            Thread thread5 = new Thread(new ParameterizedThreadStart(Thread5));
            //给方法传值
            thread5.Start("这是一个有参数的委托");
            
            //Console.ReadKey();
        }


        class BackGroundTest
        {
            private C4_SY4 parent;
            private int Count;
            public BackGroundTest(C4_SY4 _parent,int count)
            {
                this.parent = _parent;
                this.Count = count;
            }
            public void RunLoop()
            {
                //获取当前线程的名称
                string threadName = Thread.CurrentThread.Name;
                for (int i = 0; i < Count; i++)
                {
                    Console.WriteLine("{0}计数：{1}", threadName, i.ToString());
                    parent.update(threadName.ToString() + "计数:" + i.ToString());
                    //线程休眠500毫秒
                    Thread.Sleep(1000);
                }
                Console.WriteLine("{0}完成计数", threadName);
                parent.update(threadName.ToString() + "完成计数");
            }
        }

        //演示前台线程先结束，会自动结束后台线程（没有执行完的后台线程，不继续执行）
        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            clearComments();

            //演示前台、后台线程
            BackGroundTest background = new BackGroundTest(this,10);
            //创建前台线程
            Thread fThread = new Thread(new ThreadStart(background.RunLoop));
            //给线程命名
            fThread.Name = "前台线程";


            BackGroundTest background1 = new BackGroundTest(this,20);
            //创建后台线程
            Thread bThread = new Thread(new ThreadStart(background1.RunLoop));
            bThread.Name = "后台线程";
            //设置为后台线程
            bThread.IsBackground = true;

            //启动线程
            fThread.Start();
            bThread.Start();

        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            clearComments();

            //演示前台、后台线程
            BackGroundTest background = new BackGroundTest(this, 10);
            //创建前台线程
            Thread fThread = new Thread(new ThreadStart(background.RunLoop));
            //给线程命名
            fThread.Name = "前台线程";


            BackGroundTest background1 = new BackGroundTest(this, 20);
            //创建后台线程
            Thread bThread = new Thread(new ThreadStart(background1.RunLoop));
            bThread.Name = "后台线程";
            //设置为后台线程
            //bThread.IsBackground = true;

            //启动线程
            fThread.Start();
            bThread.Start();
        }

        private void threadMethod6(Object obj)
        {
            Thread.Sleep(Int32.Parse(obj.ToString()));
            Console.WriteLine(obj + "毫秒任务结束");
            update(obj + "毫秒任务结束");
        }
        private void joinAllThread6(object obj)
        {
            Thread[] threads = obj as Thread[];
            foreach (Thread t in threads)
                t.Join();
            Console.WriteLine("所有的线程结束");
            update("所有的线程结束");
        }


        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            clearComments();

            Thread thread1 = new Thread(threadMethod6);
            Thread thread2 = new Thread(threadMethod6);
            Thread thread3 = new Thread(threadMethod6);

            thread1.Start(3000);
            thread2.Start(5000);
            thread3.Start(7000);

            Thread joinThread = new Thread(joinAllThread6);
            joinThread.Start(new Thread[] { thread1, thread2, thread3 });
        }


        private void DoSomethingLong(string name)
        {

            Console.WriteLine($"****************DoSomethingLong {name} Start {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}***************");
            update($"****************DoSomethingLong " + name + " Start " + Thread.CurrentThread.ManagedThreadId.ToString("00") + " " +  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "***************");
            long lResult = 0;
            for (int i = 0; i < 1000000000; i++)
            {
                lResult += i;
            }
            Console.WriteLine($"****************DoSomethingLong {name}   End {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} {lResult}***************");
            update($"****************DoSomethingLong " + name + " End " + Thread.CurrentThread.ManagedThreadId.ToString("00") + "  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " " + lResult + "***************");

        }

        //线程同步方法
        private void btnSync_Click(object sender, RoutedEventArgs e)
        {
            clearComments();

            Console.WriteLine($"****************btnSync_Click Start {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}***************");
            update($"****************btnSync_Click Start " + Thread.CurrentThread.ManagedThreadId.ToString("00")  +  " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "***************");
            int j = 3;
            int k = 5;
            int m = j + k;
            for (int i = 0; i < 5; i++)
            {
                string name = string.Format($"btnSync_Click_{i}");
                this.DoSomethingLong(name);
            }
            Console.WriteLine($"****************btnSync_Click End {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}***************");
            update($"****************btnSync_Click End " + Thread.CurrentThread.ManagedThreadId.ToString("00") + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "***************");


        }

        //线程异步方法
        private void btnAsync_Click(object sender, RoutedEventArgs e)
        {
            clearComments();

            Console.WriteLine($"***************btnAsync_Click Start {Thread.CurrentThread.ManagedThreadId}");
            update($"***************btnAsync_Click Start " + Thread.CurrentThread.ManagedThreadId);

            Action<string> action = this.DoSomethingLong;
            // 调用委托(同步调用)
            //action.Invoke("btnAsync_Click_1");

            // 异步调用委托
            //action.BeginInvoke("btnAsync_Click_2", null, null);

            for (int i = 0; i < 5; i++)
            {
                string name = string.Format($"btnSync_Click_{i}");
                action.BeginInvoke(name, null, null);
            }

            Console.WriteLine($"***************btnAsync_Click End    {Thread.CurrentThread.ManagedThreadId}");
            update($"***************btnAsync_Click End " + Thread.CurrentThread.ManagedThreadId);

        }

        //线程异步的回调方法，实现同步
        private void btnAsyncAdvanced_Click(object sender, RoutedEventArgs e)
        {
            clearComments();

            Console.WriteLine($"***************btnAsyncAdvanced_Click Start {Thread.CurrentThread.ManagedThreadId}");
            update($"***************btnAsyncAdvanced_Click Start " + Thread.CurrentThread.ManagedThreadId);

            Action<string> action = this.DoSomethingLong;

            IAsyncResult asyncResult = null;

            // 定义一个回调，其中lamda表达式中的p为参数
            AsyncCallback callback = p =>
            {
                Console.WriteLine($"到这里计算已经完成了。{Thread.CurrentThread.ManagedThreadId.ToString("00")}。");
                update($"到这里计算已经完成了。" + Thread.CurrentThread.ManagedThreadId.ToString("00") + "。");
            };



            for (int i = 0; i < 5; i++)
            {
                string name = string.Format($"btnSync_Click_{i}");
                asyncResult = action.BeginInvoke(name, callback, null);
            }

            Console.WriteLine($"***************btnAsync_Click End    {Thread.CurrentThread.ManagedThreadId}");
            update($"***************btnAsync_Click End " + Thread.CurrentThread.ManagedThreadId);

        }


        AutoResetEvent event1 = new AutoResetEvent(false);
        AutoResetEvent event2 = new AutoResetEvent(false);
        AutoResetEvent event3 = new AutoResetEvent(false);
        public void Method1()
        {
            update("thread1 begin");
            Thread.Sleep(1000);
            update("thread1 end");
            event1.Set();
        }

        public void Method2()
        {
            update("thread2 begin");
            Thread.Sleep(2000);
            update("thread2 end");
            event2.Set();
        }

        public void Method3()
        {
            update("thread3 begin");
            Thread.Sleep(3000);
            update("thread3 end");
            event3.Set();
        }
        private void BtbAutoResetEvent(object sender, RoutedEventArgs e)
        {
            clearComments();

            Thread vThread1 = new Thread(new ThreadStart(Method1));
            Thread vThread2 = new Thread(new ThreadStart(Method2));
            Thread vThread3 = new Thread(new ThreadStart(Method3));

            AutoResetEvent[] vEventInProgress = new AutoResetEvent[3]
            {
                event1,
                event2,
                event3
            };

            vThread1.Start();
            vThread2.Start();
            vThread3.Start();

            WaitHandle.WaitAny(vEventInProgress, 8000);
            update("current thread end;");

            if (vThread1 != null)
                vThread1.Abort();
            if (vThread2 != null)
                vThread2.Abort();
            if (vThread3 != null)
                vThread3.Abort();
        }


        ManualResetEvent event4 = new ManualResetEvent(false);
        ManualResetEvent event5 = new ManualResetEvent(false);
        ManualResetEvent event6 = new ManualResetEvent(false);
        public void Method4()
        {
            //event4.Reset();
            update("thread4 begin");
            //Thread.Sleep(1000);
            update("thread4 end");
            event4.Set();
        }

        public void Method5()
        {
            //event5.Reset();
            update("thread5 begin");
            //Thread.Sleep(2000);
            update("thread5 end");
            event5.Set();
        }

        public void Method6()
        {
            //event6.Reset();
            update("thread6 begin");
            //Thread.Sleep(3000);
            update("thread6 end");
            event6.Set();
        }


        private void waitOneTest()
        {
            ManualResetEvent mansig;
            mansig = new ManualResetEvent(true);

            bool state = mansig.WaitOne(9000, true);
            Console.WriteLine("ManualResetEvent After WaitOne" + state);
            if (!state)
                update("ManualResetEvent After WaitOne " + state);

            mansig.Reset();
            if (!mansig.WaitOne(9000, true)) { 
                Console.WriteLine("ManualResetEvent After WaitOne" + state);
                update("ManualResetEvent After WaitOne " + state);
            }

            mansig.Set();
            state = mansig.WaitOne(9000, true);
            Console.WriteLine("ManualResetEvent After WaitOne" + state);
            if (!state)
                update("ManualResetEvent After WaitOne " + state);
        }

        private void waitAnyTest()
        {
            Thread vThread1 = new Thread(new ThreadStart(Method4));
            Thread vThread2 = new Thread(new ThreadStart(Method5));
            Thread vThread3 = new Thread(new ThreadStart(Method6));

            ManualResetEvent[] vEventInProgress = new ManualResetEvent[3]
            {
                event4,
                event5,
                event6
            };

            vThread1.Start();
            vThread2.Start();
            vThread3.Start();

            int index = WaitHandle.WaitAny(vEventInProgress, 5000);
            update("current thread end;");
            /*
            while (index != 0)
            {
                if (index == 1)
                {
                    update("current thread end;");

                    if (vThread1 != null)
                        vThread1.Abort();
                    if (vThread2 != null)
                        vThread2.Abort();
                    if (vThread3 != null)
                        vThread3.Abort();
                }

                index = ManualResetEvent.WaitAny(vEventInProgress, 1000);
            }
            */
        }

        private void BtbManualResetEvent(object sender, RoutedEventArgs e)
        {
            clearComments();

            waitAnyTest();

        }

        private void produceConsume(object sender, RoutedEventArgs e)
        {
            /*
            ProducerConsumer pc = new ProducerConsumer(this);
            ProducerConsumer.SharedBuffer = 20;
            ProducerConsumer.mut = new Mutex(false, "Tr");
            ProducerConsumer.threadVec = new Thread[2];
            ProducerConsumer.threadVec[0] = new Thread(new ThreadStart(pc.Consumer));
            ProducerConsumer.threadVec[1] = new Thread(new ThreadStart(pc.Producer));
            ProducerConsumer.threadVec[0].Start();
            ProducerConsumer.threadVec[1].Start();
            ProducerConsumer.threadVec[0].Join();
            ProducerConsumer.threadVec[1].Join();
            */
        }


        public class ProducerConsumer
        {
            public static Mutex mut;
            public static Thread[] threadVec;
            public static int SharedBuffer;

            private C4_SY4 parent;
            public ProducerConsumer(C4_SY4 _parent)
            {
                parent = _parent;
            }

            public void Consumer()
            {
                while (true)
                {
                    int result;
                    mut.WaitOne();
                    if (SharedBuffer == 0)
                    {
                        Console.WriteLine("Consumed {0}: end of data\r\n", SharedBuffer);
                        parent.update("Consumed " + SharedBuffer + " : end of data\r\n");
                        mut.ReleaseMutex();
                        break;
                    }
                    if (SharedBuffer > 0)
                    { // ignore negative values
                        result = SharedBuffer;
                        Console.WriteLine("Consumed: {0}", result);
                        parent.update("Consumed: " + result);
                        mut.ReleaseMutex();
                    }
                }
            }

            public void Producer()
            {
                int i;
                for (i = 20; i >= 0; i--)
                {
                    mut.WaitOne();
                    Console.WriteLine("Produce: {0}", i);
                    parent.update("Produce: "  + i);
                    SharedBuffer = i;
                    mut.ReleaseMutex();
                    Thread.Sleep(1000);
                }
            }
        }
        
        
    }
}
