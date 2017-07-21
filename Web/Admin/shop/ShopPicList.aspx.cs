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

public partial class Admin_ShopPicList : System.Web.UI.Page
{
    ShopSurroundings bll = new ShopSurroundings();
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
            int shopid= HjNetHelper.GetQueryInt("tid", 0);
            SqlWhere = " 1= 1 and Shopid="+shopid;
            BindData();
        }
    }

    protected void BindData()
    {
        this.rtpTogolist.DataSource = bll.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "sort", 1);
        this.rtpTogolist.DataBind();

        AlertScript.AjaxRegisterScript("init();", UpdatePanel1);
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
        if (bll.DeleteList(IdList) > 0)
        {
            //SectionProxyData.ClearPPTList();
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除成功','success','true',5);init();");
            BindData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除失败!','error','true',5);init();");
            BindData();
        }
    }

    /// <summary>
    /// 搜索
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSearch_Click(object sender, EventArgs e)
    {
        int shopid = HjNetHelper.GetQueryInt("tid", 0);
        SqlWhere = " 1= 1 and Shopid="+shopid;
        if (this.tbKeyword.Text.Trim() != "")
        {
            SqlWhere += " AND title LIKE '%" + WebUtility.InputText(this.tbKeyword.Text.Trim()) + "%' ";
        }
        HJlog.toLog("SqlWhere" + SqlWhere);
        BindData();
    }
}
