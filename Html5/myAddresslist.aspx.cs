using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

namespace Html5
{
    public partial class myAddresslist : System.Web.UI.Page
    {
        EAddress dal = new EAddress();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                ECustomerInfo user = UserHelp.GetUser();

                if (user == null)
                {
                    Response.Redirect("~/login.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString()));
                }
                else
                {
                    string returnurl = Request["returnurl"];
                    if (returnurl == "" || returnurl==null)
                    {
                        hfret.Value = "1";
                        newadd.HRef = "myAddressdetail.aspx";
                    }
                    else
                    {
                        back.HRef = Server.UrlDecode(returnurl);
                        newadd.HRef = "myAddressdetail.aspx?returnurl=" + returnurl;
                    }
                    BindData();
                }
               
            }
        }

        /// <summary>
        /// 获取链接地址
        /// </summary>
        public string getuseraddress(object id,object bid)
        {
            int tid = HjNetHelper.GetQueryInt("tid", 0);
            if (tid == 0)
            {
                return "myAddressdetail.aspx?id="+id.ToString();
            }
            else
            {
                return "orderdetail.aspx?id=" + tid.ToString() + "&aid=" + bid.ToString();
            }
        }

        public void BindData()
        {
            string SqlWhere = "userid=" + UserHelp.GetUser().DataID;

            IList<EAddressInfo> list = dal.GetList(20, 1, SqlWhere, "pri", 1);
            //this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
            IList<EAddressInfo> listtemp = list;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Pri == 1)
                {

                }
            }
            this.rtpAddressList.DataSource = list;
            this.rtpAddressList.DataBind();
            //if (list.Count == 0)
            //{
            //    this.my_address.Style.Add(HtmlTextWriterStyle.Display, "none");
            //    return;
            //}
        }
    }
}
