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
using System.Windows.Shapes;
using System.Windows.Navigation;

using Microsoft.Win32;

using Models.Entity;
using Utils;

namespace WPFTest.UI.Chapter6
{
    /// <summary>
    /// FgEdit.xaml 的交互逻辑
    /// </summary>
    public partial class FileEdit : Window
    {
        private FileListPage parentPage = null;
        private String idKey = "";

        public FileEdit()
        {
            InitializeComponent();
        }

        public FileEdit(FileListPage _parent)
        {
            
            InitializeComponent();
            parentPage = _parent;

            //this.fileNo.DataContext = parentPage.currentItem;
            //this.publishOrg.DataContext = parentPage.currentItem;
            //this.subject.DataContext = parentPage.currentItem;
        }

        public void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            loadData();
        }

        public void loadData() { 
            if (parentPage.currentItem == null)
            {
                parentPage.currentItem = new TblFgwj()
                {
                    entityState = EntityState.NEW
                };

            }

            if (MyStringUtil.isEmpty(parentPage.currentItem.idKey))
            {
                parentPage.currentItem.entityState = EntityState.NEW;
                idKey = MyStringUtil.getGuidStr22();
                parentPage.currentItem.idKey = idKey;
            }
            else
            {
                idKey = parentPage.currentItem.idKey;
            }

            this.grid1.DataContext = parentPage.currentItem;


            if (parentPage.currentItem.entityState == EntityState.NEW)
            {
                this.Title = "新建";
                this.btnDownload.Visibility = Visibility.Hidden;
            }
            else
            {
                this.Title = "编辑";
                if (MyStringUtil.isEmpty(parentPage.currentItem.fileName))
                    this.btnDownload.Visibility = Visibility.Hidden;
                else
                    this.btnDownload.Visibility = Visibility.Visible;

            }

        }

        private void upload_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();                        //弹出打开文件窗口类  

            fileDialog.Title = "选择要添加的文件";
            fileDialog.Multiselect = false;
            fileDialog.Filter = "Pdf Files (*.pdf)|*.pdf|Doc Files (*.doc)|*.doc|Docx Files (*.docx)|*.docx";
            fileDialog.FilterIndex = 1;
            fileDialog.FileName = string.Empty;
            fileDialog.RestoreDirectory = true;
            Nullable<bool> result = fileDialog.ShowDialog();
            if (result == false)
            {
                return;
            }
            //打开文件的路径
            string srcFileName = fileDialog.FileName;                                 
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "Files";
                //文件路径，AppDomain.CurrentDomain.BaseDirectory为程序所在位置,data.txt为查找的目标文件
                if (!System.IO.Directory.Exists(path))//查看文件夹是否存在
                {
                    System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(path);
                    directoryInfo.Create();
                }

                string fileType = System.IO.Path.GetExtension(srcFileName).Substring(1);
                string fileName = idKey + "." + fileType;
                string destFileName = path + System.IO.Path.DirectorySeparatorChar + fileName;
                System.IO.File.Copy(srcFileName, destFileName, true);
                MessageBox.Show(System.IO.Path.GetFileName(srcFileName) + "上传成功!");


