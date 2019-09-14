using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    class MyComboBoxItem
    {
        public String bm { get; set; }
        public String mc { get; set; }
        public int bm_int { get; set; }


        public MyComboBoxItem(String x, String y)
        {
            bm = x;
            mc = y;
        }

        public MyComboBoxItem()
        {
        }
    }


}
