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


using System.Data;
using Utils;
using Models.Entity;

namespace WPFTest.UI.Chapter6
{
    /// <summary>
    /// FileListPage.xaml 的交互逻辑
    /// </summary>
    public partial class FileListPage : ChildPage
    {
        

        public SqliteHelper sqlite = null;
        public TblFgwj currentItem  { get;set; }

        public FileListPage()
        {
            sqlite = new SqliteHelper("data source=demo.db");
            currentItem = null;
            InitializeComponent();
        }

        private void ChildPage_Unloaded(object sender, RoutedEventArgs e)
        {
            sqlite.CloseConnection();
        }

        private void ChildPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            loadDatas();
        }
         

        public void loadDatas() { 

            List<TblFgwj> items = new List<TblFgwj>();

            //读取整张表
            DataTable dataTable = sqlite.SelectFullTable("tbl_fgwj");
            if (dataTable != null)
            {
                int count = dataTable.Columns.Count;


                foreach (DataRow mDr in dataTable.Rows)
                {
                    TblFgwj item = new TblFgwj();
                    Type type = item.GetType();

                    foreach (DataColumn mDc in dataTable.Columns)
                    {
                        /*
                        string value = (mDr[mDc.ColumnName].Equals(DBNull.Value)) ? "" : (String)mDr[mDc.ColumnName];
                        MyStringUtil.SetModelValue(mDc.ColumnName, value, item);
                        */


                        string propertyName =  MyStringUtil.getCamelName(mDc.ColumnName);
                        System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);

                        if (propertyInfo != null)
                        {
                            string value = (mDr[mDc.ColumnName].Equals(DBNull.Value)) ? "" : (String)mDr[mDc.ColumnName];
                            propertyInfo.SetValue(item, value, null); //给对应属性赋值
                        }
                        
                    }

                    string[] result = ObjectUtils.GetModelProperties(item);

                    object[] result2 = ObjectUtils.GetModelValues(item);

                    /*
                    item.idKey   = (mDr["ID_KEY"].Equals(DBNull.Value)) ? "" : (String)mDr["ID_KEY"];
                    item.fileNo = (mDr["FILE_NO"].Equals(DBNull.Value)) ? "" : (String)mDr["FILE_NO"]; 
                    item.subject = (mDr["SUBJECT"].Equals(DBNull.Value)) ? "" : (String)mDr["SUBJECT"]; 
                    item.publishDate = (mDr["PUBLISH_DATE"].Equals(DBNull.Value)) ? "" : (String)mDr["PUBLISH_DATE"]; 
                    item.implementDate = (mDr["IMPLEMENT_DATE"].Equals(DBNull.Value)) ? "" : (String)mDr["IMPLEMENT_DATE"]; 
                    **/

                    item.entityState = EntityState.NONE;
                    items.Add(item);
                }
            }

            /*
            while (reader.Read())
            {
                TblFgwj item = new TblFgwj();
                item.idKey = reader.GetString(reader.GetOrdinal("ID_KEY"));
                item.fileNo = reader.GetString(reader.GetOrdinal("FILE_NO"));
                item.subject = reader.GetString(reader.GetOrdinal("SUBJECT"));
                item.publishDate = reader.GetString(reader.GetOrdinal("PUBLISH_DATE"));
                item.implementDate = reader.GetString(reader.GetOrdinal("IMPLEMENT_DATE"));    
            }
            */

            //dataGrid.ItemsSource = dataTable.DefaultView;

            dataGrid.ItemsSource = items;

            Dictionary<String, String> columnLabels = sqlite.getColumns("TBL_FGWJ");
            //dataGrid.Columns.d

            /*
            foreach (DataColumn column in dataTable.Columns){
                if (columnLabels.ContainsKey(column.ColumnName))
                    column.ColumnName = columnLabels[column.ColumnName];
            }*/
           

        }

        private void new_Click_1(object sender, RoutedEventArgs e)
        {
            currentItem = null;
            FileEdit window = new FileEdit(this);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();

           
        }

        private void edit_Click_1(object sender, RoutedEventArgs e)
        {

            TblFgwj item = (TblFgwj)dataGrid.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("请选择数据行");
                return;
            }

            currentItem = new TblFgwj();
            ObjectUtils.copyObjectValues(item, currentItem);

            FileEdit window = new FileEdit(this);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (window.ShowDialog() == true)
            {

            }
        }

        private void del_Click_1(object sender, RoutedEventArgs e)
        {
            TblFgwj item = (TblFgwj)dataGrid.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("请选择数据行");
                return;
            }

            int ret = sqlite.ExecuteNonQuery("delete from tbl_fgwj where ID_KEY='" + item.idKey + "'");
            if (ret > 0)
            {
                this.loadDatas();
                MessageBox.Show("删除成功!");
            }

        }

        
    }
}
