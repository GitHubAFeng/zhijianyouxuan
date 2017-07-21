using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Hangjing.Common;
using System.Data.Common;

namespace Hangjing.DBUtility
{
    public abstract class SQLHelper
    {
        //数据库连接字符串
        public static string ConnectionString = GetConnectString();

        public static string GetConnectString()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["conn"].ToString();
            return ConnectionString;
        }

        /// <summary>
        /// 执行一条SQL命令，返回影响的行数
        /// </summary>
        /// <example>
        /// int result = ExecuteNonQuery(CommandType.Text , "Update table1 set field1 = @fild1 where id = @id", myCommandParameters)
        /// </example>
        /// <param name="cmdType">命令字符串类型</param>
        /// <param name="cmdText">命令字符串</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>影响的行数</returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行一条SQL命令，返回影响的行数
        /// </summary>
        /// <example>
        /// int result = ExecuteNonQuery(myConnection,CommandType.Text , "Update table1 set field1 = @fild1 where id = @id", myCommandParameters)
        /// </example>
        /// <param name="connection">SqlConnection实例</param>
        /// <param name="cmdType">命令字符串类型</param>
        /// <param name="cmdText">命令字符串</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>影响的行数</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行数据库事务，返回影响的行数
        /// </summary>
        /// <param name="connection">SqlConnection实例</param>
        /// <param name="cmdType">命令字符串类型</param>
        /// <param name="cmdText">命令字符串</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>影响的行数</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行一条SQL命令，返回SqlDataReader
        /// </summary>
        /// <param name="cmdType">命令字符串类型</param>
        /// <param name="cmdText">命令字符串</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(ConnectionString);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch(Exception e)
            {
                HJlog.toLog(e.ToString());
                conn.Close();

                throw;
            }
        }

        /// <summary>
        /// 执行一条SQL命令，返回执行结果
        /// </summary>
        /// <param name="cmdType">命令字符串类型</param>
        /// <param name="cmdText">命令字符串</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>object</returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        ///  执行一条SQL命令，返回执行结果
        /// </summary>
        /// <param name="connection">SqlConnection实例</param>
        /// <param name="cmdType">命令字符串类型</param>
        /// <param name="cmdText">命令字符串</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>object</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }
        /// <summary>
        /// 执行一条SQL命令，返回执行结果
        /// </summary>
        /// <param name="cmdType">命令字符串类型</param>
        /// <param name="cmdText">命令字符串</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>object</returns>
        public static string ExecuteScalarr(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                string i = "";

                if (cmd.ExecuteScalar() != null)
                {
                    i = cmd.ExecuteScalar().ToString();
                }
                else
                {
                    i = "";
                }



                cmd.Parameters.Clear();
                return i;
            }
        }

        /// <summary>
        /// 构建SqlCommand
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// 执行多次同一SQL不同参数语句，实现数据库事务。
        /// </summary>
        /// <param name="cmdType">SQL类型</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="cmdParms">参数集</param>
        public static bool ExecuteSqlTran(CommandType cmdType, string cmdText, ArrayList arrayParms)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlTransaction trans = connection.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //循环
                        foreach (SqlParameter[] commandParameters in arrayParms)
                        {
                            PrepareCommand(cmd, connection, trans, cmdType, cmdText, commandParameters);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        return true;
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的不排序哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static bool ExecuteSqlTran(CommandType cmdType, Hashtable SqlList)
        {
            bool res = true;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlTransaction trans = connection.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //循环
                        foreach (string key in SqlList.Keys)
                        {
                            string cmdText = key;
                            SqlParameter[] commandParameters = (SqlParameter[])SqlList[key];

                            PrepareCommand(cmd, connection, trans, cmdType, cmdText, commandParameters);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        res = false;
                    }
                }
            }
            return res;
        }

        public static bool ExecuteSqlTran(CommandType cmdType, ArrayList cmdTexts, ArrayList arrayParms)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlTransaction trans = connection.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //循环
                        for (int x = 0; x < cmdTexts.Count; x++)
                        {
                            PrepareCommand(cmd, connection, trans, cmdType, (string)cmdTexts[x], (SqlParameter[])arrayParms[x]);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ee)
                    {
                        trans.Rollback();
                        Hangjing.AppLog.AppLog.Error(ee.Message);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句返回DataSet
        /// </summary>
        /// <param name="cmdType">查询语句类型</param>
        /// <param name="cmdText">查询语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, cmdType, cmdText, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }

        /// <summary>
        /// 受否存在记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static bool ExistsRecord(string sql, params SqlParameter[] cmdParms)
        {
            object obj = GetObject(ConnectionString, CommandType.Text, sql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //add by zheng_jianfeng 2009-03-14
        /*执行一条计算查询结果语句，返回（object）查询结果
         * 输入：计算查询结果语句
         * 输出：（object）查询结果
        */
        public static object GetObject(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, cmdParms);
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    throw new Exception(e.Message);
                }
            }
        }//public static object GetObject

        /// <summary>
        /// SQL SERVER SQL语句转义
        /// </summary>
        /// <param name="str">需要转义的关键字符串</param>
        /// <param name="pattern">需要转义的字符数组</param>
        /// <returns>转义后的字符串</returns>
        public static string RegEsc(string str)
        {
            string[] pattern = { @"%", @"_", @"'" };
            foreach (string s in pattern)
            {
                //Regex rgx = new Regex(s);
                //keyword = rgx.Replace(keyword, "\\" + s);
                switch (s)
                {
                    case "%":
                        str = str.Replace(s, "[%]");
                        break;
                    case "_":
                        str = str.Replace(s, "[_]");
                        break;
                    case "'":
                        str = str.Replace(s, "['']");
                        break;

                }

            }
            return str;
        }

        //替换关键字为红色
        public static string KeyWordChange(string str, string keyword)
        {
            //1.去除html格式
            string StrNohtml = System.Text.RegularExpressions.Regex.Replace(str, "<[^>]+>", "");
            StrNohtml = System.Text.RegularExpressions.Regex.Replace(StrNohtml, "&[^;]+;", "");

            string[] aryReg =
            {  
                @"<script[^>]*?>.*?</script>",  

                @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",  
                @"([\r\n])[\s]+",  
                @"&(quot|#34);",  
                @"&(amp|#38);",  
                @"&(lt|#60);",  
                @"&(gt|#62);",    
                @"&(nbsp|#160);",    
                @"&(iexcl|#161);",  
                @"&(cent|#162);",  
                @"&(pound|#163);",  
                @"&(copy|#169);",  
                @"&#(\d+);",  
                @"-->",  
                @"<!--.*\n"  
            };

            string[] aryRep = 
            {  
                "",  
                "",  
                "",  
                "\"",  
                "&",  
                "<",  
                ">",  
                "   ",  
                "\xa1",//chr(161),  
                "\xa2",//chr(162),  
                "\xa3",//chr(163),  
                "\xa9",//chr(169),  
                "",  
                "\r\n",  
                ""  
            };
            string newReg = aryReg[0];
            string strOutput = StrNohtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                StrNohtml = regex.Replace(StrNohtml, aryRep[i]);
            }
            StrNohtml.Replace("<", "");
            StrNohtml.Replace(">", "");
            StrNohtml.Replace("\r\n", "");

            //2.替换关键字成红色字体
            string str01, str02;
            string[] keywords;
            string keyword_tag = "";
            keyword = keyword.Replace("　", " ");
            str01 = StrNohtml.Replace(keyword, "<font color='#ff0000'>" + keyword + "</font>");
            keywords = keyword.Split(' ');
            if (keywords.Length > 0)
            {
                str02 = StrNohtml;
                for (int i = 0; i < keywords.Length; i++)
                {
                    str02 = str02.Replace(keywords[i], "<font color='#ff0000'>" + keywords[i] + "</font>");
                }
                keyword_tag = str02;
            }
            else
            {
                keyword_tag = str01;
            }

            return keyword_tag;
        }

        //add by zheng_jianfeng 2009-10-22
        /// <summary>
        /// 运行多条SQL命令 SQL命令含有GO命令的
        /// </summary>
        /// <param name="commandText">SQL命令字符串</param>
        public static void ExecuteMultiSqlCommand(string commandText)
        {
            ExecuteMultiSqlCommand(commandText, "\r\nGO\r\n");
        }

        //add by zheng_jianfeng 2009-10-22
        /// <summary>
        /// 运行含有GO命令的多条SQL命令
        /// </summary>
        /// <param name="commandText">SQL命令字符串</param>
        /// <param name="splitter">分割字符串</param>
        public static void ExecuteMultiSqlCommand(string commandText, string splitter)
        {
            int startPos = 0;

            do
            {
                int lastPos = commandText.IndexOf(splitter, startPos);
                int len = (lastPos > startPos ? lastPos : commandText.Length) - startPos;
                string query = commandText.Substring(startPos, len);

                if (query.Trim().Length > 0)
                {
                    try
                    {
                        ExecuteNonQuery(CommandType.Text, query, null);
                    }
                    catch { ;}
                }

                if (lastPos == -1)
                {
                    break;
                }
                else
                {
                    startPos = lastPos + splitter.Length;
                }
            } while (startPos < commandText.Length);
        }

        /// <summary>
        /// 返回查询一个表中记录条数的sql语句
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>The SELECT query.</returns>
        public static string SelectCountFrom(string table)
        {
            StringBuilder sb = new StringBuilder(100);

            sb.Append("select count(*) from ");
            sb.Append(table);

            return sb.ToString();
        }

        /// <summary>
        /// 返回删除一个表中记录的sql语句
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>The DELETE FROM query, without any WHERE clause.</returns>
        public static string DeleteFrom(string table)
        {
            StringBuilder sb = new StringBuilder(100);
            sb.Append("delete from ");
            sb.Append(table);

            return sb.ToString();
        }

        /// <summary>
        /// 更新一个DateTime字段的值 where是查询语句 需要带where 如：where DataId=1或者 where DataId in (1,2,3)
        /// </summary>
        public static int UpdateValue(string tablename, string param, DateTime Value, string Where)
        {
            SqlParameter[] parameters = 
            {
                new SqlParameter("@value", SqlDbType.DateTime),
                new SqlParameter("@pid", SqlDbType.Int,4)
            };
            parameters[0].Value = Value;

            StringBuilder sql = new StringBuilder("Update " + tablename + " Set ");

            sql.Append(param);
            sql.Append("=@value ");
            sql.Append(Where);

            return (int)SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一个int字段的值 where是查询语句 需要带where 如：where DataId=1或者 where DataId in (1,2,3)
        /// </summary>
        public static int UpdateValue(string tablename, string param, int intValue, string Where)
        {
            SqlParameter[] parameters = 
            {
                new SqlParameter("@value", SqlDbType.Int),
            };
            parameters[0].Value = intValue;

            StringBuilder sql = new StringBuilder("Update " + tablename + " Set ");
            sql.Append(param);
            sql.Append("=@value ");
            sql.Append(Where);

            return (int)SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一个string字段的值
        /// where是查询语句 需要带where 如：where DataId=1或者 where DataId in (1,2,3)
        ///</summary>
        public static int UpdateValue(string tablename, string param, string strValue, string Where)
        {
            SqlParameter[] parameters = 
            {
                new SqlParameter("@value", SqlDbType.VarChar,4096),
            };
            parameters[0].Value = strValue;

            StringBuilder sql = new StringBuilder("Update " + tablename + " Set ");
            sql.Append(param);
            sql.Append("=@value ");
            sql.Append(Where);

            return (int)SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);

        }

        /// <summary>
        /// 更新一个string字段的值
        /// where是查询语句 需要带where 如：where DataId=1或者 where DataId in (1,2,3)
        ///</summary>
        public static int UpdateValue(string tablename, string param, decimal decValue, string Where)
        {
            SqlParameter[] parameters = 
            {
                new SqlParameter("@value", SqlDbType.VarChar,4096),
            };
            parameters[0].Value = decValue;

            StringBuilder sql = new StringBuilder("Update " + tablename + " Set ");
            sql.Append(param);
            sql.Append("=@value ");
            sql.Append(Where);

            return (int)SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);

        }


        /// <summary>
        /// 执行语句
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strDate">要更新的SQL语句</param>
        /// <param name="strWhere">条件</param>
        /// <returns>返回是否更新成功</returns>
        /// <example>UpOnlyDate("C_Supply", "Verify=3","id=1")</example>
        public static int excutesql(string sql)
        {
            return SQLHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        #region public void SqlBulkCopyData(DataTable dt) 利用Net SqlBulkCopy 批量导入数据库,速度超快
        /// <summary>
        /// 利用Net SqlBulkCopy 批量导入数据库,速度超快
        /// </summary>
        /// <param name="dt">源内存数据表</param>
        public static void SqlBulkCopyData(DataTable dt)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlTransaction tran = connection.BeginTransaction())
                {
                    // 批量保存数据，只能用于Sql
                    SqlBulkCopy sqlbulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, tran);
                    // 设置源表名称
                    sqlbulkCopy.DestinationTableName = dt.TableName;
                    // 设置超时限制
                    sqlbulkCopy.BulkCopyTimeout = 1000;

                    foreach (DataColumn dtColumn in dt.Columns)
                    {
                        sqlbulkCopy.ColumnMappings.Add(dtColumn.ColumnName, dtColumn.ColumnName);
                    }
                    try
                    {
                        // 写入
                        sqlbulkCopy.WriteToServer(dt);
                        // 提交事务
                        tran.Commit();
                    }
                    catch(Exception e)
                    {
                        tran.Rollback();
                        sqlbulkCopy.Close();
                        HJlog.toLog(e.ToString());
                    }
                    finally
                    {
                        sqlbulkCopy.Close();
                        connection.Close();
                    }
                }
            }
        }
        #endregion

    }
}
