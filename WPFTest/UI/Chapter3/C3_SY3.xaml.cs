
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
using MsExcel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Diagnostics;

namespace WPFTest.UI.Chapter3
{
    /// <summary>
    /// C2_SY1.xaml 的交互逻辑
    /// </summary>
    public partial class C3_SY3 : ChildPage
    {
        public C3_SY3()
        {
            InitializeComponent();

        }

        private void clearComments()
        {
            listBox1.Items.Clear();
        }

        private void showComment(String comment)
        {
            listBox1.Items.Add(comment);
        }

        private void btn1_Click_1(object sender, RoutedEventArgs e)
        {
            clearComments();
            string src_file_name = Directory.GetCurrentDirectory() + @"\Files\list.csv";
            string dest_file_name = Directory.GetCurrentDirectory() + @"\Files\list.xlsx";
            MsExcel.Application oExcApp;//Excel Application;
            MsExcel.Workbook oExcBook;//
            try
            {
                if (File.Exists(dest_file_name))
                {
                    File.Delete(dest_file_name);
                }
                oExcApp = new MsExcel.Application();
                object missing = System.Reflection.Missing.Value;
                oExcBook = oExcApp.Workbooks.Add(true);
                MsExcel.Worksheet worksheet1 = (MsExcel.Worksheet)oExcBook.Worksheets["sheet1"];
                worksheet1.Activate();
                oExcApp.Visible = false;
                oExcApp.DisplayAlerts = false;
                MsExcel.Range range1 = worksheet1.get_Range("B1", "H2");
                range1.Columns.ColumnWidth = 8;
                range1.Columns.RowHeight = 20;
                range1.Merge(false);
                //设置垂直居中和水平居中
                range1.VerticalAlignment = MsExcel.XlVAlign.xlVAlignCenter;
                range1.HorizontalAlignment = MsExcel.XlHAlign.xlHAlignCenter;
                //range1.Font.Color = Color.FromRgb(0, 0, 255);
                range1.Font.Size = 20;
                range1.Font.Bold = true;

                worksheet1.Cells[1, 2] = "学生成绩单";
                worksheet1.Cells[3, 1] = "学号";
                worksheet1.Cells[3, 2] = "姓名";
                worksheet1.Columns[1].ColumnWidth = 12;
                StreamReader sw = new StreamReader(src_file_name);
                string a_str;
                string[] str_list;
                int i = 4;
                a_str = sw.ReadLine();
                while (a_str != null)
                {
                    str_list = a_str.Split(",".ToCharArray());
                    worksheet1.Cells[i, 1] = str_list[0];
                    worksheet1.Cells[i, 2] = str_list[1];
                    i++;
                    a_str = sw.ReadLine();
                }
                sw.Close();
                for (int i1 = 0; i1 < 5; i1++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        worksheet1.Cells[i1 + 18, j + 3].Value2 = "=CEILING.MATH(RAND()*100)";
                        worksheet1.Cells[i1 + 4, j + 3].Value2 = worksheet1.Cells[i1 + 18, j + 3].Value;
                    }
                }

                //添加图表
                MsExcel.Shape theShape = worksheet1.Shapes.AddChart(MsExcel.XlChartType.xl3DColumn, 120, 130, 380, 250);
            
                                
                worksheet1.Cells[3, 3].Value2 = "美术";
                worksheet1.Cells[3, 4].Value2 = "物理";
                worksheet1.Cells[3, 5].Value2 = "政治";
                worksheet1.Cells[3, 6].Value2 = "化学";
                worksheet1.Cells[3, 7].Value2 = "体育";
                worksheet1.Cells[3, 8].Value2 = "英语";
                worksheet1.Cells[3, 9].Value2 = "数学";
                worksheet1.Cells[3, 10].Value2 = "历史";
                //设定图表的数据区域
                MsExcel.Range range = worksheet1.get_Range("b3:j8");
                theShape.Chart.SetSourceData(range, Type.Missing);

                //设置图标题文本
                theShape.Chart.HasTitle = true;
                theShape.Chart.ChartTitle.Text = "学生成绩";
                theShape.Chart.ChartTitle.Caption = "学生成绩";
                
                //设置单元格边框线型
                range1 = worksheet1.get_Range("a3", "j8");
                range1.Borders.LineStyle = MsExcel.XlLineStyle.xlContinuous;

                oExcBook.RefreshAll();
                worksheet1 = null;
                object file_name = dest_file_name;
                oExcBook.Close(true, file_name, null);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcBook);
                oExcBook = null;

                oExcApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcApp);
                oExcApp = null;
                System.GC.Collect();


            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message);
            }
            finally
            {
                Console.WriteLine(" 正在结束 excel 进程");
                showComment("正在结束excel进程");
                //关闭 excel 进程
                Process[] AllProces = Process.GetProcesses();
                for (int j = 0; j < AllProces.Length; j++)
                {
                    string theProcName = AllProces[j].ProcessName;
                    if (String.Compare(theProcName, "EXCEL") == 0)
                    {
                        if (AllProces[j].Responding && !AllProces[j].HasExited)
                        {
                            AllProces[j].Kill();
                        }
                    }
                }
                //Close excel Process.

                OpenExcel(dest_file_name);
            }
        }

        public static void OpenExcel(string ExcelDocPath)
        {

            //string tempPath = System.Environment.GetEnvironmentVariable("TEMP");
            //var filepath = Path.Combine(tempPath, WordPath);
            string excelExePath = "";
            Process[] wordProcesses = Process.GetProcessesByName("EXCEL");
            foreach (Process process in wordProcesses)
            {
                // Debug.WriteLine(process.MainWindowTitle);
                // 如果有的话获得 EXCEL.exe 的完全限定名称。
                excelExePath = process.MainModule.FileName;
                break;
            }

            Process excelProcess = new Process();

            if (excelExePath.Length > 0)    // 如果有 Excel 实例在运行，使用 /w 参数来强制启动新实例，并将文件名作为参数传递。 
            {
                excelProcess.StartInfo.FileName = ExcelDocPath;
                excelProcess.StartInfo.UseShellExecute = false;
                excelProcess.StartInfo.Arguments = excelExePath + " /w";
                excelProcess.StartInfo.RedirectStandardOutput = true;
            }
            else
            { // 如果没有 Excel 实例在运行，还是 
                excelProcess.StartInfo.FileName = ExcelDocPath;
                excelProcess.StartInfo.UseShellExecute = true;
            }

            excelProcess.Start();
            // 当前进程一直在等待，直到该 Excel 实例退出。 
            excelProcess.WaitForExit();
            excelProcess.Close();

        }


    }
}
