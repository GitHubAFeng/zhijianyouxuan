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

// 商家列表管理
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com 餐品信息
// 2010-07-12
public partial class AreaAdmin_Shop_ShopList : AdminPageBase
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

    Points bll = new Points();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //WebUtility.SetDDLCity(DDLArea);
            SqlWhere = "1=1 and InUse = 'Y'";
            SqlWhere = WebUtility.GetSql(SqlWhere);
            
            BindData();
        }
    }

    /// <summary>
    /// 绑定指定的数据
    /// </summary>
    protected void BindData()
    {       
        this.AspNetPager1.RecordCount = bll.GetCount(SqlWhere);
        this.rtpTogolist.DataSource = bll.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "InTime", 1);
        this.rtpTogolist.DataBind();

        AlertScript.AjaxRegisterScript("init();", UpdatePanel1);

    }

    /// <summary>
    /// 搜索
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = "1=1 and InUse = 'Y'";
        SqlWhere = WebUtility.GetSql(SqlWhere);
        if (this.tb_TogoName.Text.Trim() != "")
        {
            SqlWhere += " AND  Name LIKE N'%" + WebUtility.InputText(this.tb_TogoName.Text.Trim()) + "%' ";
        }
        if (this.tb_Tel.Text.Trim() != "")
        {
            SqlWhere += " AND Comm LIKE '%" + WebUtility.InputText(this.tb_Tel.Text.Trim()) + "%' ";
        }
        if (this.tb_Address.Text.Trim() != "")
        {
            SqlWhere += " AND Address LIKE '%" + WebUtility.InputText(this.tb_Address.Text.Trim()) + "%' ";
        }
        if (this.tb_Start.Text != "")
        {
            SqlWhere += " AND InTime >= '" + this.tb_Start.Text + "' ";
        }
        if (this.tb_End.Text != "")
        {
            SqlWhere += " AND InTime <= '" + this.tb_End.Text + "' ";
        }
        //if (DDLArea.SelectedValue != "-1")
        //{
        //    SqlWhere += " and cityid = " + DDLArea.SelectedValue + "";
        //}
        if (this.ddlstar.SelectedValue != "-1")
        {
            SqlWhere += " AND Star = " + this.ddlstar.SelectedValue + " ";
        }
        BindData();
    }

    protected void rtpTogolist_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {

    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DelList_Click(object sender, EventArgs e)
    {
        string IdList = this.hdDels.Value;
        string[] strtemp = IdList.Split(',');
        foreach (var item in strtemp)
        {
            if (item.ToString().Trim() == "1")
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:超市不能删除!','250','150','true','3000','true','text');init();");
                return;
            }
        }

        //判断权限
        int _rs = WebUtility.AreaAdmin_checkOperator(4);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        if (bll.DelList(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
            BindData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
        }
    }

    protected void lbHiddenList_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.AreaAdmin_checkOperator(5);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        string IdList = this.hdDels.Value;
        if (bll.UpdateValue("IsDelete", 1, " where unid in(" + IdList + ")") > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:批量隐藏成功!','250','150','true','1000','true','text');init();");
            BindData();
            Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/shop/");
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:批量隐藏失败!','250','150','true','1000','true','text');init();");
        }
    }

    protected void lbShowList_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.AreaAdmin_checkOperator(6);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        string IdList = this.hdDels.Value;
        if (bll.UpdateValue("IsDelete", 0, " where unid in(" + IdList + ")") > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:批量显示成功!','250','150','true','1000','true','text');init();");
            BindData();
            Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/shop/");
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:批量显示失败!','250','150','true','1000','true','text');init();");
        }
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

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }

    /// <summary>
    /// 营业当前记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void onlineall(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(5);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }

        string sql = "UPDATE dbo.Points SET Status = 1 WHERE " + SqlWhere;
        WebUtility.excutesql(sql);
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:已经全部营业!','250','150','true','1000','true','text');init();");
        BindData();

    }

    /// <summary>
    /// 全部打烊当前记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void offlineall(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(5);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }

        string sql = "UPDATE dbo.Points SET Status = 0 WHERE " + SqlWhere;
        WebUtility.excutesql(sql);
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:已经全部打烊!','250','150','true','1000','true','text');init();");
        BindData();

    }

}
