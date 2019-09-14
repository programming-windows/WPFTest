using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFTest.UI
{
    /// <summary>
    /// YesOrNoDialog.xaml 的交互逻辑
    /// </summary>
    public partial class YesOrNoDialog : Window
    {
        public YesOrNoDialog()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public void SetInfo(String s)
        {
            infoTextBlock.Text = s;
        }

        private void okBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = true;
        }

        private void noBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = false;
        }

        private void okBorder_TouchDown(object sender, TouchEventArgs e)
        {
            this.DialogResult = true;
        }

        private void noBorder_TouchDown(object sender, TouchEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
