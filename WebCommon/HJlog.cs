using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hangjing.WebCommon
{
    /// <summary>
    /// 写日志 HJlog.toLog("info")
    /// </summary>
    public class HJlogx
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

        public static void toLog(object log)
        {
            string directory = AppDomain.CurrentDomain.BaseDirectory + "logs";
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "logs/" + DateTime.Now.ToString("yyyyMMdd") + "log.txt";
            if (File.Exists(filepath))
            {
                File.SetAttributes(filepath, FileAttributes.Normal);
            }
            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + log);
                writer.Flush();
                writer.Dispose();
            }
        }
    }
}
