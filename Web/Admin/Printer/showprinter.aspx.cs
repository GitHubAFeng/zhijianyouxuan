using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;

public partial class qy_54tss_Admin_showprinter : System.Web.UI.Page
{
    TogoPrinter dal = new TogoPrinter();

    protected string SqlWhere
    {
        set
        {
            ViewState["sqlwhere"] = value;
        }
        get
        {
            return ViewState["sqlwhere"] == null ? "" : ViewState["sqlwhere"].ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
            BindData();
        }
    }

    private void BindData()
    {
        IList<TogoPrinterInfo> list = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "dataid", 1);
        foreach (var item in list)
        {
            TimeSpan ts1 = new TimeSpan(Convert.ToDateTime(item.LastLoginDate).Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            double minu = Math.Abs(ts.TotalMinutes);
        }
        rtpUserlist.DataSource = list;
        rtpUserlist.DataBind();
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = "PrinterSn like '%" + tbKeyword.Text + "%'";

        AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        BindData();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }

    /// <summary>
    /// 五分鐘內未更新則視為離線
    /// </summary>
    /// <param name="lastlogintime"></param>
    /// <returns></returns>
    public string GetStatus(string lastlogintime)
    {
        TimeSpan ts1 = new TimeSpan(Convert.ToDateTime(lastlogintime).Ticks);
        TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
        TimeSpan ts = ts1.Subtract(ts2).Duration();
        double minu = Math.Abs(ts.TotalMinutes);

        if (minu < 5 && minu > -1)
        {
            return "在线";
        }
        else
        {
            return "离线";
        }
    }

    protected void rptUserList_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "del":
                {
                    
                    //把这个打印机设置成未用
                    string sql = " update EPrinterSecret set IsUse = 0 where PrinterNum in (select PrinterSn from ETogoPrinter where DataId = " + id + ")";
                    WebUtility.excutesql(sql);
                    if (dal.DelTogoPrinter(id) > 0)
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','','true','text');init();");
                        BindData();
                    }
                    else
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','','true','text');init();");
                        BindData();
                    }
                }
                break;
        }
    }
}
