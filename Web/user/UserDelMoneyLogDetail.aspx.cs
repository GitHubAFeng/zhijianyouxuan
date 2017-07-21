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
using Hangjing.Model;
using Hangjing.Common;

public partial class UserHome_UserDelMoneyLogDetail : System.Web.UI.Page
{
    UserDelMoneyLog dal = new UserDelMoneyLog();
    UserDelMoneyLogInfo info = new UserDelMoneyLogInfo();
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
        UserHelp.IsLogin(Request.Url.PathAndQuery);
        if (!this.Page.IsPostBack)
        {
            if (UserHelp.IsLogin())
            {
                SqlWhere = "UserId=" + UserHelp.GetUser().DataID.ToString() + "";
                BindData();
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }
    }

    private void BindData()
    {
        if (HjNetHelper.GetQueryString("id") != "")
        {
            int id = Convert.ToInt32(HjNetHelper.GetQueryInt("id", 0));
            info = new UserDelMoneyLogInfo();
            info = dal.GetModel(id);
            this.lbUserId.Text = Convert.ToString(info.UserId);
            //this.lbTogoName.Text =Convert.ToString( info.TogoName);
            this.lbDelMoney.Text = Convert.ToString(info.DelMoney);
            this.lbBuyItem.Text = Convert.ToString(info.BuyItem);
            this.lbNewAdddate.Text = Convert.ToString(info.AddDate);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("../user/UserDelMoneyLog.aspx");

    }
}
