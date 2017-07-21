using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.DBUtility;

/// <summary>
///   crm_tell 用户电话
///   crm_uid   用户编号
///   crm_bid  楼宇
/// </summary>
public partial class qy_54tss_Admin_Service_CRMLeft : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string tel = "";
            string path = Request.Url.PathAndQuery.ToLower();
            if (path.IndexOf("ordercrm.aspx") >= 0)//首页
            {
                tel = ""; ;

                if (HjNetHelper.GetQueryString("tel") != "")
                {
                    tel = HjNetHelper.GetQueryString("tel");
                    WebUtility.FixsetCookie("crm_tell", tel, 1);
                }

                //2用户地址
                if (tel != null && tel != "")
                {
                    ECustomerInfo user = new ECustomer().GetListByTel(tel);
                    if (user != null)
                    {
                        WebUtility.FixsetCookie("crm_uid", user.DataID.ToString(), 1);
                        string addsql = " userid =" + user.DataID;
                        IList<EAddressInfo> addlist = new EAddress().GetList(5, 1, addsql, "pri", 1);
                        if (addlist.Count > 0)
                        {
                            tbuname.Text = addlist[0].Receiver;
                            tbaddress.Text = addlist[0].Address;
                            hfaddress.Value  = addlist[0].Address; ;
                            add_dataid.Value = addlist[0].DataID.ToString();
                            EAddressInfo addmodel = new EAddressInfo();
                            addmodel.Receiver = "新地址";
                            addmodel.Address = "";
                            addlist.Add(addmodel);
                        }
                        WebUtility.BindRepeater(rptaddress, addlist);
                        lbtelmsg.InnerText = "(会员)";
                    }
                    else
                    {
                        lbtelmsg.InnerText = "(非会员)";
                        WebUtility.FixdelCookie("crm_uid");
                    }
                }
            }
            else//其他页面
            {
                tel = WebUtility.FixgetCookie("crm_tell");
                string myaddrid = WebUtility.FixgetCookie("used_addressid");
                if (myaddrid != null && myaddrid != "" && myaddrid != "0")
                {
                    int myid = Convert.ToInt32(myaddrid);
                    EAddressInfo model = new EAddress().GetModel(myid);
                    if (model != null)
                    {
                        tbuname.Text = model.Receiver;
                        tbaddress.Text = model.Address;
                    }
                }
            }
            tbtel.Text = tel;

            //获取数据
            //1.最近订单数据
            if (tel.Trim() != "")
            {
                rptOrderList1.DataSource = new Custorder().GetList(1, 1, "OrderComm like '%" + tel + "%'", "Unid", 1);
                rptOrderList1.DataBind();
            }
        }
    }
}


