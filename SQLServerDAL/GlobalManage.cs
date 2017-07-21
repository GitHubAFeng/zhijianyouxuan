using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using SQLDMO;
using System.Collections.Generic;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;
using Hangjing.DBUtility;

// GlobalManage.cs :class GlobalManage
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-01-13

namespace Hangjing.SQLServerDAL
{
    public class GlobalManage
    {
        public string BackUpDatabase(string backuppath, string ServerName, string UserName, string Password, string strDbName, string strFileName)
        {
            SQLServer svr = new SQLServerClass();
            try
            {
                svr.Connect(ServerName, UserName, Password);
                Backup bak = new BackupClass();
                bak.Action = 0;
                bak.Initialize = true;
                bak.Files = backuppath + strFileName;//文件加一个config后缀
                bak.Database = strDbName;
                bak.SQLBackup(svr);
                return string.Empty;
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("'", " ");
                message = message.Replace("\n", " ");
                message = message.Replace("\\", "/");
                return message;
            }
            finally
            {
                svr.DisConnect();
            }
        }

        public string RestoreDatabase(string backuppath, string ServerName, string UserName, string Password, string strDbName, string strFileName)
        {
            #region 数据库的恢复的代码

            SQLServer svr = new SQLServerClass();
            try
            {
                svr.Connect(ServerName, UserName, Password);
                QueryResults qr = svr.EnumProcesses(-1);
                int iColPIDNum = -1;
                int iColDbName = -1;
                for (int i = 1; i <= qr.Columns; i++)
                {
                    string strName = qr.get_ColumnName(i);
                    if (strName.ToUpper().Trim() == "SPID")
                    {
                        iColPIDNum = i;
                    }
                    else if (strName.ToUpper().Trim() == "DBNAME")
                    {
                        iColDbName = i;
                    }
                    if (iColPIDNum != -1 && iColDbName != -1)
                        break;
                }

                for (int i = 1; i <= qr.Rows; i++)
                {
                    int lPID = qr.GetColumnLong(i, iColPIDNum);
                    string strDBName = qr.GetColumnString(i, iColDbName);
                    if (strDBName.ToUpper() == strDbName.ToUpper())
                        svr.KillProcess(lPID);
                }


                Restore res = new RestoreClass();
                res.Action = 0;
                string path = backuppath + strFileName + ".config";
                res.Files = path;

                res.Database = strDbName;
                res.ReplaceDatabase = true;
                res.SQLRestore(svr);

                return string.Empty;
            }
            catch (Exception err)
            {
                string message = err.Message.Replace("'", " ");
                message = message.Replace("\n", " ");
                message = message.Replace("\\", "/");

                return message;
            }
            finally
            {
                svr.DisConnect();
            }

            #endregion
        }

        /// <summary>
        /// 添加访问日志
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="username"></param>
        /// <param name="groupid"></param>
        /// <param name="grouptitle"></param>
        /// <param name="ip"></param>
        /// <param name="actions"></param>
        /// <param name="others"></param>
        public void AddVisitLog(int uid, string username, int groupid, string grouptitle, string ip, string actions, string others)
        {

        }

        /// <summary>
        /// 添加词语过滤
        /// </summary>
        /// <param name="username"></param>
        /// <param name="find"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public int AddWord(string username, string find, string replacement)
        {
            return 1;
        }


        #region 系统日志
        
        private const int EstimatedLogInfoSize = 100; //估计的每条日志记录的大小在100字节左右

