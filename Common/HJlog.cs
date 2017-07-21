using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hangjing.Common
{
    /// <summary>
    /// 写日志 HJlog.toLog("info")
    /// </summary>
    public class HJlog
    {
        static String fileName = "EMPPLogFile2.txt";
        static StreamWriter sw = null;
        static StreamReader sr = null;
        static char[] ch;
        private static bool isUse = true;

        private static void init()
        {
            if (File.Exists(fileName))
            {
                sr = new StreamReader(fileName);
                int length = (int)sr.BaseStream.Length;
                ch = new char[length];
                sr.ReadBlock(ch, 0, length);
                sr.Close();
                sw = new StreamWriter(fileName);
                sw.Write(new String(ch));
            }
            else
            {
                sw = new StreamWriter(fileName);
            }
        }

        public static void toLog(object _log)
        {
            Hangjing.AppLog.AppLog.Info(_log.ToString());
        }
    }
}
