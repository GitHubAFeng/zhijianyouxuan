using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Text;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

// TogoShoppingCar.cs:点餐购物车执行类.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-03-23

public partial class Ajax_TogoShoppingCart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        //fuc 是执行类型 
        //     类型代码           参数
        //add 加入购物车        uid togoid pid pname pprice pnum
        //mod 修改数量          uid pid pnum
        //del 删除购物车餐品    uid pid
        //list 获取购物车       uid
        //delall 清空购物车     uid
        string type = Request["fuc"];
        int userid = 0;


        {
            switch (type)
            {
                case "mod":
                    if (ModCart(0, Convert.ToInt32(Request["pid"]), Convert.ToInt32(Request["pnum"])))
                    {
                        Response.Write("1");
                    }
                    else
                    {
                        Response.Write("0");
                    }
                    break;
                case "del":
                    if (DeleteCart(0, Convert.ToInt32(Request["pid"])))
                    {
                        Response.Write("1");
                    }
                    else
                    {
                        Response.Write("0");
                    }
                    break;
                case "list":
                    string list = "";
                    list = ListCart(0, Convert.ToInt32(Request["togoid"]));
                    Response.Write(list);
                    break;
                case "delall":
                    if (DeleteAllCart(Convert.ToInt32(Request["uid"])))
                    {
                        Response.Write("1");
                    }
                    else
                    {
                        Response.Write("0");
                    }
                    break;
                case "deladdr":
                    if (DelAddr(Convert.ToInt32(Request["dataid"])))
                    {
                        Response.Write("1");
                    }
                    else
                    {
                        Response.Write("0");
                    }
                    break;
                default:
                    Response.Write("-1");
                    break;
            }
        }
        Response.End();
    }

    /// <summary>
    /// 修改购物车
    /// </summary>
    /// <returns></returns>
    private bool ModCart(int uid, int pid, int pnum)
    {
        //如果已经存在商品 则增加餐品数量即可 更新价格
        Hangjing.SQLServerDAL.ETogoShoppingCart bll = new Hangjing.SQLServerDAL.ETogoShoppingCart();

        if (bll.ModCart(uid, pid, pnum, 0) > 0)
        {
            return true;
        }


        return false;
    }

    /// <summary>
    /// 删除购物车商品
    /// </summary>
    /// <returns></returns>
    private bool DeleteCart(int uid, int pid)
    {
        ECustomerInfo model = UserHelp.GetUser();
        if (new Hangjing.SQLServerDAL.ETogoShoppingCart().DeleteCart(uid, pid) > 0)
        {
            return true;
        }

        return false;

    }

    /// <summary>
    /// 清空购物车中商品
    /// </summary>
    /// <returns></returns>
    public bool DeleteAllCart(int uid)
    {
        string userid = WebUtility.FixgetCookie("uc");
        if (new Hangjing.SQLServerDAL.ETogoShoppingCart().DelAllCartItem(userid) > 0)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 获取购物车
    /// </summary>
    /// <returns></returns>
    private string ListCart(int Uid, int togoid)
    {
        string userid = WebUtility.FixgetCookie("uc");
        string blat = WebUtility.FixgetCookie("mylat");
        string blng = WebUtility.FixgetCookie("mylng");

        Hangjing.SQLServerDAL.ETogoShoppingCart bll = new Hangjing.SQLServerDAL.ETogoShoppingCart();
        IList<Hangjing.Model.ETogoShoppingCart> list = new List<Hangjing.Model.ETogoShoppingCart>();

        list = bll.GetCart(userid);
        list = list.Where(a => a.TogoId == togoid).ToList();//获取该商家的订单信息

        int all_num = 0;//总是数量
        decimal all_price = 0; //总价格
        int sendree = 0;
        decimal packagefee = 0;

        decimal oneshopprice = 0;

        StringBuilder cart_html = new StringBuilder("<table class=\"cart_table\" width=\"100%\">");
        cart_html.Append("<tbody>");
        cart_html.Append("<tr><th>餐品</th> <th width=\"80\">数量</th> <th colspan=\"2\">价格</th></tr>");

        //不同的商家

        foreach (Hangjing.Model.ETogoShoppingCart item in list)
        {
            string tc = "_" + item.TogoId;

            string distance = WebUtility.getDistance(blat, blng, item.Lat, item.Lng);
            //商家名称
            cart_html.Append("<tr class=\"shop_table_title2 access " + tc + "\"><td colspan='5' class='box_header_togo myshop'>" + item.TogoName + "<span class=\"resetMoudle-distance\">" + distance + "公里</span></td></tr>");
            oneshopprice = 0;
            for (int i = 0; i < item.ItemList.Count; i++)
            {
                item.ItemList[i].PPrice += item.ItemList[i].addprice;

                cart_html.Append("<tr><td class=\"padl5\">" + item.ItemList[i].PName + "</td>");
                cart_html.Append("<td><span class=\"bord_icon\" onclick=\"cutnum(" + item.ItemList[i].DataId.ToString() + "," + item.ItemList[i].PNum.ToString() + ",0);\">-</span><span class=\"cartnum\" onblur=\"modcart('" + item.ItemList[i].DataId.ToString() + "',this.value, '" + item.ItemList[i].PNum.ToString() + "') \">" + item.ItemList[i].PNum.ToString() + "</span><span class=\"bord_icon\" onclick=\"addnum(" + item.ItemList[i].DataId.ToString() + "," + item.ItemList[i].PNum.ToString() + ",0);\">+</span></td>");
                cart_html.Append("<td>￥" + (item.ItemList[i].PPrice * item.ItemList[i].PNum).ToString() + "</td>");
                cart_html.Append("<td><a class=\"del_cart\" style=\" cursor:pointer\"  onclick=\"delcart('" + item.ItemList[i].DataId.ToString() + "','" + item.ItemList[i].PId.ToString() + "')\"></a></td></tr>");

                all_num += item.ItemList[i].PNum;
                all_price += item.ItemList[i].PPrice * item.ItemList[i].PNum;
                oneshopprice += item.ItemList[i].PPrice * item.ItemList[i].PNum; ;

                packagefee += item.ItemList[i].owername * item.ItemList[i].PNum;


            }
            if (oneshopprice >= item.ptimes)
            {
                item.sendfree = 0;
            }
            else
            {
                //取消送餐费
                //item.sendfree = new SendFee(blat, blng, item.Lat, item.Lng).carSendMoney();
                item.sendfree = 0;
            }

            //取消送餐费
            //sendree += item.sendfree;
            all_price += item.sendfree;

            all_price += packagefee;

        }
        //打包费
        cart_html.Append("<tr><td align=\"center\" colspan=\"2\">打包费</td> <td align=\"center\" colspan=\"2\"><strong>￥" + packagefee + "</strong></td></tr>");


        //合计
        cart_html.Append("<tr><td align=\"center\" class=\"bobord\" colspan=\"2\">合计</td> <td align=\"center\" class=\"bobord\" colspan=\"2\"><strong id=\"allprice\">￥" + all_price.ToString() + "</strong></td></tr>");

        if (cart_html.ToString() != null || cart_html.ToString() != "")
        {
            return cart_html.ToString();
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 删除地址
    /// </summary>
    /// <returns></returns>
    public bool DelAddr(int dataid)
    {
        if (new Hangjing.SQLServerDAL.EAddress().DelEAddress(dataid) > 0)
        {
            return true;
        }
        return false;
    }
}
