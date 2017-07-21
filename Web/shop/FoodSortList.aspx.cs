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

public partial class shop_FoodSortList : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);
            SqlWhere = "TogoNUm= '" + UserHelp.GetUser_Togo().Unid + "'";
            BindData();
        }
    }

    EFoodSort dal = new EFoodSort();

    void BindData()
    {
        this.rptFoodSortList.DataSource = dal.GetListByTogoNum(UserHelp.GetUser_Togo().Unid);
        this.rptFoodSortList.DataBind();

        NoRecord();
    }

    private void NoRecord()
    {
        if (rptFoodSortList.Items.Count == 0)
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

    protected void rptFoodSortList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            if (dal.Delete(Convert.ToInt32(e.CommandArgument))>0)
            {
                AlertScript.RegScript(this, "tipsWindown('提示信息','text:删除成功!','250','150','true','','true','text');");
                BindData();
            }
            else
            {
                AlertScript.RegScript(this, "tipsWindown('提示信息','text:删除失败!','250','150','true','','true','text');");
            }
        }
        else
        {
            Response.Redirect("FoodSortDetail.aspx?sortid=" + e.CommandArgument.ToString() + "");
        }
    }
}
