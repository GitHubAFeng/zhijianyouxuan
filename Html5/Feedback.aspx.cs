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
using Hangjing.Common;
using Hangjing.Model;


namespace Html5
{
    public partial class Feedback : System.Web.UI.Page
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


        ETogoOpinion dal = new ETogoOpinion();
        int pagesize = 1000;
        int pageindex = 1;
        string pageurl = "Feedback.aspx?a=1";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                pageindex = HjNetHelper.GetQueryInt("PageNo", 1);
                int id = HjNetHelper.GetQueryInt("id", 0);
                if (id > 0)
                {
                    back.HRef = "shopdetail.aspx?id=" + id;
                    this.goOrder.HRef = "ShowTogo.aspx?id="+id;//点餐
                    this.goShop.HRef = "shopdetail.aspx?id=" + id;//商家

                    tbtogoname.InnerHtml = "餐馆评论";
                    SqlWhere = "TogoID =" + id;
                    BindData();
                }
                else
                {
                    if (UserHelp.IsLogin())
                    {
                        SqlWhere = "userid ='" + UserHelp.GetUser().DataID + "'";
                        BindData();

                    }
                    else
                    {
                        Response.Redirect("../Login.aspx");
                    }
                }
                
            }

        }
        public string GetAverage(int ServiceGrade, int FlavorGrade, int SpeedGrade)
        {
            int ress = ((ServiceGrade + FlavorGrade + SpeedGrade) * 100 / 15);
            return ress + "%";
        }
        void BindData()
        {
            int count = dal.GetCount(SqlWhere);
            this.rptCommentlist.DataSource = dal.GetList(pagesize, pageindex, SqlWhere, "PostTime", 1);
            this.rptCommentlist.DataBind();

            int pagecount = (count % pagesize == 0 ? count / pagesize : count / pagesize + 1);
            hfpage.Value = pagecount.ToString();

            if (pagecount > 1)
            {
                pages.InnerHtml = WebUtility.GetPageString(pageindex, pagecount, pageurl);
            }


        }
    }
}