                if (parentPage.currentItem.entityState == EntityState.NEW)
                {
                    insertFileInfo(fileType, fileName);
                    parentPage.currentItem.entityState = EntityState.NONE;
                }
                else
                {
                    updateFileInfo(fileType, fileName);
                    parentPage.currentItem.entityState = EntityState.NONE;
                }
            }
            catch (Exception exception)
            {
                Log.Write(exception.Message);
            }
        }

        private void download_Click_1(object sender, RoutedEventArgs e)
        {
            if (parentPage.currentItem == null || MyStringUtil.isEmpty(parentPage.currentItem.idKey))
            {
                MessageBox.Show("请先上传文件");
                return;
            }
            
            string fileType = parentPage.currentItem.fileType;
            string fileName = idKey + "." + fileType;
            string path = AppDomain.CurrentDomain.BaseDirectory + "Files";
            string srcFileName = path + System.IO.Path.DirectorySeparatorChar + fileName;
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "保存为";
            //saveFileDialog.Filter = "Pdf Files (*.pdf)|*.pdf|Doc Files (*.doc)|*.doc|Docx Files (*.docx)|*.docx";
            saveFileDialog.Filter = "Doc Files (*.doc)|*.doc";
            saveFileDialog.FileName = String.Empty;
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.DefaultExt = "doc";
            Nullable<bool> result = saveFileDialog.ShowDialog();
            if (result == false)
                return; 

            try
            {
                string destFileName = saveFileDialog.FileName;
                /*
                if (System.IO.File.Exists(destFileName))
                {
                    MessageBox.Show(System.IO.Path.GetFileName(destFileName) + "已存在，请重新输入目标文件名称!");
                    return;
                }*/
                System.IO.File.Copy(srcFileName, destFileName, true);
                MessageBox.Show(System.IO.Path.GetFileName(destFileName) + "下载成功!");
            }
            catch (Exception exception)
            {
                Log.Write(exception.Message);
            }

        }

        private int insertFileInfo(string fileType, string fileName)
        {
            string[] updatedColumns = new string[3]
            {
                "FILE_TYPE",
                "FILE_NAME",
                "ID_KEY"
            };

            string[] updatedValues = new string[3]
            {
                fileType,
                fileName,
                idKey
            };

            parentPage.currentItem.fileType = fileType;
            parentPage.currentItem.fileName = fileName;
            return parentPage.sqlite.InsertValues("tbl_fgwj", updatedColumns, updatedValues);
        }

        private int updateFileInfo(string fileType, string fileName)
        {
            string[] updatedColumns = new string[2]
            {
                "FILE_TYPE",
                "FILE_NAME"
            };

            string[] updatedValues = new string[2]
            {
                fileType,
                fileName
            };
            parentPage.currentItem.fileType = fileType;
            parentPage.currentItem.fileName = fileName;
            return parentPage.sqlite.UpdateValues("tbl_fgwj", updatedColumns, updatedValues, "ID_KEY", idKey, "=");
        }


        private int insertOrUpdateItem()
        {
            
            string[] updatedColumns = new string[9]
            {
                "FILE_TYPE",
                "FILE_NAME",
                "ID_KEY",
                "FILE_NO",
                "SUBJECT",
                "PUBLISH_DATE",
                "IMPLEMENT_DATE",
                "PUBLISH_ORG",
                "FLAG"
            };

            string[] updatedValues = new string[9]
            {
                parentPage.currentItem.fileType,
                parentPage.currentItem.fileName,
                parentPage.currentItem.idKey,
                parentPage.currentItem.fileNo==null?this.fileNo.Text.Trim():parentPage.currentItem.fileNo,
                parentPage.currentItem.subject==null?this.subject.Text.Trim():parentPage.currentItem.subject,
                parentPage.currentItem.publishDate==null?this.publishDate.Text.Trim():parentPage.currentItem.publishDate,
                parentPage.currentItem.implementDate==null?this.implementDate.Text.Trim():parentPage.currentItem.implementDate,
                parentPage.currentItem.publishOrg==null?this.publishOrg.Text.Trim():parentPage.currentItem.publishOrg,
                parentPage.currentItem.flag==null?"1":parentPage.currentItem.flag

            };

            if (parentPage.currentItem.entityState == EntityState.NEW)
                return parentPage.sqlite.InsertValues("tbl_fgwj", updatedColumns, updatedValues);
            else
                return parentPage.sqlite.UpdateValues("tbl_fgwj", updatedColumns, updatedValues, "ID_KEY", idKey, "=");
        }

        private void confirm_Click_1(object sender, RoutedEventArgs e)
        {

            if (MyStringUtil.isEmpty(this.fileNo.Text.Trim()))
            {
                MessageBox.Show("文件编号不能为空");
                return;
            }

            //MessageBox.Show(subject.Text);
            //MessageBox.Show(parentPage.currentItem.subject);
            insertOrUpdateItem();

            parentPage.loadDatas();
            this.Close();
        }

        private void return_Click_1(object sender, RoutedEventArgs e)
        {
            parentPage.loadDatas();
            this.Close();
        }

       


    }
}
