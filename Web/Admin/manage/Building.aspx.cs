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
using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;

// 标志建筑物
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-07-14

public partial class Admin_Building : System.Web.UI.Page
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

    Hangjing.SQLServerDAL.EBuilding bll = new Hangjing.SQLServerDAL.EBuilding();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            WebUtility.SetDDLCity(DDLArea);
            SqlWhere = "1=1";
            BindData();
        }
    }


    protected void BindData()
    {
        this.AspNetPager1.RecordCount = bll.GetCount(SqlWhere);
        this.rtpTogolist.DataSource = bll.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "DataId", 1);
        this.rtpTogolist.DataBind();
        AlertScript.AjaxRegisterScript("init();", UpdatePanel1);
        CacheHelper.ClearBuildingListList();
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = "1=1";
        if (tbKeyword.Text != "")
        {
            SqlWhere += " and [Name] like '%" + Utils.RegEsc(WebUtility.InputText(this.tbKeyword.Text.Trim())) + "%' ";
        }
        if (DDLArea.SelectedValue != "-1")
        {
            SqlWhere += " and cityid = " + DDLArea.SelectedValue + "";
        }
        if (ddlIsShow.SelectedValue != "-1")
        {
            SqlWhere += " and IsShow = " + ddlIsShow.SelectedValue + "";
        }
        BindData();
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
        if (bll.DelList(IdList)>0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
            BindData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
        }
    }

    protected void rtpTogolist_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string type = e.CommandName;
        switch (type)
        {
            case "del":
                //判断权限
                int _rs = WebUtility.checkOperator(4);
                if (_rs == 0)
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                    return;
                }
                if (bll.Delete(Convert.ToInt32(e.CommandArgument))>0)
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

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }
}
