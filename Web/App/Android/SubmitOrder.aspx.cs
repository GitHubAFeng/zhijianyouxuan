using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Web.Script.Serialization;
using Hangjing.WebCommon;

/// <summary>
/// 提交订单
/// </summary>
public partial class AndroidAPI_SubmitOrderv : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        string ret = "{\"orderid\":\"2011111512000135001\",\"orderstate\":\"1\"}";
        string jsonstring = Request["ordermodel"];
        Custorder dal = new Custorder();

        Hangjing.Common.HJlog.toLog("订单提交数据：" + jsonstring);

        IList<EAddressInfo> orderlist = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<IList<EAddressInfo>>(jsonstring);
        if (orderlist.Count > 0)
        {
            EAddressInfo info = orderlist[0];
            if (info.CustomerName == null)
            {
                info.CustomerName = "";
            }

            if (info.UserID > 0 && new ECustomer().GetCount("dataid = " + info.UserID + "") == 0)
            {
                ret = "{\"orderid\":\"\",\"orderstate\":\"-1\",\"totalprice\":\"0\",\"msg\":\"此用户已经删除或者已经加入黑名单，不能在线下订单\"}";
                Response.Write(ret);
                Response.End();
                return;
            }
            if (info.UserID > 0 && info.isuercard == 1) //使用了券，要判断券号是否正常
            {
                string cardckey = info.shopcardjson;// 网站里这个表示是券的json,这里为了app方便，只传券号，这里转成json，再提交 JJ 
                ShopCard card = new ShopCard();
                ShopCardInfo cardinfo = card.GetModelByCPwd(cardckey);
                string msg = "";
                if (cardinfo != null)
                {
                    if (cardinfo.Inve2 == "0")
                    {
                        msg = "此券未激活，不能使用";
                    }
                    else {
                        if (cardinfo.isused == 1)
                        {
                            msg = "此券已经使用过了";
                        }
                    }
                }
                else
                {
                    msg = "券号错误，请重新输入";
                }

                if (msg != "")
                {
                    ret = "{\"orderid\":\"\",\"orderstate\":\"-1\",\"totalprice\":\"0\",\"msg\":\"" + msg + "\"}";
                    Response.Write(ret);
                    Response.End();
                    return;
                }
                //生成json，兼容提交订单函数。
                StringBuilder shopcardjson = new StringBuilder("{");

                shopcardjson.Append("\"ckey\":\""+cardinfo.ckey+"\",");
                shopcardjson.Append("\"Point\":\"" + cardinfo.Point + "\",");
                shopcardjson.Append("\"ReveInt1\":\"" + cardinfo.ReveInt1 + "\"");

                shopcardjson.Append("}");

                info.shopcardjson = shopcardjson.ToString();
            }
            else
            {
                info.shopcardjson = "";
            }


            info.sendtime = info.GainTime;
            info.Phone = "";
            info.tempcode = "";
            info.kefuid = "";
            info.fromweb = info.Ordersource;


            decimal oneshopprice = 0;//单个商家菜品总价格
            decimal togolPrice = 0;//计算总金额（菜品的小计+配送费）
            IList<webPromotionConfigInfo> shoppromotions = new List<webPromotionConfigInfo>();


            foreach (var shopinfo in info.shoplist)
            {
                shoppromotions = Hangjing.WebCommon.WebHelper.getShopPromotions(shopinfo.TogoId, -1, "");

                //商家配送费和配送距离
                IList<PointsInfo> temptogolist = new Points().GetDistanceListSuper(1, 1, " Points.unid= " + shopinfo.TogoId, "unid", 1, info.ulat, info.ulng, " 1=1 ");
                IList<shopdeliveryInfo> deliveryrecord = new shopdelivery().GetList(" tid = " + shopinfo.TogoId);
                PointsInfo temptogo = temptogolist[0];
                if (temptogo.isonline == 0)//营业时间判断
                {
                    ret = "{\"orderid\":\"\",\"orderstate\":\"-1\",\"totalprice\":\"0\",\"msg\":\"商户正在休息，不能提交订单!\"}";
                    Response.Write(ret);
                    Response.End();
                    return;
                }
                if (temptogo.Inve1 < temptogo.Distance)
                {
                    ret = "{\"orderid\":\"\",\"orderstate\":\"-1\",\"totalprice\":\"0\",\"msg\":\"超过" + temptogo.Name + "配送距离" + temptogo.Inve1 + "公里，点击确定，重新选择商家\"}";
                    Response.Write(ret);
                    Response.End();
                    return;
                }

                foreach (var record in deliveryrecord)
                {
                    if (record.distancestart <= temptogo.Distance && record.distanceend > temptogo.Distance)
                    {
                        temptogo.SendFee = record.sendmoney;
                        temptogo.SendLimit = record.minmoney;
                    }
                }

                shopinfo.oldsendfree = Convert.ToInt32(temptogo.SendFee);
                shopinfo.Togoremark = Convert.ToInt32(temptogo.SendLimit);
                shopinfo.sendfree = Convert.ToInt32(temptogo.SendFee);
                shopinfo.ptimes = temptogo.PTimes;
                shopinfo.latlng = "{'ulat':'" + info.ulat + "','ulng':'" + info.ulng + "','slat':'" + temptogo.Lat + "','slng':'" + temptogo.Lng + "'}";
                shopinfo.TogoName = temptogo.Name;

                oneshopprice = 0;//单个商家的菜品总价
                foreach (ETogoShoppingCartInfo item in shopinfo.ItemList)
                {
                    decimal subtotal = Convert.ToDecimal(item.PPrice) * item.PNum;
                    oneshopprice += subtotal;
                    togolPrice += subtotal;

                    item.TogoName = shopinfo.TogoName;
                    item.Remark = "";
                    item.Funit = "";

                   
                    info.foodprice = oneshopprice;
                    info.senmoney = shopinfo.sendfree;

                }

                togolPrice += shopinfo.sendfree;

                if (shopinfo.oldsendfree == 0 && oneshopprice < shopinfo.Togoremark)
                {
                    ret = "{\"orderid\":\"\",\"orderstate\":\"-1\",\"totalprice\":\"0\",\"msg\":\"低于商户起起送价" + shopinfo.Togoremark + "元\"}";
                    Response.Write(ret);
                    Response.End();
                    return;
                }

            }

            info.Promotions = WebHelper.getOrderPromotions(shoppromotions, info);
            foreach (var item in info.Promotions)
            {
                togolPrice -= item.freeSendFee;
            }




            if (info.UserID > 0)
            {
                ECustomerInfo einfo = new ECustomer().GetModel(info.UserID);
                decimal useroney = einfo.Usermoney;
                if (info.paymode == 3)//3账户余额
                {
                    resultinfo paymentcheck = new UserTool(einfo.DataID).checkPayment(togolPrice, info.PayPassword.Trim());
                    if (paymentcheck.status != 1)
                    {

                        ret = "{\"orderid\":\"\",\"orderstate\":\"-1\",\"totalprice\":\"0\",\"msg\":\""+ paymentcheck.message + "\"}";
                        Response.Write(ret);
                        Response.End();
                        return;
                    }


                    
                }
            }

            IList<ROrderinfo> returnlist = new OrderManager().submitOrder(info.shoplist, info, Context);
            if (returnlist != null)
            {
              
                string noticemsg = "订单总金额：" + returnlist[0].Currentprice.ToString("#0.00") + "元.";
                ret = "{\"orderid\":\"" + returnlist[0].Orderid + "\",\"orderstate\":\"1\",\"totalprice\":\"" + returnlist[0].Currentprice + "\",\"msg\":\"" + noticemsg + "\"}";//
            }
            else
            {
                ret = "{\"orderid\":\"\",\"orderstate\":\"-1\",\"totalprice\":\"0\",\"msg\":\"订单提交失败\"}";
            }
        }
        else
        {
            ret = "{\"orderid\":\"\",\"orderstate\":\"-1\",\"totalprice\":\"0\",\"msg\":\"订单提交失败\"}";
        }

        Response.Write(ret);
        Response.End();
    }
}

