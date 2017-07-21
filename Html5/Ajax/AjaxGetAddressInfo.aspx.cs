using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Hangjing.Common;
using System.Text.RegularExpressions;

using Hangjing.SQLServerDAL;

using Hangjing.Model;

public partial class Ajax_AjaxGetAddressInfo : System.Web.UI.Page
{

    EAddress dal = new EAddress();

    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Clear();
        //string name = WebUtility.InputText(Request["name"]);
        string sb = "";
        string addressid = Request["addid"];
        if (addressid!="")
        {
            EAddressInfo model = dal.GetModel(Convert.ToInt32(addressid));
            if (model!=null)
            {
                sb = model.Receiver + "^" + model.Mobilephone + "^" + model.Address;
            }
        }


        Response.Write(sb);
        Response.End();

    }

}
