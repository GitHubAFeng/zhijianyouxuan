using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

namespace Html5
{
    public partial class myChange : System.Web.UI.Page
    {
        private string SqlWhere
        {
            get
            {
                return ViewState["where"] == null ? "" : ViewState["where"].ToString();
            }
            set
            {
                ViewState["where"] = (object)value;
            }
        }
        Integral dal = new Integral();
        protected void Page_Load(object sender, EventArgs e)
        {
            //UserHelp.IsLogin(Request.Url.PathAndQuery);
            ECustomerInfo user = UserHelp.GetUser();
            if (user == null)
            {
                Response.Redirect("~/login.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString()));
            }

            if (!this.Page.IsPostBack)
            {
                SqlWhere = "custid ='" + user.DataID + "'";
                BindData();
            }
        }

        //绑定数据
        private void BindData()
        {
            int pagesize = 10;
            int pageindex = HjNetHelper.GetQueryInt("PageNo", 1);

            string pageurl = "?x=";
            this.rptComment.DataSource = dal.GetList(pagesize, pageindex, SqlWhere, "IntegralId", 1);
            this.rptComment.DataBind();

            //生成页码条
            int count = dal.GetCount(SqlWhere);
            int pagecount = (count % pagesize == 0 ? count / pagesize : count / pagesize + 1);
            if (pagecount > 1)
            {
                pages.InnerHtml = WebUtility.GetPageString(pageindex, pagecount, pageurl);
            }
        }


        //根据状态得到状态HTML
        protected string GetState(object o)
        {
            int state = Convert.ToInt32(o);
            string value = "";
            switch (state)
            {
                case 0: value = "<span class=\"orange\">未审核</span>"; break;
                case 1: value = "<span class=\"green\">审核通过</span>"; break;
                case 2: value = "<span class=\"red\">审核未通过</span>"; break;
            }
            return value;
        }


    }
}