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
    public partial class remarkdetail : System.Web.UI.Page
    {
        public string remark = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WebUtility.BindRepeater(rptfastremark, SectionProxyData.GetSTemplateList());
                remark = Server.UrlDecode(Server.UrlDecode(WebUtility.FixgetCookie("mark")));
                //获取post信息，提交订单
                if (Request.HttpMethod.ToUpper() == "POST")
                {
                    add_Click();
                }
            }
        }
        /// <summary>
        /// 添加备注
        /// </summary>
        protected void add_Click()
        {
            string id = HjNetHelper.GetQueryString("id");
            string remark = WebUtility.InputText(Request.Form["tbremark"]);

            if (remark != "" && remark != "请输入备注信息，如：不要辣等")
            {
                WebUtility.FixsetCookie("mark", Server.UrlEncode(remark), 30);
            }
            Response.Redirect("orderdetail.aspx?id=" + id);

        }
    }
}