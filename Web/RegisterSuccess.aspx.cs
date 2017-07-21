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

public partial class RegisterSuccess : System.Web.UI.Page
{
    ECustomerInfo info = new ECustomerInfo();
    ECustomer bll = new ECustomer();

    protected void Page_Load(object sender, EventArgs e)
    {
        int id = HjNetHelper.GetQueryInt("Dataid", 0);

        if (id ==0)
        {
            //错误处理
            AlertScript.RegScript(Page, "alert('未能获取相应信息。');window.location = 'index.aspx';");
        }
        else
        {
            ECustomerInfo info = bll.GetModel(id);
            if (info == null)
            {
               //错误处理
                AlertScript.RegScript(Page, "alert('未能获取相应信息。');");
            }
            else
            {
                this.LabName.Text = info.Name;
                this.LabPhone.Text = info.Tell;
            }
       
        }

    }
}