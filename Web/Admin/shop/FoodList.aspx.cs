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

// 商家列表管理
// CopyRight (c) 2010-2011 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com 餐品信息

public partial class Admin_shop_ShopFoodList :AdminPageBase
{
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

    Foodinfo bll = new Foodinfo();

    protected void Page_Load(object sender, EventArgs e)
    {
     //   CheckRights("A");
        if (!Page.IsPostBack)
        {
            SqlWhere = "1=1 and InUse != 'd' ";
            if (Request["tid"] != null)
            {
                SqlWhere += " And FPMaster =" + HjNetHelper.GetQueryString("tid");
                hidTogoId.Value = HjNetHelper.GetQueryString("tid");
            }
            else
            {
                divop.Style["display"] = "none";
            }
            if (!string.IsNullOrEmpty(HjNetHelper.GetQueryString("type")))
            {
                string type = HjNetHelper.GetQueryString("type");
                SqlWhere += " And InUse = '" + type + "'";
            }
            BindData();
        }
    }

    /// <summary>
    /// 数据
    /// </summary>
    protected void BindData()
    {    
        this.AspNetPager1.RecordCount = bll.GetCount(SqlWhere);
        this.rptFoodlist.DataSource = bll.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "unid", 1);
        this.rptFoodlist.DataBind();

        AlertScript.AjaxRegisterScript("init();", UpdatePanel1);
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = "1=1 and InUse != 'd' ";
        if (Request["tid"] != null)
        {
            SqlWhere += " and FPMaster =" + HjNetHelper.GetQueryString("tid") + " and InUse != 'd '";
        }
        if (!string.IsNullOrEmpty(tbKeyword.Text))
        {
            SqlWhere += " and FoodName like '%" + WebUtility.InputText(tbKeyword.Text) + "%'";
        }
        if (tbshop.Text != "")
        {
            SqlWhere += " and FPMaster in (select unid from points where   Name like '%" + WebUtility.InputText(tbshop.Text) + "%')";
        }
       
        BindData();
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        if (bll.UpdateValue("InUse", "d", "where Unid in (" + IdList + ")") > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
            BindData();
            Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/shop/shop" + HjNetHelper.GetQueryInt("tid", 0) + "/food");
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
        }
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
            if (bll.UpdateValue("InUse", "d", "where Unid in (" + e.CommandArgument + ")") > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
                BindData();
                Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/shop/shop" + HjNetHelper.GetQueryInt("tid", 0) + "/food");
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
            }
        }
    }


    protected void lbDownFood_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(6);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        string IdList = this.hdDels.Value;
        if (bll.UpdateValue("InUse", "n", "where Unid in (" + IdList + ")") > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:批量下架成功!','250','150','true','1000','true','text');init();");
            BindData();
            Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/shop/shop" + HjNetHelper.GetQueryInt("tid", 0) + "/food");
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:批量下架失败!','250','150','true','1000','true','text');init();");
        }
    }

    protected void lbUpFood_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(5);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        string IdList = this.hdDels.Value;
        if (bll.UpdateValue("InUse", "y", "where Unid in (" + IdList + ")") > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:批量上架成功!','250','150','true','1000','true','text');init();");
            BindData();
            Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/shop/shop" + HjNetHelper.GetQueryInt("tid", 0) + "/food");
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:批量上架失败!','250','150','true','1000','true','text');init();");
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected string getState(object s)
    {
        string rs = "";
        switch (s.ToString())
        {
            case "0":
                rs = "<font color='red'>未审核</font>";
                break;
            case "1":
                rs = "<font color='green'>已通过</font>";
                break;
            case "2":
                rs = "未通过";
                break;
        }
        return rs;

    }
}
