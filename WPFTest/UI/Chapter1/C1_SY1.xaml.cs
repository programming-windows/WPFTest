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

using Microsoft.International.Converters.PinYinConverter;
using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;
using DotNetSpeech;

namespace WPFTest.UI.Chapter1
{
    /// <summary>
    /// C1_SY1.xaml 的交互逻辑
    /// </summary>
    public partial class C1_SY1 : ChildPage
    {
        public C1_SY1()
        {
            InitializeComponent();
        }


        private Boolean checkInput()
        {
            if (textBox.Text.Trim().Length == 0)
            {
                MessageBox.Show("输入不能为空");
                return false;
            }
            return true;
        }

        private void btn1_Click_1(object sender, RoutedEventArgs e)
        {
            if (!checkInput())
                return;

            listBox.Items.Clear();

            char one_char = textBox.Text.Trim().ToCharArray()[0];
            int ch_int = (int)one_char;
            string str_char_int = string.Format("{0}", ch_int);
            if (ch_int > 127)
            {
                ChineseChar chineseChar = new ChineseChar(one_char);
                System.Collections.ObjectModel.ReadOnlyCollection<string> pinyin = chineseChar.Pinyins;
                string pin_str = "";
                foreach (string pin in pinyin)
                {
                    listBox.Items.Add(pin);
                    pin_str += pin + "\r\n";
                }
            }
        }

        private void btn2_Click_1(object sender, RoutedEventArgs e)
        {
            if (!checkInput())
                return;

            listBox.Items.Clear();

            String t = ChineseConverter.Convert(textBox.Text.Trim(),
            ChineseConversionDirection.TraditionalToSimplified);
            listBox.Items.Add(t);
        }

        private void btn3_Click_1(object sender, RoutedEventArgs e)
        {
            if (!checkInput())
                return;

            listBox.Items.Clear();

            String t = ChineseConverter.Convert(textBox.Text.Trim(),
            ChineseConversionDirection.SimplifiedToTraditional);
            listBox.Items.Add(t);
        }

        private void btn4_Click_1(object sender, RoutedEventArgs e)
        {
            if (!checkInput())
                return;

            SpeechVoiceSpeakFlags spFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
            SpVoice voice = new SpVoice();
            voice.Speak(textBox.Text.Trim(), spFlags);
        }
    }
}