        /// <summary>
        /// Converts an <see cref="T:EntryType" /> to its character representation.
        /// </summary>
        /// <param name="type">The <see cref="T:EntryType" />.</param>
        /// <returns>Th haracter representation.</returns>
        private char LogTypeToChar(LogType type)
        {
            switch (type)
            {
                case LogType.Error:
                    return 'E';
                case LogType.Warning:
                    return 'W';
                case LogType.General:
                    return 'G';
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Converts the character representation of an <see cref="T:EntryType" /> back to the enumeration value.
        /// </summary>
        /// <param name="c">The character representation.</param>
        /// <returns>The<see cref="T:EntryType" />.</returns>
        private LogType LogTypeFromChar(char c)
        {
            switch (char.ToUpperInvariant(c))
            {
                case 'E':
                    return LogType.Error;
                case 'W':
                    return LogType.Warning;
                case 'G':
                    return LogType.General;
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// 写入一条日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="entryType"></param>
        /// <param name="user"></param>
        public void AddSystemLog(string message, LogType entryType, string user)
        {
            if (message == null) throw new ArgumentNullException("message");
            if (message.Length == 0) throw new ArgumentException("Message cannot be empty", "message");
            if (user == null) throw new ArgumentNullException("user");
            if (user.Length == 0) throw new ArgumentException("User cannot be empty", "user");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ESystemLog(");
            strSql.Append("LogTime,LogType,[User],Message)");
            strSql.Append(" values (");
            strSql.Append("@LogTime,@LogType,@User,@Message)");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@LogTime", SqlDbType.DateTime),
				new SqlParameter("@LogType", SqlDbType.Char,1),
				new SqlParameter("@User", SqlDbType.NVarChar,100),
				new SqlParameter("@Message", SqlDbType.NVarChar,4000)
            };
            parameters[0].Value = DateTime.Now;
            parameters[1].Value = LogTypeToChar(entryType);
            parameters[2].Value = user;
            parameters[3].Value = message;

            try
            {
                SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);

                //如果日志大小超过系统设置的日志大小则进行裁剪以减少日志所占用的空间
                int logSize = GetLogSize();
                if (logSize > int.Parse(HjNetHelper.GetSiteConfig().MaxSystemLogSize))
                {
                    
                    CutLog((int)(logSize * 0.75));
                }
            }
            catch { }
        }

        /// <summary>
        /// 裁剪日志
        /// </summary>
        /// <param name="size"></param>
        private void CutLog(int size)
        {
            string sql1 = SQLHelper.SelectCountFrom("ESystemLog");
            
            //获取当前记录条数
            int rows = HJConvert.ToInt32(SQLHelper.ExecuteScalar( CommandType.Text, sql1, null));

            if (rows == -1)
            {
                return;
            }

            //计算当前的记录大小
            int estimatedSize = rows * EstimatedLogInfoSize;

            //如果当前的记录大小大于要保留的大小则执行一下裁剪操作
            if (size < estimatedSize)
            {
                //当前的大小和要保留的大小之间的差值
                int difference = estimatedSize - size;
                //需要删除的记录条数
                int entriesToDelete = difference / EstimatedLogInfoSize;
                
                //entriesToDelete += entriesToDelete / 10;

                if (entriesToDelete > 0)
                {

                    string sql2 = "select id from ESystemLog order by id asc";

                    //查找要删除的记录的id
                    List<int> ids = new List<int>(entriesToDelete);
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql2, null))
                    {
                        if (dr.Read())
                        {
                            while (dr.Read() && ids.Count < entriesToDelete)
                            {
                                ids.Add((int)dr["Id"]);
                            }
                        }
                    }
                    
                    if (ids.Count > 0)
                    {
                        //where in 中的参数个数做限制 50个为一组

                        for (int chunk = 0; chunk <= ids.Count / 50; chunk++)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append("delete from ESystemLog where id in(");

                            for (int i = chunk * 50; i < Math.Min(ids.Count, (chunk + 1) * 50); i++)
                            {
                                sb.Append(ids[i]);

                                if (i != Math.Min(ids.Count, (chunk + 1) * 50) - 1)
                                {
                                    sb.Append(", ");
                                }
                            }

                            sb.Append(")");

                            if (!(SQLHelper.ExecuteNonQuery(CommandType.Text, sb.ToString(), null) > 0))
                            {
                                return;
                            }
                        }
                    }

                }
            }
        }

        /// <summary>
        /// Gets all the Log Entries, sorted by date/time (oldest to newest).
        /// </summary>
        /// <returns>The Log Entries.</returns>
        public IList<LogInfo> GetLogList(int pagesize, int pageindex, string strWhere, string orderName, int orderType)
        {

            IList<LogInfo> infos = new List<LogInfo>();

            SqlParameter[] parameters = 
            {
				new SqlParameter("@tblName", SqlDbType.VarChar,255),
				new SqlParameter("@strGetFields", SqlDbType.VarChar,1000),
				new SqlParameter("@primary", SqlDbType.VarChar,255),
                new SqlParameter("@orderName", SqlDbType.VarChar,255),
				new SqlParameter("@PageSize", SqlDbType.Int),					
				new SqlParameter("@PageIndex", SqlDbType.Int),
				new SqlParameter("@OrderType", SqlDbType.Bit),
				new SqlParameter("@strWhere", SqlDbType.VarChar,1500)
             };
            parameters[0].Value = "ESystemLog";
            parameters[1].Value = "Id,LogTime, LogType, [User], Message";
            parameters[2].Value = "Id";
            parameters[3].Value = orderName;
            parameters[4].Value = pagesize;
            parameters[5].Value = pageindex;
            parameters[6].Value = orderType;
            parameters[7].Value = strWhere;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.StoredProcedure, "pageselectpri", parameters))
            {
                while (dr.Read())
                {
                    LogInfo model = new LogInfo(HJConvert.ToInt32(dr["Id"]), LogTypeFromChar(dr["LogType"].ToString().ToCharArray()[0]), HJConvert.ToDateTime(dr["LogTime"]), HJConvert.ToString(dr["message"]), HJConvert.ToString(dr["User"]));
                    infos.Add(model);
                }
            }

            return infos;
        }

        /// <summary>
        /// Clear the Log.
        /// </summary>
        public int ClearLog()
        {
            string sql = SQLHelper.DeleteFrom("ESystemLog");

            return SQLHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        /// <summary>
        /// 获取当前系统日志的大小 返回kb单位的大小
        /// </summary>
        public int GetLogSize()
        {
            string sql = SQLHelper.SelectCountFrom("ESystemLog");
            int rows = 0;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                if (dr.Read())
                {
                    rows = HJConvert.ToInt32(dr[0]);
                }
            }

            if (rows == -1)
            {
                return 0;
            }

            int estimatedSize = rows * EstimatedLogInfoSize;

            return estimatedSize / 1024;
        }

        /// <summary>
        /// 获取日志记录条数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int GetLogCount(string sqlWhere)
        {
            string sql = SQLHelper.SelectCountFrom("ESystemLog");
            int rows = 0;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                if (dr.Read())
                {
                    rows = HJConvert.ToInt32(dr[0]);
                }
            }

            return rows;
        }

        /// <summary>
        /// 删除一条日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteLog(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ESystemLog ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = 
            {
				new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

            return SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 批量删除日志
        /// </summary>
        /// <param name="logs"></param>
        /// <returns></returns>
        public int DeleteLogs(string logs)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from ESystemLog where Id in ({0})");

            int i = SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(str.ToString(), logs), null);
            return i;
        }

        #endregion

        
    }
}
