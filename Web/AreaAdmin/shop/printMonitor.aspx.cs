using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.DBUtility;
using System.Text;
using System.Net;
using System.ComponentModel;

/// <summary>
/// 打印监控，每30秒刷新一次
/// </summary>
public partial class qy_54tss_AreaAdmin_Sale_printMonitor : System.Web.UI.Page
{
    BackgroundWorker bgw;

    public string msg = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bgw = new BackgroundWorker();
            bgw.WorkerSupportsCancellation = true;
            bgw.WorkerReportsProgress = true;

            bgw.DoWork += new DoWorkEventHandler(DoWork);
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);

            bgw.RunWorkerAsync();
        }
    }

    /// <summary>
    /// 耗时操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void DoWork(object sender, DoWorkEventArgs e)
    {
        try
        {
            //订单提交超过1分，且状态为等待打印，30分以后就不在查询
            string sql = " dateadd(minute,1,addtime) < getdate() and printstate =0  ";//and dateadd(minute,30,addtime) > getdate()
            IList<printorderlogInfo> logs = new printorderlog().GetList(10, 1, sql, "pid", 1);
            foreach (var item in logs)
            {
                FeYinPrinter fy = new FeYinPrinter(item.orderid);
                string data = fy.QueryState(item.orderid);
                if (data.Trim() != "0")
                {
                    sql = "UPDATE dbo.printorderlog SET printstate = " + data + " ,updatetime = GETDATE() WHERE orderid = '" + item.orderid + "' ;UPDATE dbo.Custorder SET deliversiteid = " + data + " WHERE orderid = '" + item.orderid + "';";

                    SQLHelper.excutesql(sql);
                }
                else
                {
                    sql = "UPDATE dbo.printorderlog SET updatetime = GETDATE() WHERE orderid = '" + item.orderid + "' ;";
                    SQLHelper.excutesql(sql);
                }
            }
        }
        catch (Exception ee)
        {
            HJlog.toLog(ee.ToString());
        }

    }

    /// <summary>
    /// 完成
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        divMessages.InnerHtml = msg;
    }
}
