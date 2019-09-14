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
    /// RoomTypeListPage.xaml 的交互逻辑
    /// </summary>
    public partial class RoomTypeListPage : ChildPage
    {

        public RoomType currentItem  { get;set; }

        public RoomTypeListPage()
        {
            currentItem = null;
            InitializeComponent();
        }

        private void ChildPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            loadDatas();
        }

        public void loadDatas() { 

            //List<RoomType> items = getRoomTypesFromExcel();
            List<RoomType> items = getRoomTypesByRandom();
            dataGrid.ItemsSource = items;

            if (items!=null && items.Count() > 0)
            {
                loadRooms(items.First().type);
            }else
            {
                dataGrid2.ItemsSource = null;
            }
        }

        /// <summary>
        /// 产生固定的几个房型
        /// </summary>
        /// <returns></returns>
        private List<RoomType> getRoomTypesByRandom()
        {
            List<RoomType> results = new List<RoomType>();

            RoomType rt1 = new RoomType()
            {
                type = "t1",
                description = "豪华大床房",
                remarks = "360海景房",
                price = 460,
                status = "0"     //未选定
            };

            RoomType rt2 = new RoomType()
            {
                type = "t2",
                description = "豪华双人房",
                remarks = "360海景房",
                price = 360,
                status = "0"     //未选定
            };

            RoomType rt3 = new RoomType()
            {
                type = "t3",
                description = "经济双人房",
                remarks = "360海景房",
                price = 260,
                status = "0"     //未选定
            };

            RoomType rt4 = new RoomType()
            {
                type = "t4",
                description = "经济大床房",
                remarks = "360海景房",
                price = 160,
                status = "0"     //未选定
            };

            results.Add(rt1);
            results.Add(rt2);
            results.Add(rt3);
            results.Add(rt4);
            return results;
        }

        private List<RoomType> getRoomTypesFromExcel()
        {
            List<RoomType> results = new List<RoomType>();
            DataSet ds = ExcelUtil.ExcelToDS("Sheet1", "");
            if (ds != null)
            {
                foreach (DataRow mDr in ds.Tables[0].Rows)
                {
                    RoomType rt = new RoomType()
                    {
                        type = (mDr["房型编号"].Equals(DBNull.Value)) ? "" : (String)mDr["房型编号"],
                        description = (mDr["房型名称"].Equals(DBNull.Value)) ? "" : (String)mDr["房型名称"],
                        remarks = (mDr["房型特征"].Equals(DBNull.Value)) ? "" : (String)mDr["房型特征"],
                        price = (mDr["单价"].Equals(DBNull.Value)) ? 0 : (Double)mDr["单价"],
                        status = (mDr["当前状态"].Equals(DBNull.Value)) ? "" : (String)mDr["当前状态"]
                    };
                    results.Add(rt);
                }
            }
            return results;
        }

        private List<Room> getAllRoomsFromExcel(String roomTypeNo)
        {
            List<Room> results = new List<Room>();
            String condition = "";
            if (!MyStringUtil.isEmpty(roomTypeNo))
            {
                condition = "[房型编号]='" + roomTypeNo + "'";
            }

            DataSet ds = ExcelUtil.ExcelToDS("Sheet2", condition);
            if (ds != null)
            {
                foreach (DataRow mDr in ds.Tables[0].Rows)
                {
                    Room rt = new Room()
                    {
                        type = new RoomType()
                        {
                            type = (mDr["房型编号"].Equals(DBNull.Value)) ? "" : (String)mDr["房型编号"],
                        },
                        roomNumber = (mDr["房号"].Equals(DBNull.Value)) ? "" : (String)mDr["房号"],
                        location =  (mDr["所在地址"].Equals(DBNull.Value)) ? "" : (String)mDr["所在地址"],
                        comments = (mDr["房型特征"].Equals(DBNull.Value)) ? "" : (String)mDr["房型特征"],
                        price = (mDr["单价"].Equals(DBNull.Value)) ? 0 : (Double)mDr["单价"],
                        //state = (mDr["当前状态"].Equals(DBNull.Value)) ? "" : (String)mDr["当前状态"]
                        state = Room.RoomState.VACANT
                    };
                    results.Add(rt);
                }
            }
            return results;
        }

        private List<Room> getAllAvailableRooms(String roomTypeNo)
        {
            return getAllRooms(roomTypeNo);
        }

        private List<Room> getAllRooms(String _roomTypeNo)
        {
            return getAllRoomsFromExcel(_roomTypeNo);
        }



        private void new_Click_1(object sender, RoutedEventArgs e)
        {
            currentItem = null;
            //FileEdit window = new FileEdit(this);
            //window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //window.ShowDialog();

           
        }

        private void edit_Click_1(object sender, RoutedEventArgs e)
        {

            RoomType item = (RoomType)dataGrid.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("请选择数据行");
                return;
            }

            MessageBox.Show(item.type, item.description);

            currentItem = new RoomType();
            ObjectUtils.copyObjectValues(item, currentItem);

            /*
            FileEdit window = new FileEdit(this);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (window.ShowDialog() == true)
            {

            }
            */
        }

        
        private void del_Click_1(object sender, RoutedEventArgs e)
        {
            RoomType item = (RoomType)dataGrid.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("请选择数据行");
                return;
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

        private void datagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid datagrid = sender as DataGrid;
            Point aP = e.GetPosition(datagrid);
            IInputElement obj = datagrid.InputHitTest(aP);
            DependencyObject target = obj as DependencyObject;


            while (target != null)
            {
                if (target is DataGridRow)
                {
                    break;
                }
                target = VisualTreeHelper.GetParent(target);
            }

            RoomType roomType = (RoomType)((DataGridRow)target).Item;
            if (roomType == null)
            {
                MessageBox.Show("请选择数据行");
                return;
            }
            loadRooms(roomType.type);

        }

        private void showDetail_Click_1(object sender, RoutedEventArgs e)
        {
            RoomType item = (RoomType)dataGrid.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("请选择数据行");
                return;
            }

            loadRooms(item.type);
        }

        private void loadRooms(String roomType)
        {
            List<Room> items = getAllRoomsFromExcel(roomType);
            dataGrid2.ItemsSource = items;
        }

        
    }
}
