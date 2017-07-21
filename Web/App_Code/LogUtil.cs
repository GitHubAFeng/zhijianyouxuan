using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
///LogUtil 的摘要说明
/// </summary>
public class LogUtil
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

    public static void toLog(String log)
    {
        //if (isUse)
        //{
        //    init();
        //    isUse = false;
        //}
        //sw.WriteLine(DateTime.Now.ToString() + " " + log);
        //sw.Flush();

        using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "logs/log.txt", true))
        {
            writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + log);
            writer.Flush();
            writer.Dispose();
        }
    }
}