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
    public partial class gift_list : System.Web.UI.Page
    {
        Gifts bll = new Gifts();
        protected void Page_Load(object sender, EventArgs e)
        {
            //UserHelp.IsLogin(Server.UrlEncode(Request.Url.PathAndQuery));
            ECustomerInfo userInfo = UserHelp.GetUser();
            if (userInfo == null)
            {
                Response.Redirect("~/login.aspx?returnurl=" + Server.UrlEncode(Request.RawUrl));
            }

            if (!Page.IsPostBack)
            {
                GetGiftByType();
            }
        }


        /// <summary>
        /// 根据参数获取指定类型的产品
        /// </summary>
        /// <returns></returns>
        private void GetGiftByType()
        {
            //绑定优惠券
            string cardsql = " mtype = 1 and CardCount > 0";//为现金券 并且 数量大于0
            IList<batshopcardInfo> cardlist = new batshopcard().GetList(10, 1, cardsql, "sortnum", 1);
            rptVoucher.DataSource = cardlist;
            rptVoucher.DataBind();

            //绑定其他礼品
            IList<GiftsInfo> list = new List<GiftsInfo>();
            rptGiftList.DataSource = bll.GetList(100, 1, " 1=1 ", "sortnum", 1);
            rptGiftList.DataBind();

        }


    }
}