#region license
/**********************************************
* CopyRight (c) 2009-2010 HangJing Teconology. All Rights Re* Function :
* Created by jijunjian at 2011-4-22 9:36:09.
* E-Mail: jijunjian@ihangjing.com
*********************************************** */
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

/// <summary>
/// 积分兑换优惠券
/// </summary>
public partial class Gift_ShowCard : System.Web.UI.Page
{
    batshopcard dal = new batshopcard();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int Id = HjNetHelper.GetQueryInt("id", 0);
            batshopcardInfo info = dal.GetModel(Id);

            imgLogo.ImageUrl = WebUtility.ShowPic(info.Inve2);
            lbGiftName.Text = info.title;
            lbPoint.Text = info.mydiscount.ToString();
            lbHave.Text = info.CardCount.ToString();
            litIntroduce.Text = info.Contents;
           

            info.TogoName = "";
            switch (info.Inve1)
            {
                case 1:
                    info.TogoName = "减" + info.point; ;
                    break;
                case 2:
                    info.TogoName = "" + info.point + "折";
                    break;
                case 3:
                    info.TogoName = "" + info.point + "倍积分";
                    break;
                default:
                    break;
            }
            lbmoney.Text = info.TogoName;

            ECustomerInfo user = UserHelp.GetUser();
            if (user != null)
            {
                mypoints.InnerHtml = "<span class=\"f10 padlr10 \">>></span>当前可用积分：" + user.Point;
            }

            Page.Title = info.title + " - "+WebUtility.GetWebName();
        }
    }

    protected void GetGift_Click(object sender, EventArgs e)
    {
        //判断是否登入
        UserHelp.IsLogin(Request.Url.PathAndQuery);
        int Id = HjNetHelper.GetQueryInt("id", 0);
        batshopcardInfo info = dal.GetModel(Id);

        //1.判断积分是否足够进行兑换 积分需要从数据库中获取不能从cookie或者session中获取以免出现错误
        ECustomer cbll = new ECustomer();
        ECustomerInfo user = new ECustomerInfo();
        user = cbll.GetModel(UserHelp.GetUser().DataID);
        if (user.Point < info.mydiscount)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert(\"积分不足，不能兑换\");hideload_super();");
            return;
        }
        if (info.CardCount == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert(\"此券库存不足，不能兑换\");hideload_super();");
            return;
        }

        //2.进行兑换 (减库存，减积分，生成券记录)
        int rs = dal.ExchangeVoucher(user.DataID, info.DataID, info.mydiscount);
        switch (rs)
        {
            case -1:
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert(\"积分不足，不能兑换\");hideload_super();");
                break;
            case -2:
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert(\"此券库存不足，不能兑换\");hideload_super();");
                break;
            case 0:
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert(\"兑换成功，提交订单时可以使用优惠券抵部分金额了，点击确定，查看我的优惠券\");gourl('/user/myshopcard.aspx');");

                break;
            default:
                break;
        }

    }

}
