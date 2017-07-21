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
using System.IO;
using org.in2bits.MyXls;

public partial class shop_FoodList : System.Web.UI.Page
{
    protected string otime
    {
        get
        {
            object o = ViewState["otime"];
            return (o == null) ? "" : o.ToString();
        }
        set
        {
            ViewState["otime"] = value;
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

    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);
        if (!this.Page.IsPostBack)
        {
            int tid = UserHelp.GetUser_Togo().Unid;
            SqlWhere = "FPMaster= '" + tid + "' and InUse != 'd'";
            BindData();
            PointsInfo model = new Points().GetModel(UserHelp.GetUser_Togo().Unid);
            otime = model.OpenTime;
            Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/shop/shop" + tid + "/food");
        }
    }

    Foodinfo dal = new Foodinfo();
    protected void BindData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rptFoodList.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "FoodNo", 1);
        this.rptFoodList.DataBind();
        NoRecord();
    }

    private void NoRecord()
    {
        if (rptFoodList.Items.Count == 0)
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "block");
        }
        else
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void rptFoodList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string type = e.CommandName;
        if (type == "del")
        {
            if (dal.UpdateValue("InUse", "d", "where Unid = (" + e.CommandArgument + ")") > 0)
            {
                AlertScript.AjaxRegisterScript("tipsWindown('提示信息','text:操作成功!','250','150','true','2000','true','text');", Page);
            }
            else
            {
                AlertScript.AjaxRegisterScript("tipsWindown('提示信息','text:操作失败!','250','150','true','2000','true','text');", Page);
            }
            BindData();
            return;
        }
        if (type == "update")
        {
            Response.Redirect("FoodDetail.aspx?id=" + Convert.ToInt32(e.CommandArgument));
        }
        if (type == "set")
        {
            FoodinfoInfo model = dal.GetModel(Convert.ToInt32(e.CommandArgument));
            string InUser = model.InUse.Trim();
            if (InUser == "y")//下线
            {
                if (dal.UpdateValue("InUse", "n", "where Unid = (" + e.CommandArgument + ")") > 0)
                {
                    AlertScript.AjaxRegisterScript("tipsWindown('提示信息','text:操作成功1!','250','150','true','2000','true','text');", Page);
                }
                else
                {
                    AlertScript.AjaxRegisterScript("tipsWindown('提示信息','text:操作失败!','250','150','true','2000','true','text');", Page);
                }
                BindData();
            }
            else
            {
                if (dal.UpdateValue("InUse", "y", "where Unid = (" + e.CommandArgument + ")") > 0)
                {
                    AlertScript.AjaxRegisterScript("tipsWindown('提示信息','text:操作成功2!','250','150','true','2000','true','text');", Page);
                    BindData();
                }
                else
                {
                    AlertScript.AjaxRegisterScript("tipsWindown('提示信息','text:操作失败!','250','150','true','2000','true','text');", Page);
                }
            }
        }
    }


    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = "FPMaster= '" + UserHelp.GetUser_Togo().Unid + "' and InUse != 'd'";
        if (tbKeyword.Value != "")
        {
            SqlWhere += " and  FoodName like '%" + WebUtility.InputText(tbKeyword.Value) + "%'";
        }

        BindData();
    }

}
