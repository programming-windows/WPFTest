using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class SqlserverUtil
    {
        //public string sqlString = "server=localhost;user id=root;password=123456;database=db1";
        public string sqlString;
        string sql = "SQL命令语句";
        //public enum displayRows { GUESTID,GUESTNAME,ROOMTYPE,ROOMN0,CHECKINTIME,CHECKOUTTIME,TOTALPRICE};  
        

        public  SqlserverUtil()
        {
            sqlString = "Server=" + Parameters.Get().server + ";User ID=" + Parameters.Get().userID +
                ";Password=" + Parameters.Get().password + ";Database=" + Parameters.Get().database+ ";Trusted_Connection = False;";
           // sqlString = "server=" + parameters.Get().server + ";user id=" + parameters.Get().userID +
           //     ";password=" + parameters.Get().password + ";database=" + parameters.Get().database;

        }

        public SqlConnection connect()
        {
            SqlConnection conn = new SqlConnection(sqlString);
            conn.Open();
            if (conn.State == ConnectionState.Open) { return conn; }
            else { return null; }
        }

        public void close(SqlConnection conn)
        {
            conn.Close();

        }
        /*
        public DataTable query(SqlConnection conn)
        {
            sql = "select * from dbo.invoice";
            DataTable dt = new DataTable();
            //DataTable result = new DataTable();         
            SqlDataAdapter mda = null;
            //MySqlDataAdapter mda = new MySqlDataAdapter();
            try
            {
                mda = new SqlDataAdapter(sql, conn);
                
                mda.Fill(dt);
                //int i = dt.Columns.Count;             
                //dt.Columns[1].ColumnName = "订单类型";
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable query(SqlConnection conn, string field, string condition)
        {
            
            sql = "select * from dbo.invoice where " + field + "=" + "'" + condition + "'";
            DataTable dt = new DataTable();
            SqlDataAdapter mda = null;
            //MySqlDataAdapter mda = new MySqlDataAdapter();
            try
            {
                
                mda = new SqlDataAdapter(sql, conn);
                mda.Fill(dt);
                
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            //dt = cmd.ExecuteNonQuery();
        }
        **/

        public DataTable query(string condition)
        {
            SqlConnection localConnection = null;
            sql = "select * from dbo.invoice where 1=1 ";
            if (condition != null && !condition.Equals(""))
                sql = sql + condition;
            sql = sql + " order by PAYTIME desc";

            DataTable dt = new DataTable();
            SqlDataAdapter mda = null;
            //MySqlDataAdapter mda = new MySqlDataAdapter();
            try
            {
                localConnection = new SqlConnection(sqlString);
                localConnection.Open();
                mda = new SqlDataAdapter(sql, localConnection);
                mda.Fill(dt);
                localConnection.Close();
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            //dt = cmd.ExecuteNonQuery();
        }

        public DataTable query(List<string> fields, string condition)
        {
            SqlConnection localConnection = null;

            sql = "select";
            for (int i = 0; i < fields.Count; i++)
            {
                if (i == 0)
                    sql = sql + " " + fields[i];
                else
                    sql = sql + "," + fields[i];
            }

            sql = sql + " from dbo.invoice where 1=1 ";
            if (condition != null && !condition.Equals(""))
                sql = sql + condition;

            sql = sql + " order by PAYTIME desc";

            DataTable dt = new DataTable();
            SqlDataAdapter mda = null;
            //MySqlDataAdapter mda = new MySqlDataAdapter();
            try
            {
                localConnection = new SqlConnection(sqlString);
                localConnection.Open();
                mda = new SqlDataAdapter(sql, localConnection);
                mda.Fill(dt);                
                localConnection.Close();
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            //dt = cmd.ExecuteNonQuery();
        }

        /*
        public DataTable query(SqlConnection conn, string field, string condition1, string condition2)
        {
            //int con1 = StringToNumber(condition1);
            //int con2 = StringToNumber(condition2);
            sql = "select * from dbo.invoice where " + field + ">= " + "'" + condition1 + "' and " + field + "<=" + "'" + condition2 + "'";

            DataTable dt = new DataTable();
            SqlDataAdapter mda = null;
            //MySqlDataAdapter mda = new MySqlDataAdapter();
            try
            {
                mda = new SqlDataAdapter(sql, conn);
                mda.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            //dt = cmd.ExecuteNonQuery();
        }
        */
    }
}
