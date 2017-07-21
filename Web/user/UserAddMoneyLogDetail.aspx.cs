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

public partial class UserHome_UserAddMoneyLogDetail : System.Web.UI.Page
{
    UserAddMoneyLog bll = new UserAddMoneyLog();
    UserAddMoneyLogInfo info = new UserAddMoneyLogInfo();
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
            info = new UserAddMoneyLogInfo();
            info = bll.GetModel(id);
            this.lbUserId.Text = Convert.ToString(info.UserId);
            this.lbAddMoney.Text = Convert.ToString(info.AddMoney);
            if (info.State == 0)
            {
                this.lbState.Text = "充值失败";
            }
            else 
            {
                this.lbState.Text = "充值成功";
            }
            //this.lbState.Text =Convert.ToString(info.State);
            if (info.PayType == 0)
            {
                this.lbPayType.Text = "支付宝";
            }
            else 
            {
                this.lbPayType.Text = "银行卡";
            }
            
            //this.lbPayType.Text = Convert.ToString(info.PayType);

            this.lbPayDate.Text = Convert.ToString(info.PayDate);
            if (info.PayState == 0)
            {
                this.lbPayState.Text = "支付失败";
            }
            else
            {
                this.lbPayState.Text = "支付成功";
            }
            //this.lbPayState.Text = Convert.ToString(info.PayState);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("../user/UserAddMoneyLog.aspx");
    }
}
