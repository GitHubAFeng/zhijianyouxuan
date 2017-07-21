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

public partial class Gift_ShowGift1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int Id = HjNetHelper.GetQueryInt("id", 0);
            Gifts bll = new Gifts();

            GiftsInfo info = new GiftsInfo();
            info = bll.GetModel(Id);

            imgLogo.ImageUrl = WebUtility.ShowPic(info.Picture);
            lbGiftName.Text = info.Gname;
            lbPoint.Text = info.NeedIntegral.ToString();
            lbHave.Text = info.Stocks.ToString(); ; //库存暂未实现
            litIntroduce.Text = info.Content;
            Label1.Text ="￥"+info.GiftsPrice.ToString("#0.0");

            Page.Title = info.Gname + " - 积分商城 - " + WebUtility.GetWebName();

            InitUser();

        }
    }

    protected void GetGift_Click(object sender, ImageClickEventArgs e)
    {
        //进行兑换
        Response.Redirect("GetGift.aspx?id=" + HjNetHelper.GetQueryInt("id", 0).ToString() + "");
    }

    /// <summary>
    /// 需要登入的才能执行的操作则进行判断是否已经登入
    /// </summary>
    /// <returns></returns>
    private void InitUser()
    {
        ECustomerInfo model = UserHelp.GetUser();
        if (model != null)
        {
            AlertScript.RegScript(Page, UpdatePanel1, "inithead(" + model.Point + ", '" + model.Name + "', '" + WebUtility.ShowPic(model.Picture) + "');");
        }
        else
        {

        }
    }

    protected void go_buy(object sender, EventArgs e)
    {
        Response.Redirect("GetGift.aspx?id="+HjNetHelper.GetQueryInt("id" , 0)+"&buy=0");
    }

    protected void go_change(object sender, EventArgs e)
    {
        Response.Redirect("GetGift.aspx?id=" + HjNetHelper.GetQueryInt("id", 0) + "&buy=1");
    }
}
