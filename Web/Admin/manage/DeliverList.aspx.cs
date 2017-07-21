#region license
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// Function :礼品信息列表
// Created by tuhui at 2010-6-24 16:28:03.
// E-Mail: tuhui@ihangjing.com
#endregion
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
using Hangjing.Model;
using Hangjing.Common;

public partial class qy_55tuan_Admin_DeliverList : System.Web.UI.Page
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

    Deliver dal = new Deliver();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlWhere = " 1=1 ";
            BindData();
            WebUtility.SetDDLCity(ddlcity);

        }
    }

    private void BindData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rtpGifts.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "DataId", 0);
        this.rtpGifts.DataBind();
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init();");
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = " 1=1 ";
        if (tbKeyword.Text != "")
        {
            SqlWhere += " and  Name like '%" + WebUtility.InputText(tbKeyword.Text) + "%'";
        }
        if (ddlcity.SelectedValue != "0")
        {
            SqlWhere += " and Inve1 = " + ddlcity.SelectedValue + "";
        }
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        BindData();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void DelList_Click(object sender, EventArgs e)
    {
        string IdList = this.hidDels.Value;
        //判断权限
        int _rs = WebUtility.checkOperator(4);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        if (dal.DelList(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除成功','success','true',5);init();");
            BindData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除失败','error','true',5);init();");
        }
    }

    protected string ParseState(object x)
    {
        string temp = x.ToString();
        string rs = "";
        switch (temp)
        {
            case "0":
                rs = "离线";
                break;
            case "1":
                rs = "正常";
                break;
        }
        return rs;
    }
    protected string IsWorking(object x)
    {
        string temp = x.ToString();
        string rs = "";
        switch (temp)
        {
            case "0":
                rs = "暂停接单";
                break;
            case "1":
                rs = "正常接单";
                break;
        }
        return rs;
    }


    protected string ApprovedState(object x)
    {
        string temp = x.ToString();
        string rs = "";
        switch (temp)
        {
            case "0":
                rs = "审核通过";
                break;
            case "1":
                rs = "未审核";
                break;
        }
        return rs;
    }

    protected void rtpGifts_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(4);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        if (e.CommandName == "Del")
        {
            if (dal.DelDeliver(Convert.ToInt32(e.CommandArgument)) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除成功','success','true',5);init();");
                BindData();
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除失败','error','true',5);init();");
            }
        }
        //审核
        if (e.CommandName == "Approved")
        {
            if (dal.UpdateValue("IsApproved", 0, " where dataid=" + Convert.ToInt32(e.CommandArgument)) > 0)
            {
                BindData();
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('审核失败','error','true',5);init();");
            }
        }
    }

    protected void clearGPSReclord_Click(object sender, EventArgs e)
    {
        string sql = "TRUNCATE TABLE dbo.GPS_Records";

        WebUtility.excutesql(sql);
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('删除成功','error','true',5);init();Loader.hide();");
    }

    protected void delGPSReclord_Click(object sender, EventArgs e)
    {
        Button lb = (Button)sender;
        int day = Convert.ToInt32(lb.CommandArgument.ToString());

        string sql = "DELETE FROM dbo.GPS_Records WHERE AddTime < '" + DateTime.Now.AddDays(-day) + "'";

        WebUtility.excutesql(sql);
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('删除成功','error','true',5);init();Loader.hide();");
    }


}
