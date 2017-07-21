using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class UserHome_delmoneyList : System.Web.UI.Page
{
    UserDelMoneyLog dal = new UserDelMoneyLog();

    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin(Request.Url.PathAndQuery);

        if (!Page.IsPostBack)
        {
            lbMoney.Text = UserHelp.GetUser().Usermoney.ToString("0.0");
            lbUserName.Text = UserHelp.GetUser().Name;

            AspNetPager1.RecordCount = dal.GetCount("UserId=" + UserHelp.GetUser().DataID.ToString() );

            if (AspNetPager1.RecordCount == 0)
            {
                noRecord.Style.Add("display", "block");
            }
            BindData();
        }
    }

    private void BindData()
    {
        rptPointCount.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, "UserId=" + UserHelp.GetUser().DataID.ToString() + " ", "AddDate", 1);
        rptPointCount.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }
}
