// InitionPrinter.aspx.cs :打印机初始化页面
// CopyRight (c) 2010 HangJing Teconology. All Rights Reserved.
// wlf@ihangjing.com
// 2009-03-25
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HJPrinter;

public partial class InitionPrinter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sn1 = Request.QueryString["sn1"];
        string sn2 = Request.QueryString["sn2"];
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(sn1) && !string.IsNullOrEmpty(sn2))
        {
            HJCustomer customer = HJPrinters.GetCustID(sn1);
            if (customer == null)
            {
                sb.Append("<none></none>");
            }
            else
            {
                sb.Append("<custid>");
                sb.Append(customer.CustID);
                sb.Append("</custid>");

                sb.Append("<phead>");
                sb.Append(customer.CustName);
                sb.Append("</phead>");

                sb.Append("<ptimes>");
                sb.Append(customer.PrintTimes.ToString());
                sb.Append("</ptimes>");

                sb.Append("<pend>");
                sb.Append(customer.PrintEnd);
                sb.Append("</pend>");
            }
        }
        else
        {//返回错误
            sb.Append("<error></error>");
        }
        Response.Write(sb.ToString());
        Response.End();
    }
}
