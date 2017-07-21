using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Threading;
using System.Collections.Generic;
using System.IO;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
/// <summary>
///checkMachine 的摘要说明
/// </summary>
public class checkMachine
{
    const int DELAY_LEAVE = 600;               //隔多少秒扫描一次
    const int SCANER = 30000;                 //休眠时间
    private Thread thread;                   //定义内部线程 
    private static bool _flag = false;       //定义唯一标志 
    private DateTime time = DateTime.Now;

    public checkMachine()
    {
        if (!_flag)
        {
            _flag = true;
            this.thread = new Thread(new ThreadStart(ThreadProc));
            thread.Name = "machine_online";
            thread.Start();
        }
    }

    internal void ThreadProc()
    {
        //如果最后活动时间超过目前时间5分钟则说明可能用户已经掉线
        //保存时间
        while (true)
        {
            Togo dal = new Togo();
            IList<TogoPrinterInfo> togo_List = dal.GetAllToCheck();

            if (togo_List.Count != 0)
            {
                for (int i = 0; i < togo_List.Count; i++)
                {
                    if (togo_List[i].LastLoginDate.AddSeconds(DELAY_LEAVE).CompareTo(DateTime.Now) <= 0)//by jijunjian
                    {
                        dal.UpdateState(togo_List[i].TogoId+"", 0, Convert.ToDateTime("1900-01-01"));
                    }
                }
            }
            Thread.Sleep(SCANER);
        }
    }
}


