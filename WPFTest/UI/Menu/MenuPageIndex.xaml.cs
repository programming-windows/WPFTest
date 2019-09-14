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
    /// MenuPageIndex.xaml 的交互逻辑
    /// </summary>
    public partial class MenuPageIndex : ChildPage
    {
        public MenuPageIndex()
        {
            InitializeComponent();
        }

        private void jz_Click_1(object sender, RoutedEventArgs e)
        {
            FireNextEvent("index_jz");
        }

        private void sbdss_Click_1(object sender, RoutedEventArgs e)
        {
            FireNextEvent("index_sbdss");
        }

        private void hss_Click_1(object sender, RoutedEventArgs e)
        {
            FireNextEvent("index_hss");
        }

        private void hfw_Click_1(object sender, RoutedEventArgs e)
        {
            FireNextEvent("index_hfw");
        }

        private void shdw_Click_1(object sender, RoutedEventArgs e)
        {
            FireNextEvent("index_shdw");
        }

        private void shry_Click_1(object sender, RoutedEventArgs e)
        {
            FireNextEvent("index_shry");
        }

        private void ChildPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            FireNextEvent("index_jz");
        }
    }
}
