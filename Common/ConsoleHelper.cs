// ConsoleHelper.cs
// CopyRight (c) 2009 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-01-12
// 机器参数帮助类
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.Management;



namespace Hangjing.Common
{
    public sealed class ConsoleHelper
    {
        // 字段
        private readonly string applicationPath;
        private static ConsoleHelper consoleHelper;
        private readonly string physicalApplicationPath;

        // 方法
        private ConsoleHelper()
        {
        }

        /// <summary>
        /// 访问IP
        /// </summary>
        /// <returns></returns>
        public static string VisiteIp()
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
                }
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return string.Empty;
        }

        /// <summary>
        /// 系统主机名
        /// </summary>
        /// <returns></returns>
        public static string ConsoleHostName()
        {
            return Dns.GetHostName().ToUpper();
        }

        /// <summary>
        /// 计算机的硬盘序列号
        /// </summary>
        /// <returns></returns>
        public static string ConsoleDisk()
        {
            string text = CacheUtils.Get("ComputerUtils_ColumnSerialNumber") as string;
            if (!StringHelper.IsNullorEmpty(text))
            {
                return text;
            }
            text = string.Empty;
            try
            {
                ManagementObjectCollection instances = new ManagementClass("win32_logicaldisk").GetInstances();
                foreach (ManagementObject mo in instances)
                {
                    if (mo["DeviceID"].ToString() == "C:")
                    {
                        text = mo["VolumeSerialNumber"].ToString();
                        goto Label_00BA;
                    }
                }
            }
            catch
            {
            }
        Label_00BA:
            text = text.Replace(":", "");
            CacheUtils.Max("ComputerUtils_ColumnSerialNumber", text);
            return text;
        }

        /// <summary>
        /// 计算机的CPU标识
        /// </summary>
        /// <returns></returns>
        public static string ConsoleCpu()
        {
            string text = CacheUtils.Get("ComputerUtils_ProcessorId") as string;
            if (StringHelper.IsNullorEmpty(text))
            {
                text = string.Empty;
                try
                {
                    ManagementObjectCollection instances = new ManagementClass("win32_Processor").GetInstances();
                    foreach (ManagementObject mo in instances)
                    {
                        text = mo["ProcessorId"].ToString();
                        break;
                    }
                }
                catch
                {
                }
                text = text.Replace(":", "");
                CacheUtils.Max("ComputerUtils_ProcessorId", text);
            }
            return text;
        }

        /// <summary>
        /// 计算机的网卡地址
        /// </summary>
        /// <returns></returns>
        public static string ConsoleNetCard()
        {
            string text = CacheUtils.Get("ComputerUtils_MacAddress") as string;

            if (!StringHelper.IsNullorEmpty(text))
            {
                return text;
            }

            text = string.Empty;

            try
            {
                ManagementObjectCollection instances = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances();
                foreach (ManagementObject mo in instances)
                {
                    if ((bool)mo["IPEnabled"])
                    {
                        text = mo["MacAddress"].ToString();
                        goto Label_00B0;
                    }
                }
            }
            catch
            {
            }
        Label_00B0:
            text = text.Replace(":", "");
            CacheUtils.Max("ComputerUtils_MacAddress", text);
            return text;
        }


        private ConsoleHelper(string physicalApplicationPath, string applicationPath)
        {
            this.physicalApplicationPath = physicalApplicationPath;
            this.applicationPath = applicationPath;
        }

        // 属性
        public string ApplicationPath
        {
            get
            {
                return this.applicationPath;
            }
        }

        /// <summary>
        /// 系统根目录地址
        /// </summary>
        /// <returns></returns>
        public static ConsoleHelper Instance
        {
            get
            {
                if ((consoleHelper == null) && (HttpContext.Current != null))
                {
                    string applicationPath = HttpContext.Current.Request.ApplicationPath;
                    if (StringHelper.IsNullorEmpty(applicationPath))
                    {
                        applicationPath = "/";
                    }
                    consoleHelper = new ConsoleHelper(HttpContext.Current.Request.PhysicalApplicationPath, applicationPath);
                }
                return consoleHelper;
            }
        }

        /// <summary>
        /// 系统物理目录
        /// </summary>
        public string PhysicalApplicationPath
        {
            get
            {
                return this.physicalApplicationPath;
            }
        }

        public static string GetHost()
        {
            string text = string.Empty;
            if (HttpContext.Current != null)
            {
                text = HttpContext.Current.Request.Headers["HOST"];
                if (StringHelper.IsNullorEmpty(text))
                {
                    text = HttpContext.Current.Request.Url.Host;
                }
            }
            return (StringHelper.IsNullorEmpty(text) ? string.Empty : text.Trim().ToLower());
        }

        public enum DatabaseType
        {
            SqlServer,
            Access,
            Unknown
        }

        // 数据库方法
        public static bool DBEquals(DatabaseType type, string typeStr)
        {
            if (StringHelper.IsNullorEmpty(typeStr))
            {
                return false;
            }
            return string.Equals(GetDBTypeValue(type).ToLower(), typeStr.ToLower());
        }

        public static bool DBEquals(string typeStr, DatabaseType type)
        {
            return DBEquals(type, typeStr);
        }

        public static DatabaseType GetDBEnumType(string typeStr)
        {
            DatabaseType unknown = DatabaseType.Unknown;
            if (DBEquals(DatabaseType.SqlServer, typeStr))
            {
                return DatabaseType.SqlServer;
            }
            if (DBEquals(DatabaseType.Access, typeStr))
            {
                return DatabaseType.Access;
            }
            if (DBEquals(DatabaseType.Unknown, typeStr))
            {
                unknown = DatabaseType.Unknown;
            }
            return unknown;
        }

        public static string GetDBTypeText(DatabaseType type)
        {
            if (type == DatabaseType.SqlServer)
            {
                return "Microsoft SQL Server";
            }
            if (type == DatabaseType.Access)
            {
                return "Microsoft Access";
            }
            if (type != DatabaseType.Unknown)
            {
                throw new Exception();
            }
            return "Unknown";
        }

        public static string GetDBTypeValue(DatabaseType type)
        {
            if (type == DatabaseType.SqlServer)
            {
                return "SqlServer";
            }
            if (type == DatabaseType.Access)
            {
                return "Access";
            }
            if (type != DatabaseType.Unknown)
            {
                throw new Exception();
            }
            return "Unknown";
        }

    }
}
