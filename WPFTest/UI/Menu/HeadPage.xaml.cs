using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WPFTest.UI.Menu
{
    /// <summary>
    /// Head.xaml 的交互逻辑
    /// </summary>
    public partial class HeadPage : ChildPage
    {
        public HeadPage()
        {
            InitializeComponent();
        }

        private void setting_Click_1(object sender, RoutedEventArgs e)
        {
            FireNextEvent("setting");
        }

        private void exit_Click_1(object sender, RoutedEventArgs e)
        {
            FireQuitEvent("");
        }
    }
}
