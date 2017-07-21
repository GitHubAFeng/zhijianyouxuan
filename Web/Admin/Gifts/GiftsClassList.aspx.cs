// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// Function :礼品分类管理
// Created by tuhui at 2011-5-24 16:28:34.
// E-Mail: tuhui@ihangjing.com
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

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class Admin_GiftsClass_GiftsClassList :AdminPageBase
{
    GiftsClass dal = new GiftsClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckRights("E");
        if (!IsPostBack)
        {
            BindData();
        }
        
    }

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

    private void BindData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rtpGiftclass.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "ClassOrder", 1);
        this.rtpGiftclass.DataBind();
    }

    protected void rtpGiftclass_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Del":
                {
                    //判断权限
                    int _rs = WebUtility.checkOperator(4);
                    if (_rs == 0)
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                        return;
                    }
                    if (dal.DelGiftsClass(Convert.ToInt32(e.CommandArgument)) > 0)
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除成功','success','true',5);init();");
                        BindData();
                    }
                    else
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除失败!','error','true',5);init();");
                    }
                }
                break;
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

    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        this.AspNetPager1.CurrentPageIndex = e.NewPageIndex;
        BindData();
    }
}

