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

public partial class Admin_shop_NoticenewsList :AdminPageBase
{
    Noticenews dal = new Noticenews();
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

    protected void Page_Load(object sender, EventArgs e)
    {
       /// CheckRights("A");
        if (!this.Page.IsPostBack)
        {
            SqlWhere = "1=1";
            if (!string.IsNullOrEmpty(HjNetHelper.GetQueryString("tid")))
            {
                int togoid = HjNetHelper.GetQueryInt("tid", 0);
                SqlWhere += " And inve1 =" + togoid.ToString();
            }
            GetNoticenews();
        }
    }

    protected void GetNoticenews()
    {
        AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
       this.rtpNoticenews.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "DataId", 1);
        this.rtpNoticenews.DataBind();
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init();");
    }

    protected void rtpNoticenews_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            //判断权限
            int _rs = WebUtility.checkOperator(4);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            if (dal.Delnoticenews(Convert.ToInt32(e.CommandArgument)) > 0)
            {

                AlertScript.RegScript(this, UpdatePanel1, "tipsWindown('提示信息','text:操作成功!','250','150','true','2000','true','text');");
                GetNoticenews();
            }
            else
            {
                AlertScript.RegScript(this, UpdatePanel1, "tipsWindown('提示信息','text:操作成功!','250','150','true','2000','true','text');");
            }
        }
    }

    protected void DelList_Click(object sender, EventArgs e)
    {
        string IdList = this.hidDels.Value;
        //判断权限
        int _rs = WebUtility.checkOperator(4);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        if (dal.DelList(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
            GetNoticenews();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
            GetNoticenews();
        }
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = "1=1";
        if (tbtitle.Text.Trim() != "")
        {
            SqlWhere += " And  Title like '%" + WebUtility.InputText(tbtitle.Text) + "%'";
        }
        if (this.tbDate1.Text != "")
        {
            SqlWhere += " And AddDate >= " + "'" + this.tbDate1.Text + "'";
        }
        if (this.tbDate2.Text != "")
        {
            SqlWhere += " And AddDate <= " + "'" + this.tbDate2.Text + "'";
        }
        if (ddlstate.SelectedValue != "-1")
        {
            SqlWhere += " And inve2 ='" + ddlstate.SelectedValue + "'";
        }
        if (!string.IsNullOrEmpty(HjNetHelper.GetQueryString("tid")))
        {
            int togoid = HjNetHelper.GetQueryInt("tid", 0);
            SqlWhere += " And inve1 =" + togoid.ToString();
        }
        GetNoticenews();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetNoticenews();
    }

    protected string getState(object s)
    {
        string rs = "";
        switch (s.ToString())
        {
            case "0":
                rs = "未审核";
                break;
            case "1":
                rs = "审核通过";
                break;
            case "2":
                rs = "未通过";
                break;
        }
        return rs;

    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        Response.Redirect("NoticenewsDetail.aspx?tid=" + HjNetHelper.GetQueryInt("tid", 0));
    }
}
