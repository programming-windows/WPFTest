using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    class MyDropDownList
    {

        public static List<MyComboBoxItem> roomTypeStatus = new List<MyComboBoxItem>()
        {
            new MyComboBoxItem() {mc = "未选定", bm = "0"},
            new MyComboBoxItem() {mc = "已选定", bm = "1"}
        };



        public static List<MyComboBoxItem> level = new List<MyComboBoxItem>()
        {

            new MyComboBoxItem("1", "1"),
            new MyComboBoxItem("2", "2"),
            new MyComboBoxItem("3", "3"),
            new MyComboBoxItem("4", "4"),
            new MyComboBoxItem("5", "5")
        };
    }
}
