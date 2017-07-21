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

public partial class UserHome_Left : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Hangjing.Model.ECustomerInfo model = new Hangjing.Model.ECustomerInfo();
            model = UserHelp.GetUser();
            model = new ECustomer().GetModelByNameAPassword(model.Name, model.Password);
            UserHelp.SetLogin(model);
            if (model.Picture != "")
            {
                imghead.Src = WebUtility.ShowPic(model.Picture);
            }
            else
            {
                imghead.Src = "../images/nopic_02.jpg";
            }
            this.litName.Text = model.Name;
            int score = (int)model.Point;
            this.LitPoint.Text = score.ToString();
            LitUserMoney.Text = model.Usermoney + "元";
            litgroupid.Text = model.GroupID.ToString() + "元";
        }
    }
}
