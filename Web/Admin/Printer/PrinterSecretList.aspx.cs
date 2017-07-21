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

// 打印机列表管理
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com 更新
// 2010-07-13

public partial class qy_54tss_Admin_Printer_PrinterSecretList : System.Web.UI.Page
{
    private int PageIndex
    {
        get
        {
            object o = ViewState["PageIndex"];
            return (o == null) ? 1 : Convert.ToInt32(o);
        }
        set
        {
            ViewState["PageIndex"] = value;
        }
    }

    private string SqlWhere
    {
        get
        {
            object o = ViewState["SqlWhere"];
            return (o == null) ? "" : Convert.ToString(o);
        }
        set
        {
            ViewState["SqlWhere"] = value;
        }
    }

    Hangjing.SQLServerDAL.PrinterSecret bll = new Hangjing.SQLServerDAL.PrinterSecret();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PageIndex = 1;
            if (!string.IsNullOrEmpty(Request.QueryString["P"]))
            {
                PageIndex = Convert.ToInt32(Request.QueryString["p"]);
            }

            if (!string.IsNullOrEmpty(HjNetHelper.GetQueryString("type")))
            {
                SqlWhere = "isuse=" + HjNetHelper.GetQueryInt("type", 0).ToString() + "";
            }

            BindData();
        }
    }

    protected void BindData()
    {
        this.AspNetPager1.RecordCount = bll.GetCount(SqlWhere);

        this.rptPrinterList.DataSource = bll.GetList(AspNetPager1.PageSize, PageIndex, SqlWhere, "DataId", 1);
        this.rptPrinterList.DataBind();

        AlertScript.AjaxRegisterScript("init();", UpdatePanel1);
    }

    /// <summary>
    /// 搜索
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSearch_Click(object sender, EventArgs e)
    {
        AlertScript.RegScript(this.Page, UpdatePanel1, "loading();");

        SqlWhere = " 1=1 ";

        if (ddlSearchType.SelectedValue == "Num")
        {
            SqlWhere += " and PrinterNum like '%" + Utils.RegEsc(WebUtility.InputText(this.tbKeyword.Text.Trim())) + "%' ";
        }
        if (ddlSearchType.SelectedValue == "Sn")
        {
            SqlWhere += " and PrinterSn like '%" + Utils.RegEsc(WebUtility.InputText(this.tbKeyword.Text.Trim())) + "%'";
        }

        BindData();

    }

    protected void rptPrinterList_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        string type = e.CommandName;
        switch (type)
        {
            case "del":
               
                if (bll.DelList(e.CommandArgument.ToString()) > 0)
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
                    BindData();
                }
                else
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
                }
                break;

        }
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DelList_Click(object sender, EventArgs e)
    {
        //判断权限
       
        string IdList = this.hdDels.Value;
        if (bll.DelList(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
            BindData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
            BindData();
        }
    }

    protected void ddlSearchType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    TogoPrinter daltp = new TogoPrinter();
    protected string GetTogo(object sn2)
    {
        string rs = "暂无商店";
        IList<TogoPrinterInfo> list = daltp.GetList(1, 1, "PrinterSn= '" + sn2 + "'", "dataid", 1);
        if (list.Count > 0)
        {
            rs = "<font color='green'>" + list[0].TogoName + "</font>";
        }
        return rs;
    }
    protected string Getphone(object sn2)
    {
        string rs = "暂无设置";
        IList<TogoPrinterInfo> list = daltp.GetList(1, 1, "PrinterSn= '" + sn2 + "'", "dataid", 1);
        if (list.Count > 0)
        {
            rs = "<font color='green'>" + list[0].TogoNum + "</font>";
        }
        return rs;
    }

    protected string Getcount(object sn1)
    {
        string rs = "未使用";
        IList<TogoPrinterInfo> list = daltp.GetList(1, 1, "PrinterSn= '" + sn1 + "'", "dataid", 1);
        if (list.Count > 0)
        {
            rs = "<font color='green'>使用中</font>";
        }

        return rs;
    }
}
