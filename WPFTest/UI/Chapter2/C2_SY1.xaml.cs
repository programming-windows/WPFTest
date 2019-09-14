using dll_csharp;
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

namespace WPFTest.UI.Chapter2
{
    /// <summary>
    /// C2_SY1.xaml 的交互逻辑
    /// </summary>
    public partial class C2_SY1 : ChildPage
    {
        public C2_SY1()
        {
            InitializeComponent();

        }

        private void btn1_Click_1(object sender, RoutedEventArgs e)
        {
            string strText = textBox1.Text.Trim();
            long ret = DLLCsharp.FactorialF(long.Parse(strText));
            textBox2.Text = String.Concat(ret);
        }

        private void btn2_Click_1(object sender, RoutedEventArgs e)
        {
            string strText = textBox3.Text.Trim();
            long ret = DLLCsharp.FibonacciF(long.Parse(strText));
            textBox4.Text = String.Concat(ret);
        }

        private void btn3_Click_1(object sender, RoutedEventArgs e)
        {
            listBox1.Items.Clear();

            //DLL所在的绝对路径 
            Assembly assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "dll_csharp.dll");
            //注意写法：程序集.类名  
            Type type = assembly.GetType("dll_csharp.DLLCsharp");

            System.Reflection.MethodInfo[] ms = type.GetMethods();
            foreach (System.Reflection.MethodInfo methodInfo in ms) {

                string method = "";
                method += methodInfo.ReturnType.Name;
                method += " " + methodInfo.Name;
                System.Reflection.ParameterInfo[] ps = methodInfo.GetParameters();
                int count = 0;
                method += "(";
                foreach (System.Reflection.ParameterInfo parameter in ps)
                {
                    if (count == 0)
                        method += parameter.ParameterType.Name + " " + parameter.Name;
                    else
                        method += "," + parameter.ParameterType.Name + " " + parameter.Name;

                    count += 1;
                }
                method += ")";

                listBox1.Items.Add(method);

                //获取类中的公共方法GetResule                                              
                //MethodInfo methed = type.GetMethod("GetResule");
                //创建对象的实例
                //object instance = System.Activator.CreateInstance(type);
                //执行方法  new object[]为方法中的参数
                //object result = methodInfo.Invoke(instance, new object[] { });
            }

                

        }
    }
}
