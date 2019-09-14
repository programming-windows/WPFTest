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
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace WPFTest.UI.Chapter6
{
    /// <summary>
    /// ADOTest.xaml 的交互逻辑
    /// </summary>
    public partial class ADOTest : ChildPage
    {
        public ObservableCollection<Book> bookList { get; set; }
        public Book currentItem { get; set; }
        public MainWindow parentWindow;

        public ADOTest(MainWindow _parent)
        {
            bookList = new ObservableCollection<Book>();
            currentItem = null;
            parentWindow = _parent;
            InitializeComponent();
        }

        private void ChildPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            loadDatas();
        }

        public void loadDatas() {


        }

        private void showDetail_Click_1(object sender, RoutedEventArgs e)
        {
            //显示可用数据库 
            this.btnShow.IsEnabled = false;
            this.btnConnect.IsEnabled = false;
            this.btn1.IsEnabled = false;
            this.btn2.IsEnabled = false;
            this.btn3.IsEnabled = false;
            this.btn4.IsEnabled = false;

            this.comboBox.Items.Clear();


            System.Data.Common.DbDataSourceEnumerator emumerator =
                System.Data.SqlClient.SqlClientFactory.Instance.CreateDataSourceEnumerator();
            DataTable dsrc_table = emumerator.GetDataSources();

            //dsrc_table.co
            if (dsrc_table != null && dsrc_table.Rows.Count > 0)
            {
                //this.comboBox.ItemsSource = dsrc_table.Rows;

                foreach (DataRow mDr in dsrc_table.Rows)
                {
                    string str = mDr[0] + " " + mDr[1];
                    this.comboBox.Items.Add(str);
                }


                this.btnShow.IsEnabled = true;
                this.btnConnect.IsEnabled = true;
            }
        }

        private string getConnectionStr()
        {
            /*
            string currentItem = (string)(this.comboBox.Items.CurrentItem);
            if (MyStringUtil.isEmpty(currentItem))
            {
                MessageBox.Show("请选择服务器实例");
                return;
            }
            */

            string currentServer = "LIU-OFFICE";
            string[] str = currentServer.Split(' ');
            string server = str[0];
            if (str.Length >= 2 && !MyStringUtil.isEmpty(str[1]))
            {
                server += "\\" + str[1];
            }

            string user = textBox1.Text.Trim();
            string psw = textBox2.Text.Trim();
            if (MyStringUtil.isEmpty(user))
            {
                MessageBox.Show("请输入用户名");
                return "";
            }

            String strConnect = "Server=" + server + ";User ID=" + user + ";password=" + psw + ";database=XML2017";
            return strConnect;
        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            string strConnect = this.getConnectionStr();
            if (MyStringUtil.isEmpty(strConnect))
            {
                return;
            }

            //测试连接
            bool con_sucess = true;
            //SqlConnection hCon = new SqlConnection("Server=6BE5ACE6428D418\\SQLEC;User ID=sa;Password=mssql;database=model");
            SqlConnection hCon = new SqlConnection(strConnect);
            try
            {
                hCon.Open();
                hCon.Close();
            }
            catch (Exception ee)
            {
                con_sucess = false;
                MessageBox.Show(ee.Message);
            }
            finally
            {
                if (con_sucess)
                {//连接成功
                    MessageBox.Show("数据库连接测试成功");
                }
                else
                { //不成功

                }
            }
        }





        private void btnDataReader_Click(object sender, RoutedEventArgs e)
        {

            string strConnect = this.getConnectionStr();
            if (MyStringUtil.isEmpty(strConnect))
            {
                return;
            }

            bookList.Clear();
            SqlConnection hCon = null;
            try
            {
                hCon = new SqlConnection(strConnect);
                hCon.Open();

                string sql = "select * from myBook";

                SqlCommand command = new SqlCommand();
                command.Connection = hCon;
                command.CommandText = sql;
                SqlDataReader mDr = command.ExecuteReader();
                while (mDr.Read())
                {
                    Book book = new Book()
                    {
                        Id = (mDr["id"].Equals(DBNull.Value)) ? "" : (String)mDr["id"],
                        Year = (mDr["year"].Equals(DBNull.Value)) ? 0 : (int)mDr["year"],
                        Title = (mDr["title"].Equals(DBNull.Value)) ? "" : (String)mDr["title"],
                        Price = (mDr["price"].Equals(DBNull.Value)) ? 0.00 : (Double)mDr["price"],
                        AuthorFirst = (mDr["author_first"].Equals(DBNull.Value)) ? "" : (String)mDr["author_first"],
                        AuthorLast = (mDr["author_last"].Equals(DBNull.Value)) ? "" : (String)mDr["author_last"],
                        Publisher = (mDr["publisher"].Equals(DBNull.Value)) ? "" : (String)mDr["publisher"],
                        entityState = EntityState.NONE
                    };
                    bookList.Add(book);
                }

                mDr.Close();
                command.Clone();

                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = bookList;

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            finally
            {
                if (hCon != null && hCon.State == ConnectionState.Open)
                {
                    hCon.Close();
                }
            }

        }


        private void btnDataAdapter_Click(object sender, RoutedEventArgs e)
        {
            string strConnect = this.getConnectionStr();
            if (MyStringUtil.isEmpty(strConnect))
            {
                return;
            }

            bookList.Clear();
            SqlConnection hCon = null;
            try
            {
                hCon = new SqlConnection(strConnect);
                hCon.Open();

                string sql = "select * from myBook";


                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sql, hCon);

                da.Fill(ds);
                if (ds != null)
                {
                    foreach (DataRow mDr in ds.Tables[0].Rows)
                    {
                        /*
                        Book book = new Book();
                        book.year = (mDr["year"].Equals(DBNull.Value)) ? 0 : (int)mDr["year"];
                        book.title = (mDr["title"].Equals(DBNull.Value)) ? "" : (String)mDr["title"];
                        book.price = (mDr["price"].Equals(DBNull.Value)) ? 0.00 : (Double)mDr["price"];
                        book.authorFirst = (mDr["author_first"].Equals(DBNull.Value)) ? "" : (String)mDr["author_first"];
                        book.authorLast = (mDr["author_last"].Equals(DBNull.Value)) ? "" : (String)mDr["author_last"];
                        book.publisher = (mDr["publisher"].Equals(DBNull.Value)) ? "" : (String)mDr["publisher"];
                        book.id = (mDr["id"].Equals(DBNull.Value)) ? "" : (String)mDr["id"];
                        */

                        Book book = new Book()
                        {

                            Year = (mDr["year"].Equals(DBNull.Value)) ? 0 : (int)mDr["year"],
                            Title = (mDr["title"].Equals(DBNull.Value)) ? "" : (String)mDr["title"],
                            Price = (mDr["price"].Equals(DBNull.Value)) ? 0.00 : (Double)mDr["price"],
                            AuthorFirst = (mDr["author_first"].Equals(DBNull.Value)) ? "" : (String)mDr["author_first"],
                            AuthorLast = (mDr["author_last"].Equals(DBNull.Value)) ? "" : (String)mDr["author_last"],
                            Publisher = (mDr["publisher"].Equals(DBNull.Value)) ? "" : (String)mDr["publisher"],
                            Id = (mDr["id"].Equals(DBNull.Value)) ? "" : (String)mDr["id"],
                            entityState = EntityState.NONE
                        };

                        bookList.Add(book);
                    }
                }

                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = bookList;

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            finally
            {
                if (hCon != null && hCon.State == ConnectionState.Open)
                {
                    hCon.Close();
                }
            }

        }


        private void new_Click_1(object sender, RoutedEventArgs e)
        {
            currentItem = null;
            //FileEdit window = new FileEdit(this);
            //window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //window.ShowDialog();
            Book book = new Book()
            {
                entityState = EntityState.NEW
            };

            currentItem = book;
            this.bookList.Add(book);
            this.dataGrid.ItemsSource = null;
            this.dataGrid.ItemsSource = bookList;


        }

        private void edit_Click_1(object sender, RoutedEventArgs e)
        {
            /*
            currentItem = (Book)dataGrid.SelectedItem;
            if (currentItem == null)
            {
                MessageBox.Show("请选择数据行");
                return;
            }
            */

            int updated = update();
            if (updated > 0)
            {
                MessageBox.Show("批量更新了" + updated + "条数据");
            }
        }

        private int update()
        {

            string strConnect = this.getConnectionStr();
            if (MyStringUtil.isEmpty(strConnect))
                return 0;

            SqlConnection hCon = null;
            try
            {
                hCon = new SqlConnection(strConnect);
                hCon.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM myBook", hCon);
                DataSet ds = new DataSet();
                da.Fill(ds, "bookTable");

                da.UpdateCommand = new SqlCommand("UPDATE myBook SET price = @price " + "WHERE id = @id", hCon);
                da.UpdateCommand.Parameters.Add("@price", SqlDbType.Float, 15, "price");
                da.UpdateCommand.Parameters.Add("@id", SqlDbType.NVarChar, 15, "id");

                ds.Tables["bookTable"].Rows[0]["price"] = (Double)ds.Tables["bookTable"].Rows[0]["price"] + 10;
                ds.Tables["bookTable"].Rows[1]["price"] = (Double)ds.Tables["bookTable"].Rows[0]["price"] + 20;

                int ret = da.Update(ds.Tables[0]);
                return ret;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return -1;
            }
            finally
            {
                if (hCon != null && hCon.State == ConnectionState.Open)
                {
                    hCon.Close();
                }
            }
        }


        private void del_Click_1(object sender, RoutedEventArgs e)
        {
            Book item = (Book)dataGrid.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("请选择数据行");
                return;
            }

            YesOrNoDialog yd = new YesOrNoDialog();
            yd.Owner = parentWindow;
            yd.SetInfo("再次确定是否要删除？");
            if (yd.ShowDialog() == false)
                return;
            
            if (delete(item)>0) { 
                this.bookList.Remove(item);
                this.dataGrid.ItemsSource = bookList;
            }

            /*
            int ret = sqlite.ExecuteNonQuery("delete from tbl_fgwj where ID_KEY='" + item.idKey + "'");
            if (ret > 0)
            {
                this.loadDatas();
                MessageBox.Show("删除成功!");
            }
            */
        }

        private int delete(Book book)
        {
            string strConnect = this.getConnectionStr();
            if (MyStringUtil.isEmpty(strConnect))
                return 0;
            
            if (MyStringUtil.isEmpty(book.Id) || book.entityState==EntityState.NEW)
                return 1;

            SqlConnection hCon = null;
            try
            {
                hCon = new SqlConnection(strConnect);
                hCon.Open();
                //book.id
                string sql = "delete  from myBook where id='" + book.Id + "'";

                SqlCommand command = new SqlCommand();
                command.Connection = hCon;
                command.CommandText = sql;
                int ret = command.ExecuteNonQuery();
                return ret;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return -1;
            }
            finally
            {
                if (hCon != null && hCon.State == ConnectionState.Open)
                {
                    hCon.Close();
                }
            }
        }

        private void save_Click_1(object sender, RoutedEventArgs e)
        {
            DataGridHelper.SetRealTimeCommit(dataGrid, true);

            string strConnect = this.getConnectionStr();
            if (MyStringUtil.isEmpty(strConnect))
            {
                return ;
            }

            SqlConnection hCon = null;
            try
            {
                hCon = new SqlConnection(strConnect);
                hCon.Open();

                string sql = "select * from myBook";
                SqlCommand updateCommand = new SqlCommand("UPDATE myBook SET year = @year ,title = @title ,price = @price ,author_first = @author_first ,author_last = @author_last ,publisher = @publisher " + " WHERE id = @id", hCon);
                /*
                updateCommand.Parameters.Add("@year", SqlDbType.Int, 15, "year");
                updateCommand.Parameters.Add("@title", SqlDbType.NVarChar, 100, "title");
                updateCommand.Parameters.Add("@price", SqlDbType.Float, 12, "price");
                updateCommand.Parameters.Add("@author_first", SqlDbType.NVarChar, 100, "author_first");
                updateCommand.Parameters.Add("@author_last", SqlDbType.NVarChar, 100, "author_last");
                updateCommand.Parameters.Add("@publisher", SqlDbType.NVarChar, 100, "publisher");
                updateCommand.Parameters.Add("@id", SqlDbType.NVarChar, 10, "id");
                */

                SqlCommand insertCommand = new SqlCommand("insert into myBook (year,title,price,author_first,author_last,publisher,id) values (@year ,@title ,@price ,@author_first ,@author_last ,@publisher, @id)", hCon);
                /*
                insertCommand.Parameters.Add("@year", SqlDbType.Int, 15, "year");
                insertCommand.Parameters.Add("@title", SqlDbType.NVarChar, 100, "title");
                insertCommand.Parameters.Add("@price", SqlDbType.Float, 12, "price");
                insertCommand.Parameters.Add("@author_first", SqlDbType.NVarChar, 100, "author_first");
                insertCommand.Parameters.Add("@author_last", SqlDbType.NVarChar, 100, "author_last");
                insertCommand.Parameters.Add("@publisher", SqlDbType.NVarChar, 100, "publisher");
                insertCommand.Parameters.Add("@id", SqlDbType.NVarChar, 10, "id");
                */

                int updatedCount = 0;
                int indertedCount = 0;

                foreach (Book book in bookList)
                {
                    if (MyStringUtil.isEmpty(book.Id) && book.entityState == EntityState.MODIFIED)
                    {
                        book.entityState = EntityState.NEW;
                    }

                    if (book.entityState == EntityState.MODIFIED)
                    {
                        updateCommand.Parameters.AddWithValue("@year", book.Year);
                        updateCommand.Parameters.AddWithValue("@title", book.Title);
                        updateCommand.Parameters.AddWithValue("@price", book.Price);
                        updateCommand.Parameters.AddWithValue("@author_first", book.AuthorFirst);
                        updateCommand.Parameters.AddWithValue("@author_last", book.AuthorLast);
                        updateCommand.Parameters.AddWithValue("@publisher", book.Publisher);
                        updateCommand.Parameters.AddWithValue("@id", book.Id);
                        updatedCount = updatedCount + updateCommand.ExecuteNonQuery();
                    }

                    if (book.entityState == EntityState.NEW)
                    {
                        if (MyStringUtil.isEmpty(book.Id))
                            book.Id = MyStringUtil.getGuidStr22();

                        insertCommand.Parameters.AddWithValue("@year", book.Year);
                        insertCommand.Parameters.AddWithValue("@title", book.Title);
                        insertCommand.Parameters.AddWithValue("@price", book.Price);
                        insertCommand.Parameters.AddWithValue("@author_first", book.AuthorFirst);
                        insertCommand.Parameters.AddWithValue("@author_last", book.AuthorLast);
                        insertCommand.Parameters.AddWithValue("@publisher", book.Publisher);
                        insertCommand.Parameters.AddWithValue("@id", book.Id);
                        indertedCount = indertedCount + insertCommand.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("更新了" + updatedCount + "条数据，新增了" + indertedCount + "条数据");


            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            finally
            {
                if (hCon != null && hCon.State == ConnectionState.Open)
                {
                    hCon.Close();
                }
            }
        }

        private void dataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus == null) return;
                e.Handled = true;

                if (this.dataGrid.CurrentCell.Column.DisplayIndex == this.dataGrid.Columns.Count - 1)
                {
                    this.dataGrid.CommitEdit(DataGridEditingUnit.Row, exitEditingMode: true);
                    elementWithFocus = Keyboard.FocusedElement as UIElement;
                }
                if (elementWithFocus != null)
                {
                    elementWithFocus.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
            }

            if (e.Key == Key.Back)  //删除行       
            {
                //Book item = (Book)dataGrid.SelectedItem;

                
                int pos = dataGrid.Items.CurrentPosition;
                bookList.RemoveAt(pos);
                dataGrid.Items.RemoveAt(pos);
                
                /*
                for (int i = 0; i < StuList.Count; i++)//不用for会报错 
                {
                    foreach (Student tq in StuList)  //删除选中行
                    {
                        StuList.RemoveAt(tq.FId);
                        break;
                    }
                }
                */

            }
        }

        
    }
}
