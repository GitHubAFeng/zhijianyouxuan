using System;
using System.Collections;
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

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class shop_Reminder : System.Web.UI.Page
{
    private string SqlWhere
    {
        get
        {
            object o = ViewState["SqlWhere"];
            return (o == null) ? string.Empty : o.ToString();
        }
        set
        {
            ViewState["SqlWhere"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);

        if (!this.Page.IsPostBack)
        {
            SqlWhere = "ReveVar= '" + UserHelp.GetUser_Togo().Unid.ToString() + "'";
            //if (Request["id"] != null)//今天订单 
            //{
            //    SqlWhere += " and OrderDateTime > '" + DateTime.Now.ToShortDateString() + "' and OrderDateTime < '" + DateTime.Now.ToString() + "'";
            //}
            BindData();
        }
    }

    hurryorder dal = new hurryorder();
    protected void BindData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rptOrderList.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "CID", 1);
        this.rptOrderList.DataBind();
        NoRecord();
    }

    private void NoRecord()
    {
        if (rptOrderList.Items.Count == 0)
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "block");
        }
        else
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
    }

    /// <summary>
    /// 以汉字表示订单状态
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public string GetState(int state)
    {
        string result = null;
        switch (state)
        {
            case 0:
                result = "新增订单";
                break;
            case 1:
                result = "新增订单";
                break;
            case 2:
                result = "正在打印";
                break;
            case 3:
                result = "处理成功";
                break;
            case 4:
                result = "处理失败";
                break;
            default:
                result = "超时";
                break;
        }

        return result;
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void rptOrderList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            Response.Redirect("OrderDetail.aspx=" + Convert.ToInt32(e.CommandArgument));
        }
    }

    //protected void btSearch_Click(object sender, EventArgs s)
    //{
    //    SqlWhere = "TogoId = " + UserHelp.GetUser_Togo().Unid + "";

    //    if (Request["id"] != null)//今天订单 
    //    {
    //        SqlWhere += " and OrderDateTime > '" + DateTime.Now.ToShortDateString() + "' and OrderDateTime < '" + DateTime.Now.ToString() + "'";
    //    }
    //    if (tbKeyword.Value != "")
    //    {
    //        SqlWhere += " and orderid like '%" + WebUtility.InputText(tbKeyword.Value) + "%'";
    //    }
    //    if (starttime.Value != "")
    //    {
    //        SqlWhere += "  and  OrderDateTime > '" + WebUtility.InputText(this.starttime.Value.Trim()) + "' ";
    //    }
    //    if (enttime.Value != "")
    //    {
    //        SqlWhere += " and OrderDateTime < '" + WebUtility.InputText(this.enttime.Value.Trim()) + " 23:59:59' ";
    //    }
    //    BindData();
    //}
}
