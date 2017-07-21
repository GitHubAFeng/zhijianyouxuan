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

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;

public partial class Banner : System.Web.UI.UserControl
{
    /// <summary>
    /// 商家编号
    /// </summary>
    private int togoId
    {
        get
        {
            object o = ViewState["TogoId"];
            return Convert.ToInt32(o);
        }
        set
        {
            ViewState["TogoId"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ECustomerInfo user = UserHelp.GetUser();
        if (user != null)
        {
            string sql = "UserID = " + user.DataID;
            divLoinged.Style["display"] = "";
            divUnlogin.Style["display"] = "none";
            lbusername.InnerText = user.Name;
            lbusername.HRef = WebUtility.GetUrl("~/user/MyInfo.aspx");

        }
        else
        {
            divLoinged.Style["display"] = "none";
            divUnlogin.Style["display"] = "";
            orderlink.HRef = "/myorder.aspx";
        }

        string tempcode = WebUtility.FixgetCookie("uc");

        Hangjing.SQLServerDAL.ETogoShoppingCart bll = new Hangjing.SQLServerDAL.ETogoShoppingCart();
        string sqlwhere = String.Format("tempcode='{0}'", tempcode);
        int count = bll.GetCount(sqlwhere);
        countshop.InnerHtml = count.ToString();

    }
}





