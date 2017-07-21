using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using HJPrinter;
using Hangjing.Common;

public partial class UpdateOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string CustID = Request.QueryString["id"];
        string OrdID = Request.QueryString["orderid"];
        string State = Request.QueryString["state"];
        string up = Request.QueryString["up"];
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(OrdID) && !string.IsNullOrEmpty(State))
        {//更新订单
            if (State == "1")
            {
                if (!string.IsNullOrEmpty(up))
                {
                    HJPrinters.UpdateIsUpdate(CustID, 0);
                }
                string trueorderid = HJEncryption.Decryption(OrdID, HJPrinters.GetPasskeyWithCustID(CustID));
                if (HJPrinters.SetOSIDStata(trueorderid))
                {
                    sb.Append("<success></success>");
                }
                else
                {
                    sb.Append("<error></error>");
                }
            }
            else
            {
                sb.Append("<error></error>");
            }
        }
        else
        {
            sb.Append("<error></error>");
        }
        Response.Write(sb.ToString());
        Response.End();

    }
}
