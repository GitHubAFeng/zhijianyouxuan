/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * FileName : Admin_PptList.aspx.cs
 * Function : ppt列表
 * Created by jijunjian at 2010-8-21 14:41:01.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

/// <summary>
/// 促销列表
/// </summary>
public partial class Admin_Promotionlist : System.Web.UI.Page
{
    webPromotionConfig bll = new webPromotionConfig();
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
        if (!Page.IsPostBack)
        {
            WebUtility.BindList("state", "title", new promotionType().GetAllByType(0), tbptype);
            SqlWhere = " 1= 1";
            BindData();
        }
    }

    protected void BindData()
    {
        IList<webPromotionConfigInfo> list = bll.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "pid", 1, 0);
        foreach (var item in list)
        {
            item.url = "addPromotion.aspx?id="+item.pId+"&tid="+item.shopid;
            if (item.shopid > 0)
            {
                item.url = "/Admin/shop/shopPromotion.aspx?id=" + item.pId + "&tid=" + item.shopid;
            }
        }

        this.rtpTogolist.DataSource = list;
        this.rtpTogolist.DataBind();

        AlertScript.AjaxRegisterScript("init();", UpdatePanel1);
    }

    /// <summary>
    /// 事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptUserList_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            //判断权限
            int _rs = WebUtility.checkOperator(4);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            if (bll.DelList(e.CommandArgument.ToString()) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('删除成功','text:删除成功!','250','150','true','1000','true','text');init();");
                BindData();
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
            }
            CacheHelper.ClearWebPromotionConfig();
        }
    }


    protected void DelList_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(4);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        string IdList = this.hdDels.Value;
        if (bll.DelList(IdList) > 0)
        {
            SectionProxyData.ClearPPTList();
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除成功','success','true',5);init();");
            BindData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除失败!','error','true',5);init();");
            BindData();
        }
        CacheHelper.ClearWebPromotionConfig();
    }

    /// <summary>
    /// 搜索
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = " 1= 1";
        if (tbptype.SelectedValue != "-1")
        {
            SqlWhere += " and ptype = '" + tbptype.SelectedValue + "'";
        }
        if (this.tbshopname.Text != "")
        {
            SqlWhere += " and shopid in (select Unid from Points where Name like '%" + Utils.RegEsc(WebUtility.InputText(this.tbshopname.Text.Trim())) + "%')";
        }

        switch (this.ddlshopid.SelectedValue)
        {
            case "0":
                SqlWhere += " and shopid =0 ";
                break;
            case "1":
                SqlWhere += " and shopid >  0 ";
                break;
            default:
                break;
        }

        BindData();
    }
}
