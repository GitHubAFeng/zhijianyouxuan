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
    /// <summary>
    /// 订单点评
    /// </summary>
    public partial class Commentshop : System.Web.UI.Page
    {
        ETogoOpinion dal = new ETogoOpinion();
        private int id = HjNetHelper.GetQueryInt("id", 0);

        protected void Page_Load(object sender, EventArgs e)
        {
            ECustomerInfo user = UserHelp.GetUser();
            if (user == null)
            {
                Response.Redirect("login.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString()));
            }

            CustorderInfo model = new Custorder().GetModel(id);
            if (model == null || model.Commentstate == 1)
            {
                Response.Redirect("showtogoorder.aspx?id=" + model.orderid+"&review=1");
            }

            backlink.HRef = "showtogoorder.aspx?id=" + model.orderid;

            if (Request.HttpMethod.ToUpper() == "POST")
            {
                add_Click();
            }

            //获取post信息，提交订单
        }

        /// <summary>
        /// 提交评论
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void add_Click()
        {
            ETogoOpinionInfo info = new ETogoOpinionInfo();
            CustorderInfo model = new Custorder().GetModel(id);
            ECustomerInfo user = UserHelp.GetUser();

            info.Comment = WebUtility.InputText(Request.Form["tbComment"]);
            info.UserID = user.DataID;
            info.TogoID = model.TogoId;
            info.Point = 0;
            info.PostTime = DateTime.Now;
            info.ServiceGrade = Convert.ToInt32(WebUtility.InputText(Request.Form["tbpoint"]));
            info.FlavorGrade = 5;
            info.SpeedGrade = 5;
            info.UserName = user.Name;
            info.Rtime = Convert.ToDateTime("1900-01-01");
            info.Rcontent = "";

            if (new ETogoOpinion().Add(info) > 0)
            {
                string sql = "UPDATE dbo.Custorder SET Commentstate = 1 WHERE Unid = " + id;
                WebUtility.excutesql(sql);


                WebUtility.RegScript(Page, "alert('评论成功'); window.location = 'showtogoorder.aspx?id="+model.orderid+"'");
               

                return;
            }
            else
            {
                WebUtility.RegScript(Page, "alert('评论失败');");
                return;
            }





        }
    }
}
