
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

namespace WPFTest.UI.Chapter4
{
    /// <summary>
    /// C2_SY1.xaml 的交互逻辑
    /// </summary>
    public partial class C4_SY2 : ChildPage
    {

        public static Process cmdP;
        public static StreamWriter cmdStreamInput;
        private static StringBuilder cmdOutput = null;

        
        public C4_SY2()
        {
            InitializeComponent();

        }

        public C4_SY2(MainWindow parent)
        {
            InitializeComponent();
            this.parentWindow = parent;

        }

        private void ChildPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        

        private void clearComments()
        {
            //listBox1.Items.Clear();
            textBox2.Text = "";
        }

        private void showComment(String comment)
        {
            if (MyStringUtil.isEmpty(comment)) {
                //listBox1.Items.Add("");
                return;
            }

            //listBox1.Items.Add(comment);
            textBox2.Text = comment;
        }

        private void runCMD(object sender, RoutedEventArgs e)
        {
            clearComments();
            string strCmd = "ping www.sohu.com -n 20";
            if (!MyStringUtil.isEmpty(textBox1.Text))
                strCmd = "ping " + textBox1.Text.Trim() + " -n 20";

            cmdOutput = new StringBuilder("");
            
            cmdP = new Process();
            cmdP.StartInfo.FileName = "cmd.exe";
            cmdP.StartInfo.CreateNoWindow = true;
            cmdP.StartInfo.UseShellExecute = false;
            cmdP.StartInfo.RedirectStandardOutput = true;
            cmdP.StartInfo.RedirectStandardInput = true;
            
            /*异步调用
            cmdP.OutputDataReceived += new DataReceivedEventHandler(strOutputHandler);
            cmdP.Start();

            cmdStreamInput = cmdP.StandardInput;
            cmdStreamInput.WriteLine(strCmd);
            cmdStreamInput.WriteLine("exit");
            cmdP.BeginOutputReadLine();
            */

            //同步调用
            cmdP.Start();
            cmdP.StandardInput.WriteLine(strCmd);
            cmdP.StandardInput.WriteLine("exit");
            textBox2.Text = cmdP.StandardOutput.ReadToEnd();
            cmdP.WaitForExit();
            cmdP.Close();
        }

     

        private void closeCMD(object sender, RoutedEventArgs e)
        {
            cmdP.Close();
        }
    }
}
