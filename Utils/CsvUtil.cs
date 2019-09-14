using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class CsvUtil
    {

        public void ExportDataToCVS(String fileName, System.Data.DataTable dt)
        {
            if (MyStringUtil.isEmpty(fileName))
                return;

            if (dt == null || dt.Rows.Count <= 0)
                return;

            StreamWriter sw = new StreamWriter(fileName, false, Encoding.GetEncoding("gb2312"));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sb.Append(dt.Columns[i].ColumnName.ToString() + ",");
            }
            sb.Append(Environment.NewLine);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    sb.Append(dt.Rows[i][j].ToString() + ",");
                }
                sb.Append(Environment.NewLine);//每写一行数据后换行
            }
            sw.Write(sb.ToString());
            sw.Flush();
            sw.Close();//释放资源
        }
    }
}
