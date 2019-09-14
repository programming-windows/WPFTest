
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

using dll_csharp;

namespace WPFTest.UI.Chapter3
{
    /// <summary>
    /// C2_SY1.xaml 的交互逻辑
    /// </summary>
    public partial class C3_SY1 : ChildPage
    {
        public C3_SY1()
        {
            InitializeComponent();

        }

        private void btn1_Click_1(object sender, RoutedEventArgs e)
        {
            string strText1 = textBox1.Text.Trim();
            string strText2 = textBox2.Text.Trim();
            string ret = ComTest.add("D61A457C-DBEF-43DE-80F4-394703BD3D41", "Simulation Transaction",int.Parse(strText1), int.Parse(strText2));
            textBox3.Text = String.Concat(ret);
        }

        private void btn2_Click_1(object sender, RoutedEventArgs e)
        {
            string strText1 = textBox5.Text.Trim();
            string strText2 = textBox6.Text.Trim();
            string ret = ComTest.multi("B218DF77-16A0-44E7-A1D7-79394D0EA674", "User Transaction", int.Parse(strText1), int.Parse(strText2));
            textBox7.Text = String.Concat(ret);
        }
    }
}
