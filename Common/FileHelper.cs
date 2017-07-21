// FileHelper.cs
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-01-12
// 文件、文件夹帮助类

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.UI;
using Microsoft.VisualBasic;
using System.Collections;
using System.Net;
using Scripting;



namespace Hangjing.Common
{
    public static class FileHelper
    {
        /// <summary>
        /// 返回文件是否存在
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否存在</returns>
        public static bool FileExists(string filename)
        {
            return System.IO.File.Exists(filename);
        }

        /// <summary>
        /// 建立文件夹
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool CreateDir(string name)
        {
            return FileHelper.MakeSureDirectoryPathExists(name);
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>创建是否成功</returns>
        [DllImport("dbgHelp", SetLastError = true)]
        private static extern bool MakeSureDirectoryPathExists(string name);

        /// <summary>
        /// 判断目录是否存在
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static bool IsDirectoryExists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        /// <summary>
        /// 获取路径中的目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetDirectoryPath(string path)
        {
            if (!StringHelper.IsNullorEmpty(Path.GetExtension(path)))
            {
                return Path.GetDirectoryName(path);
            }
            return path;
        }

        /// <summary>
        /// 如果不存在该目录则创建目录
        /// </summary>
        /// <param name="path"></param>
        public static void CreateDirectoryIfNotExists(string path)
        {
            string directoryPath = GetDirectoryPath(path);
            if (!IsDirectoryExists(directoryPath))
            {
                try
                {
                    Directory.CreateDirectory(directoryPath);
                }
                catch
                {
                    FileSystemObject obj2 = new FileSystemObjectClass();
                    string[] strArray = directoryPath.Split(new char[] { '\\' });
                    string str2 = strArray[0];
                    for (int i = 1; i < strArray.Length; i++)
                    {
                        str2 = str2 + @"\" + strArray[i];
                        if (!(!StringHelper.Contains(str2.ToLower(), ConsoleHelper.Instance.PhysicalApplicationPath.ToLower()) || IsDirectoryExists(str2)))
                        {
                            obj2.CreateFolder(str2);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 属否存在路径对应的文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsFileExists(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }

        /// <summary>
        /// 如果存在该路径对应的文件则删除文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool DeleteFileIfExists(string filePath)
        {
            bool flag = true;
            try
            {
                if (IsFileExists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            catch
            {
                try
                {
                    FileSystemObject obj2 = new FileSystemObjectClass();
                    obj2.DeleteFile(filePath, true);
                }
                catch
                {
                    flag = false;
                }
            }
            return flag;
        }

        /// <summary>
        /// 备份文件
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <param name="overwrite">当目标文件存在时是否覆盖</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!System.IO.File.Exists(sourceFileName))
            {
                throw new FileNotFoundException(sourceFileName + "文件不存在！");
            }
            if (!overwrite && System.IO.File.Exists(destFileName))
            {
                return false;
            }
            try
            {
                System.IO.File.Copy(sourceFileName, destFileName, true);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 备份文件,当目标文件存在时覆盖
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName)
        {
            return BackupFile(sourceFileName, destFileName, true);
        }

        /// <summary>
        /// 恢复文件
        /// </summary>
        /// <param name="backupFileName">备份文件名</param>
        /// <param name="targetFileName">要恢复的文件名</param>
        /// <param name="backupTargetFileName">要恢复文件再次备份的名称,如果为null,则不再备份恢复文件</param>
        /// <returns>操作是否成功</returns>
        public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName)
        {
            try
            {
                if (!System.IO.File.Exists(backupFileName))
                {
                    throw new FileNotFoundException(backupFileName + "文件不存在！");
                }
                if (backupTargetFileName != null)
                {
                    if (!System.IO.File.Exists(targetFileName))
                    {
                        throw new FileNotFoundException(targetFileName + "文件不存在！无法备份此文件！");
                    }
                    else
                    {
                        System.IO.File.Copy(targetFileName, backupTargetFileName, true);
                    }
                }
                System.IO.File.Delete(targetFileName);
                System.IO.File.Copy(backupFileName, targetFileName);
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public static bool RestoreFile(string backupFileName, string targetFileName)
        {
            return RestoreFile(backupFileName, targetFileName, null);
        }

        /// <summary>
        /// 组合目录
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static string Combine(params string[] paths)
        {
            string str = string.Empty;
            if ((paths != null) && (paths.Length > 0))
            {
                str = (paths[0] != null) ? paths[0].Replace('/', '\\').TrimEnd(new char[] { '\\' }) : string.Empty;
                for (int i = 1; i < paths.Length; i++)
                {
                    string str2 = (paths[i] != null) ? paths[i].Replace('/', '\\').Trim(new char[] { '\\' }) : string.Empty;
                    str = Path.Combine(str, str2);
                }
            }
            return str;
        }

        public static string GetPathDifference(string rootPath, string path)
        {
            if (!(StringHelper.IsNullorEmpty(path) || !StringHelper.StartsWithIgnoreCase(path, rootPath)))
            {
                return path.Substring(rootPath.Length, path.Length - rootPath.Length).Trim(new char[] { '/', '\\' });
            }
            return string.Empty;
        }

        public static string ParseNavigationUrl(string url, string domainUrl)
        {
            string str = string.Empty;
            if (!StringHelper.IsNullorEmpty(url))
            {
                if (url.StartsWith("~"))
                {
                    str = Combine(domainUrl, url.Substring(1));
                }
                else
                {
                    str = url;
                }
            }
            return str;
        }

        public static string ParseNavigationUrl(string url)
        {
            return ParseNavigationUrl(url, ConsoleHelper.Instance.ApplicationPath);
        }

        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public static String GetFileName(string filename)
        {
            int l = filename.LastIndexOf("/");
            return filename.Substring(l + 1, filename.Length - l - 1);
        }
    }
}
