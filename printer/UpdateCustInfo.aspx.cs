using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using HJPrinter;

/// <summary>
/// 打印机通过该页面更新信息到服务器上
/// </summary>
public partial class UpdateCustInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string CustID = Request.QueryString["id"];
        string SN2 = Request.QueryString["sn"];
        string Name = Request.QueryString["phead"];
        string PrintTimes = Request.QueryString["ptimes"];
        string PrintEnd = Request.QueryString["pend"];
        //string Phone = Request.QueryString["phone"];
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(CustID) && !string.IsNullOrEmpty(SN2))
        {
            if (CustID.CompareTo(HJPrinters.GetCustID(HJEncryption.Decryption(SN2, HJPrinters.GetPasskeyWithCustID(CustID))).CustID) == 0)
            {//判断，通过SN2获取的商家ID是否和传入的商家编号一致，防止别人通过非打印机方式修改商家信息
                HJCustomer customer = new HJCustomer();
                customer.CustID = CustID;
                customer.CustName = Name;
                customer.PrintTimes = int.Parse(PrintTimes);
                customer.PrintEnd = PrintEnd;
                //customer.CustPhone = Phone;
                if (HJPrinters.SetCustomerInfoWithCustID(customer))
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
