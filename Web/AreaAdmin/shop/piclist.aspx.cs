﻿using System;
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

public partial class qy_54tss_AreaAdmin_aboutus_piclist : System.Web.UI.Page
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

    ShopFoodPicture dal = new ShopFoodPicture();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlWhere = " 1=1 ";
            BindData();
        }
    }

    private void BindData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rtpGifts.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "IID", 1);
        this.rtpGifts.DataBind();
        AlertScript.RegScript(this.Page, UpdatePanel1, "init();");
        CacheHelper.ClearShopPicTag();
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = " 1=1 ";
        if (tbKeyword.Text.Trim() != "")
        {
            SqlWhere += " and title like '%" + WebUtility.InputText(tbKeyword.Text) + "%'";
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

    protected void rtpGifts_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            if (dal.DelShopFoodPicture(Convert.ToInt32(e.CommandArgument)) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除成功','success','true',5);init();");
                BindData();
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除失败','error','true',5);init();");
            }
        }
    }

    /// <summary>
    /// 添加一个图片
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void one_Click(object sender, EventArgs e)
    {
        string url = "addpic.aspx";
        if (Request["tid"] != null)
        {
            url += "?tid="+Request["tid"];
        }
        Response.Redirect(url);
    }


}
