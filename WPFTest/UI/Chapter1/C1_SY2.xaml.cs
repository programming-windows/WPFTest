using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTest.UI.Chapter1
{
    /// <summary>
    /// C1_SY2.xaml 的交互逻辑
    /// </summary>
    public partial class C1_SY2 : ChildPage
    {

        string folder_path = "";
        string[] folder_files;

        string dest_file = "";

        public C1_SY2()
        {
            InitializeComponent();
        }

        private void selectDirectory(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            folder_path = m_Dialog.SelectedPath.Trim();
            label1.Content = folder_path;
        }

        private void searchFiles(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(folder_path))//检查文件目录是否存在
            {
                //搜索给定字符串的文件
                folder_files = Directory.GetFiles(folder_path,
                         textBox.Text, SearchOption.AllDirectories);
                listBox.Items.Clear();
                int selected_index = 0;
                foreach (string folder_file in folder_files)
                {
                    selected_index = listBox.Items.Add(folder_file);
                    listBox.SelectedIndex = selected_index;
                }
            }

        }

        private void addFiles(object sender, RoutedEventArgs e)
        {
            
            foreach (string item in listBox.Items)
            {
                if (!listBox2.Items.Contains(item))
                    listBox2.Items.Add(item);
            }
        }

        private void clearFiles(object sender, RoutedEventArgs e)
        {
            listBox2.Items.Clear();
        }

        private void moveUp(object sender, RoutedEventArgs e)
        {
            int sel_index = listBox2.SelectedIndex;
            string sel_str = listBox2.SelectedItem.ToString();
            if (sel_index > 0)
            {
                //将当前选中的项与前一项交换，并交换列表框的选中序号
                listBox2.Items[sel_index] = listBox2.Items[sel_index - 1];
                listBox2.Items[sel_index - 1] = sel_str;
                listBox2.SelectedIndex = sel_index - 1;
            }

        }

        private void moveDown(object sender, RoutedEventArgs e)
        {
            int sel_index = listBox2.SelectedIndex;
            string sel_str = listBox2.SelectedItem.ToString();
            if (sel_index < listBox2.Items.Count -1)
            {
                //将当前选中的项与下一项交换，并交换列表框的选中序号
                listBox2.Items[sel_index] = listBox2.Items[sel_index + 1];
                listBox2.Items[sel_index + 1] = sel_str;
                listBox2.SelectedIndex = sel_index + 1;
            }
        }

        private void openFile(object sender, RoutedEventArgs e)
        {

        }

        private void nameTargetFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog m_Dialog = new SaveFileDialog();
            m_Dialog.Title = "选择要合并后的文件";
            m_Dialog.InitialDirectory = System.Environment.SpecialFolder.DesktopDirectory.ToString();
            m_Dialog.OverwritePrompt = false;
            if (m_Dialog.ShowDialog() == DialogResult.OK)
            {
                dest_file = m_Dialog.FileName;
                label3.Content = dest_file;
            }

        }

        private void mergeFiles(object sender, RoutedEventArgs e)
        {
            if (dest_file==null || "".Equals(dest_file))
            {
                System.Windows.MessageBox.Show("请选择保存文件名称");
                return;
            }

            if (File.Exists(dest_file))
            {
                File.Delete(dest_file);
            }
            FileStream fs_dest = new FileStream(dest_file, FileMode.CreateNew, FileAccess.Write);
            byte[] DataBuffer = new byte[100000];
            byte[] file_name_buf;
            //int file_name_len=0;
            FileStream fs_source = null;
            int read_len;
            FileInfo fi_a = null;

            foreach (string item in listBox2.Items)
            {
                fi_a = new FileInfo(item);
                file_name_buf = Encoding.Default.GetBytes(fi_a.Name);
                //写入文件名
                fs_dest.Write(file_name_buf, 0, file_name_buf.Length);
                //换行
                fs_dest.WriteByte((byte)13);
                fs_dest.WriteByte((byte)10);

                //写入数据
                fs_source = new FileStream(fi_a.FullName, FileMode.Open, FileAccess.Read);
                read_len = fs_source.Read(DataBuffer, 0, 100000);
                while (read_len > 0)
                {
                    fs_dest.Write(DataBuffer, 0, read_len);
                    read_len = fs_source.Read(DataBuffer, 0, 100000);
                }

                //换行
                fs_dest.WriteByte((byte)13);
                fs_dest.WriteByte((byte)10);
                fs_source.Close();

            }

            fs_source.Dispose();
            fs_dest.Flush();
            fs_dest.Close();
            fs_dest.Dispose();
            Process.Start(dest_file);
            
        }
    }
}
