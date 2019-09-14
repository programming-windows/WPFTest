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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Collections.ObjectModel;
using System.Data;

using System.Data.SqlClient;

using System.ComponentModel;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

using Utils;

namespace WPFTest
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : Page
    {
        Dictionary<string,string> fieldsArr = new Dictionary<string, string>();
        DataTable dt = new DataTable();
        List<string> displayColumns = new List<string> { "BOOKINGNO", "PAYTIME", "GUESTID", "GUESTNAME", "ROOMTYPE", "ROOMNO", "CHECKINTIME", "CHECKOUTTIME","NIGHT", "PRICE", "TOTALPRICE" };

        SqlserverUtil dbutil = new SqlserverUtil();

        public Page1()
        {
            InitializeComponent();

            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            IniDatagridColumns();
            DatePicker1.SelectedDate = DateTime.Now;
            DatePicker2.SelectedDate = DateTime.Now;
        }

        private void IniDatagridColumns()
        {
            dt = dbutil.query(displayColumns, "and 1=2");
            SelectDisplayColumns(dt);
            dataGrid.ItemsSource = dt.DefaultView;
        }




        private void button_Click(object sender, RoutedEventArgs e)
        {
            //只选了主选项不选副选项一律输出整张表内容
            //DataTable displayData = new DataTable();


            String condition = "";
            if (DatePicker1.Text != "")
            {
                
                condition += "and PAYTIME>='" + MyDateTimeUtil.Format(Convert.ToDateTime(DatePicker1.Text + " 00:00:01"),"yyyy-MM-dd HH:mm:ss")  + "'";
            }

            if (DatePicker2.Text != "")
            {
                condition += "and PAYTIME<='" + MyDateTimeUtil.Format(Convert.ToDateTime(DatePicker2.Text + " 23:59:59"), "yyyy-MM-dd HH:mm:ss") + "'";
            }

            if (roomNo.Text != "")
            {
                condition += " and ROOMNO='" + roomNo.Text + "'";
            }

            if (guestName.Text != "")
            {
                condition += " and GUESTNAME like '%" + guestName.Text + "%'";
            }

            //System.Windows.MessageBox.Show(condition);

            if (condition.Equals(""))
            {
                System.Windows.MessageBox.Show("请输入查询条件");
                return;
            }
            
            dt = dbutil.query(displayColumns, condition);
            SelectDisplayColumns(dt);
            

            double total = 0;
            foreach (DataRow row in dt.Rows)
            {
                total += double.Parse(row["总价"].ToString());
            }
            dt = UpdateDataTable(dt);
            //dt.Columns[9].DataType = typeof(string);
            //string s = dt.Columns[10].ColumnName;
            DataRow newRow;
            newRow = dt.NewRow();
            //if (dt.Rows.Count == 0)
           // {
                newRow["入住天数"] = "汇总";
            // }
            // else { newRow["单价"] = "   总数："; }
            //newRow["总价"] = dt.Rows.Count;
            newRow["单价"] = dt.Rows.Count+"个订单";
            newRow["总价"] = "¥"+total;
            dt.Rows.Add(newRow);
                        
            dataGrid.ItemsSource = dt.DefaultView;
            setColumnsWidth();           
            
        }

        /// <summary>
        /// 要在datatable里加“总数”,对应的倒数第二列数据格式是double，所以必须先把类型转成字符串
        /// </summary>
        /// <param name="argDataTable">数据表DataTable</param>
        /// <returns>数据表DataTable</returns>  

        private DataTable UpdateDataTable(DataTable argDataTable)
        {
            DataTable dtResult = new DataTable();
            //克隆表结构
            dtResult = argDataTable.Clone();
            foreach (DataColumn col in dtResult.Columns)
            {
                // if (col.ColumnName == "单价")
                if (col.ColumnName == "入住天数"|| col.ColumnName == "单价"|| col.ColumnName == "总价")
                {
                    //修改列类型
                    col.DataType = typeof(String);
                }
            }
            
            foreach (DataRow row in argDataTable.Rows)
            {
                DataRow rowNew = dtResult.NewRow();
                rowNew["订单号"] = row["订单号"];
                //修改记录值
                rowNew["办理入住时间"] = row["办理入住时间"];
                rowNew["证件号码"] = row["证件号码"];
                rowNew["姓名"] = row["姓名"];
                rowNew["房型"] = row["房型"];
                rowNew["房号"] = row["房号"];
                rowNew["入住时间"] = row["入住时间"];
                rowNew["退房时间"] = row["退房时间"];
                // rowNew["入住天数"] = row["入住天数"];
                rowNew["入住天数"] = row["入住天数"].ToString();
                rowNew["单价"] = row["单价"].ToString();
                //rowNew["单价"] = row["单价"];
                //rowNew["总价"] = row["总价"];
                rowNew["总价"] = row["总价"].ToString();
                dtResult.Rows.Add(rowNew);
            }
                       
            return dtResult;
        }

        private void setColumnsWidth()
        {
            DataGridLength length1 = new DataGridLength(1, DataGridLengthUnitType.Star);
            DataGridLength length1_5 = new DataGridLength(1.5, DataGridLengthUnitType.Star);
            DataGridLength length2 = new DataGridLength(2, DataGridLengthUnitType.Star);
            DataGridLength length2_5 = new DataGridLength(2.5, DataGridLengthUnitType.Star);
            DataGridLength length3 = new DataGridLength(3, DataGridLengthUnitType.Star);
            DataGridLength length4 = new DataGridLength(4, DataGridLengthUnitType.Star);
            //lastColumn.Width = length;
            dataGrid.Columns[0].Width = length2;
            dataGrid.Columns[1].Width = length3;
            dataGrid.Columns[2].Width = length4;
            dataGrid.Columns[3].Width = length1_5;
            dataGrid.Columns[4].Width = length2;
            dataGrid.Columns[5].Width = length1_5;
            dataGrid.Columns[6].Width = length2;
            dataGrid.Columns[7].Width = length2;
            dataGrid.Columns[8].Width = length2;
            dataGrid.Columns[9].Width = length1_5;
            dataGrid.Columns[10].Width = length2;

            Style styleCenter0 = new Style(typeof(DataGridColumnHeader));
            //Style styleRight0 = new Style(typeof(System.Windows.Controls.Datagrid));
            Style styleCenter = new Style(typeof(TextBlock));
            Style styleRight = new Style(typeof(TextBlock));
            Setter setCenter = new Setter(TextBlock.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Center);
            Setter setRight = new Setter(TextBlock.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Right);
            //styleRight.Setters.Add(setRight);
            Setter setHead = new Setter(DataGridColumnHeader.HorizontalContentAlignmentProperty, System.Windows.HorizontalAlignment.Center);
           // Setter setHead1 = new Setter(DataGridColumnHeader., System.Windows.VerticalAlignment.Center);
            styleCenter.Setters.Add(setCenter);
            styleCenter0.Setters.Add(setHead);
            styleRight.Setters.Add(setRight);
           // styleRight0.Setters.Add(setHead1);       
            foreach (DataGridColumn c in dataGrid.Columns)
            {
                DataGridTextColumn tc = c as DataGridTextColumn;
                if (tc.Header.ToString() != "总价" && tc.Header.ToString() != "单价")
                {
                    //tc.HeaderStyle = styleCenter0;
                    tc.ElementStyle = styleCenter;
                }
                else { tc.ElementStyle = styleRight; }
                tc.HeaderStyle = styleCenter0;
            }

        }

        private void AddColumn(DataTable dt)
        {
            dt.Columns.Add(new DataColumn());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                    dt.Rows[i][dt.Columns.Count - 1] = "A" + (dt.Columns.Count - 1).ToString();
                else
                    dt.Rows[i][dt.Columns.Count - 1] = "";
                //dt.Rows[i][dt.Columns.Count - 1] = i.ToString() + " - " + (newColumnIndex - 1).ToString();
            }
            //dataGrid.ItemsSource = null;
            //dataGrid.ItemsSource = dt.DefaultView;
        }



        private void SelectDisplayColumns(DataTable dt)
        {           
            for(int i = dt.Columns.Count-1; i >=0;  i--)
            {
                if (displayColumns.Contains(dt.Columns[i].ColumnName) == false)
                {
                    dt.Columns.RemoveAt(i);                                                                                  
                }
            }
            dt.Columns[0].ColumnName = "订单号";
            dt.Columns[1].ColumnName = "办理入住时间";
            dt.Columns[2].ColumnName = "证件号码";
            dt.Columns[3].ColumnName = "姓名";
            dt.Columns[4].ColumnName = "房型";
            dt.Columns[5].ColumnName = "房号";
            dt.Columns[6].ColumnName = "入住时间";
            dt.Columns[7].ColumnName = "退房时间";
            dt.Columns[8].ColumnName = "入住天数";
            dt.Columns[9].ColumnName = "单价";
            dt.Columns[10].ColumnName = "总价";
            //return dt;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (dt.Rows.Count <= 1)
            {
                System.Windows.MessageBox.Show("没有数据，无法导出", "提示");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            string filename;
            sfd.InitialDirectory = System.Environment.CurrentDirectory;
            filename = MyDateTimeUtil.Format(Convert.ToDateTime(DatePicker1.Text ), "yyyyMMdd");
            //if (DatePicker2.Text == "") { filename = DatePicker1.Text; }
            if(DatePicker2.Text!="") { filename = filename + "-" + MyDateTimeUtil.Format(Convert.ToDateTime(DatePicker2.Text), "yyyyMMdd"); }
            sfd.FileName = filename+"自助入住记录统计";
            sfd.RestoreDirectory = true;
            sfd.Filter = "Excel文件（*.xlsx)|*.xlsx|Excel文件(*.xls)|*.xls |CSV文件(*.csv) | *.csv";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                
                if (sfd.FileName.ToLower().EndsWith("csv")) {
                    CsvUtil cu = new CsvUtil();
                    cu.ExportDataToCVS(sfd.FileName, dt);
                    System.Windows.MessageBox.Show("成功导出CSV文件", "成功");
                }
                else
                {
                    Type officeType = Type.GetTypeFromProgID("Excel.Application");
                    if (officeType == null)
                    {
                        Console.WriteLine("没安装！");
                        System.Windows.MessageBox.Show("提示", "请先安装office2007以上版本办公软件");
                        return;
                    }
                    else { 
                        Console.WriteLine("已安装！");

                        ExcelUtil eu = new ExcelUtil();
                        eu.DataTabletoExcel(dt, sfd.FileName);
                        System.Windows.MessageBox.Show("成功导出EXCEL文件", "成功");
                    }

                    
                }

                
            }
            //var dialog = new FolderBrowserDialog();
            //dialog.ShowDialog();
            //folderpathTB.Text = dialog.SelectedPath;
           // excelUtil eu = new excelUtil();
            //eu.DataTabletoExcel(dt, System.Environment.CurrentDirectory + "\\temp.xlsx");
            //eu.DataTabletoExcel(dt, dialog.SelectedPath + "\\temp.xlsx");
        }

        private void dataGrid_AutoGeneratedColumns(object sender, EventArgs e)
        {
            /*
            dataGrid.Columns[0].Header = "订单号";
            dataGrid.Columns[1].Header = "办理入住时间";
            dataGrid.Columns[2].Header = "证件号码";
            dataGrid.Columns[3].Header = "姓名";
            dataGrid.Columns[4].Header = "房型";
            dataGrid.Columns[5].Header = "房号";
            dataGrid.Columns[6].Header = "入住时间";
            dataGrid.Columns[7].Header = "退房时间";
            dataGrid.Columns[8].Header = "入住天数";
            dataGrid.Columns[9].Header = "单价";
            dataGrid.Columns[10].Header = "总价";
            */
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            int count = dataGrid.Columns.Count;
            int[] widths=new int[count];
            for(int i = 0; i < count; i++)
            {
                widths[i] = (int)dataGrid.Columns[i].ActualWidth;
                Console.Write(widths[i]);
            }
            PrintDataService pds = new PrintDataService();
            HCPrintcs hcp = new HCPrintcs();
            PrintInfo pInfo = new PrintInfo();
            pInfo.widths = widths;
            hcp.printDataTable(dt, pInfo);
           //pds.PrintData(dt);
        }

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //dataGrid.Columns.cou
            /*
            dataGrid.Columns[0].Header = "订单号";
            dataGrid.Columns[1].Header = "办理入住时间";
            dataGrid.Columns[2].Header = "证件号码";
            dataGrid.Columns[3].Header = "姓名";
            dataGrid.Columns[4].Header = "房型";
            dataGrid.Columns[5].Header = "房号";
            dataGrid.Columns[6].Header = "入住时间";
            dataGrid.Columns[7].Header = "退房时间";
            dataGrid.Columns[8].Header = "入住天数";
            dataGrid.Columns[9].Header = "单价";
            dataGrid.Columns[10].Header = "总价";
            */
        }
    }
}
