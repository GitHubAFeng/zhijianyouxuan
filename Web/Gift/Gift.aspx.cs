#region license
/**********************************************
* CopyRight (c) 2009-2010 HangJing Teconology. All Rights Re* Function :
* Created by jijunjian at 2011-4-21 9:20:52.
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

public partial class Gift_Gift1 : System.Web.UI.Page
{
    Gifts bll = new Gifts();
    int userid = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
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
        Hangjing.SQLServerDAL.Gifts bll = new Gifts();
        IList<GiftsInfo> list = new List<GiftsInfo>();
        GiftsClass daltype = new GiftsClass();
        rpttype.DataSource = daltype.GetList(10, 1, "", "ClassOrder", 1);
        rpttype.DataBind();

        rptSortList.DataSource = rpttype.DataSource;
        rptSortList.DataBind();

        //绑定优惠券
        string cardsql = " mtype = 1 and CardCount > 0";
        IList<batshopcardInfo> cardlist = new batshopcard().GetList(10, 1, cardsql, "sortnum", 1);
        foreach (var item in cardlist)
        {
            item.TogoName = "";
            switch (item.Inve1)
            {
                case 1:
                    item.TogoName = "减"+item.point;;
                    break;
                case 2:
                    item.TogoName = ""+item.point+"折";
                    break;
                case 3:
                    item.TogoName = "" + item.point + "倍积分";
                    break;
                default:
                    break;
            }
        }

        rptVoucher.DataSource = cardlist;
        rptVoucher.DataBind();
     
    }

    /// <summary>
    /// 检查用户 已经登入则现实用户名
    /// </summary>
    private bool CheckUser()
    {
        if (UserHelp.IsLogin())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected IList<GiftsInfo> getsub(object i, string index)
    {
        string sql = "Gifts.ClassId = " + i.ToString();
        return bll.GetList(100, 1, sql, "sortnum", 1);
    }
}
