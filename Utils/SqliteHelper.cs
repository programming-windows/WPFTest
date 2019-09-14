using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SQLite;

using Utils;

namespace Utils
{
    public class SqliteHelper
    {
        /// <summary>
        /// 数据库连接定义
        /// </summary>
        private SQLiteConnection dbConnection;

        /// <summary>
        /// SQL命令定义
        /// </summary>
        private SQLiteCommand dbCommand;

        /// <summary>
        /// 数据读取定义
        /// </summary>
        private SQLiteDataReader dataReader;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接SQLite库字符串</param>
        public SqliteHelper(string connectionString)
        {
            try
            {
                dbConnection = new SQLiteConnection(connectionString);
                dbConnection.Open();
            }catch(Exception e)
            {
                Log.Write(e.ToString());
            }
        }
        
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseConnection()
        {
            //销毁Commend
            if(dbCommand !=null)
            {
                dbCommand.Cancel();
            }
            dbCommand = null;
            //销毁Reader
            if(dataReader != null)
            {
                dataReader.Close();
            }
            dataReader = null;
            //销毁Connection
            if(dbConnection != null)
            {
                dbConnection.Close();
            }
            dbConnection = null;

        }

        /// <summary>
        /// 读取整张数据表
        /// </summary>
        /// <returns>The full table.</returns>
        /// <param name="tableName">数据表名称</param>
        public SQLiteDataReader ReadFullTable(string tableName)
        {
            string queryString = "SELECT * FROM " + tableName;
            return ExecuteQuery(queryString);
        }

        public DataTable SelectFullTable(string tableName)
        {
            string queryString = "SELECT * FROM " + tableName;
            return ExecuteSelect(queryString);
        }

        /// <summary>
        /// 向指定数据表中插入数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="values">插入的数值</param>
        public int InsertValues(string tableName, string[] colValues)
        {
            //获取数据表中字段数目
            int fieldCount = ReadFullTable(tableName).FieldCount;
            //当插入的数据长度不等于字段数目时引发异常
            if (colValues.Length != fieldCount)
            {
                throw new SQLiteException("colValues.Length!=fieldCount");
            }

            if (colValues.Length <= 0)
            {
                throw new SQLiteException("colValues.Length<=0");
            }

            string queryString = "INSERT INTO " + tableName + " VALUES (" + "'"+ colValues[0]+"'";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += ", " + "'"+ colValues[i] + "'";
            }
            queryString += " )";
            return ExecuteNonQuery(queryString);
        }

