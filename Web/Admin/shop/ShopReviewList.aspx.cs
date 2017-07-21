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

// 商家列表管理
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com 餐品信息
// 2010-07-12

public partial class Admin_Shop_ShopReviewList : System.Web.UI.Page
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

    ETogoOpinion dal = new ETogoOpinion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindData();
        }
    }

    /// <summary>
    /// 绑定指定的数据
    /// </summary>
    protected void BindData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);

        this.rtpTogolist.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "dataid", 1);
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
        SqlWhere = "1= 1";
        if (this.tbKeyword.Text.Trim() != "")
        {
            SqlWhere += " and  username  like '%" + WebUtility.InputText(this.tbKeyword.Text.Trim()) + "%' ";
        }
        if (this.tbshop.Text.Trim() != "")
        {
            SqlWhere += " and togoid in (select unid from points where  name   like '%" + WebUtility.InputText(this.tbshop.Text.Trim()) + "%') ";
        }
        if (tbstarttime.Text != "")
        {
            SqlWhere += " and  PostTime > '" + WebUtility.InputText(this.tbstarttime.Text) + "' ";
        }
        if (tbendtime.Text != "")
        {
            SqlWhere += " and  PostTime < '" + WebUtility.InputText(this.tbendtime.Text) + "' ";
        }
        BindData();

    }

    protected void rtpTogolist_ItemCommand(object sender, RepeaterCommandEventArgs e)
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
            if (dal.DelETogoOpinion(Convert.ToInt32(e.CommandArgument)) > 0)
            {
                AlertScript.RegScript(this, "tipsWindown('提示信息','text:删除成功!','250','150','true','','true','text');init();");
                BindData();
            }
            else
            {
                AlertScript.RegScript(this, "tipsWindown('提示信息','text:删除失败!','250','150','true','','true','text');init();");
            }
        }
        else
        {
            Response.Redirect("ShopReviewdetail.aspx?id=" + Convert.ToInt32(e.CommandArgument));
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
        int _rs = WebUtility.checkOperator(4);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        string IdList = this.hdDels.Value;
        if (dal.DelList(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
            BindData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
        }
    }

    protected string isSee(string time)
    {
        string rs = "否";
        if (time == "")
        {
            //todo;
        }
        DateTime d = Convert.ToDateTime(time);
        if (d > Convert.ToDateTime("1900-01-01"))
        {
            rs = "是";
        }
        if (d > Convert.ToDateTime("1922-01-01"))
        {
            rs = "是(已回复)";
        }
        return rs;
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