        /// <summary>
        /// 插入指定数据表内的数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        /// <param name="key">关键字</param>
        /// <param name="value">关键字对应的值</param>
        /// <param name="operation">运算符：=,<,>,...，默认“=”</param>
        public int InsertValues(string tableName, string[] colNames, string[] colValues)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length");
            }

            if (colNames.Length <= 0)
            {
                throw new SQLiteException("colNames.Length<=0");
            }
            if (colValues.Length <= 0)
            {
                throw new SQLiteException("colValues.Length<=0");
            }

            string queryString = "INSERT INTO " + tableName + " (" + colNames[0];
            for (int i = 1; i < colNames.Length; i++)
            {
                queryString += ","+ colNames[i];
            }
            queryString += ") VALUES (" + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += ", " + "'" + colValues[i] + "'";
            }
            queryString += " )";
            return ExecuteNonQuery(queryString);
        }


        /// <summary>
        /// 更新指定数据表内的数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        /// <param name="key">关键字</param>
        /// <param name="value">关键字对应的值</param>
        /// <param name="operation">运算符：=,<,>,...，默认“=”</param>
        public int UpdateValues(string tableName, string[] colNames, string[] colValues, string key, string value, string operation="=")
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length");
            }

            string queryString = "UPDATE " + tableName + " SET " + colNames[0] + "=" + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += ", " + colNames[i] + "=" + "'" + colValues[i] + "'";
            }
            queryString += " WHERE " + key + operation + "'" + value + "'";
            return ExecuteNonQuery(queryString);
        }

        /// <summary>
        /// 删除指定数据表内的数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        public int DeleteValuesOR(string tableName, string[] colNames, string[] colValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
            }

            string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += "OR " + colNames[i] + operations[0] + "'" + colValues[i] + "'";
            }
            return ExecuteNonQuery(queryString);
        }

        /// <summary>
        /// 删除指定数据表内的数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        public int DeleteValuesAND(string tableName, string[] colNames, string[] colValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
            }

            string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += " AND " + colNames[i] + operations[i] + "'" + colValues[i] + "'";
            }
            return ExecuteNonQuery(queryString);
        }


        /// <summary>
        /// 创建数据表
        /// </summary> +
        /// <returns>The table.</returns>
        /// <param name="tableName">数据表名</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colTypes">字段名类型</param>
        public int CreateTable(string tableName, string[] colNames, string[] colTypes)
        {
            string queryString = "CREATE TABLE IF NOT EXISTS " + tableName + "( " + colNames[0] + " " + colTypes[0];
            for (int i = 1; i < colNames.Length; i++)
            {
                queryString += ", " + colNames[i] + " " + colTypes[i];
            }
            queryString += "  ) ";
            return ExecuteNonQuery(queryString);
        }

        /// <summary>
        /// Reads the table.
        /// </summary>
        /// <returns>The table.</returns>
        /// <param name="tableName">Table name.</param>
        /// <param name="items">Items.</param>
        /// <param name="colNames">Col names.</param>
        /// <param name="operations">Operations.</param>
        /// <param name="colValues">Col values.</param>
        public DataTable ReadTable(string tableName, string[] items, string[] colNames, string[] operations, string[] colValues)
        {
            string queryString = "SELECT " + items[0];
            for (int i = 1; i < items.Length; i++)
            {
                queryString += ", " + items[i];
            }
            queryString += " FROM " + tableName + " WHERE " + colNames[0] + " " + operations[0] + " " + colValues[0];
            for (int i = 0; i < colNames.Length; i++)
            {
                queryString += " AND " + colNames[i] + " " + operations[i] + " " + colValues[0] + " ";
            }
            return ExecuteSelect(queryString);
        }


        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <returns>The query.</returns>
        /// <param name="queryString">SQL命令字符串</param>
        public SQLiteDataReader ExecuteQuery(string queryString)
        {
            try
            {
                dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = queryString;
                dataReader = dbCommand.ExecuteReader();

            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }

            return dataReader;
        }


        public DataTable ExecuteSelect(string queryString)
        {
            DataTable ds = null;
            try
            {
                dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = queryString;
                //dataReader = dbCommand.ExecuteReader();

                if (dbCommand.ExecuteScalar() != null)
                {
                    ds = new DataTable();
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(queryString, dbConnection);
                    adapter.Fill(ds);
                }

            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }

            return ds;
        }

        public int ExecuteNonQuery(string queryString)
        {
            try
            {
                dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = queryString;

                return dbCommand.ExecuteNonQuery();
                /*
                dataReader = dbCommand.ExecuteReader();
                DataSet ds = new DataSet();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(queryString, dbConnection);
                adapter.Fill(ds);
                */

            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }

            return 0;
        }

        //sql语句中，包含@参数
        public int ExecuteNonQuery(string queryString, Dictionary<String,String> parameters)
        {
            try
            {
                dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = queryString;

                
                foreach (String key in parameters.Keys)
                {
                    dbCommand.Parameters.AddWithValue("@" + key, parameters[key]);
                }

                return dbCommand.ExecuteNonQuery();
                /*
                dataReader = dbCommand.ExecuteReader();
                DataSet ds = new DataSet();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(queryString, dbConnection);
                adapter.Fill(ds);
                */

            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }

            return 0;
        }

        /// <summary>
        /// 获取某个数据表的列名及中文名
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public Dictionary<String, String> getColumns(string tableName)
        {   
            Dictionary<String, String> result = new Dictionary<String, String>();

            string queryString = "select * from ts_table_label where TABLE_NAME='" + tableName + "'";
            DataTable dataTable = ExecuteSelect(queryString);

            foreach (DataRow mDr in dataTable.Rows)
            {
                string columnName = (mDr["COL_NAME"].Equals(DBNull.Value)) ? "" : (String)mDr["COL_NAME"];
                string columnLabel = (mDr["COL_LABEL"].Equals(DBNull.Value)) ? "" : (String)mDr["COL_LABEL"];

                if (!MyStringUtil.isEmpty(columnName))
                {
                    if (MyStringUtil.isEmpty(columnLabel))
                        columnLabel = columnName;

                    result.Add(columnName, columnLabel);
                }

            }
            return result;
        }

    }
}
