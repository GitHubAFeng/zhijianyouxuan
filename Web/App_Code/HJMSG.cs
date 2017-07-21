using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

using Hangjing.SQLServerDAL;
using Hangjing.Model;

/// <summary>
///HJMSG 的摘要说明
/// </summary>
public class HJMSG
{
    protected static int _minutes = 2;
  
    /// <summary>
    /// 注册后的订单提交后发短信
    /// </summary>
    /// <param name="phone">电话</param>
    /// <param name="content">内容</param>
    /// <returns></returns>
    public static void SendorderTogo(IList<Hangjing.Model.ROrderinfo> mylist, IList<Hangjing.Model.ETogoShoppingCart> Foods, Hangjing.Model.EAddressInfo infos)
    {
        Custorder dal = new Custorder();

        string content = "";
        string foods = "";
        decimal p = 0;
        string order="";
        for (int i = 0; i < mylist.Count; i++)
        {

            p += mylist[i].Currentprice;
          
            order=mylist[i].Orderid;
        }
        string phone = infos.Phone;
        int TogoID=0;
        //循環找出該商家的菜單
        for (int j = 0; j < Foods.Count; j++)
        {
            if (Foods[j].TogoId > 0)
            {
                for (int t = 0; t < Foods[j].ItemList.Count; t++)
                {

                    foods += Foods[j].ItemList[t].PName + "" + Foods[j].ItemList[t].PNum + "份 " + Foods[j].ItemList[t].Remark + "，";
                }
                TogoID =Foods[j].TogoId;
                foods = foods.Substring(0,foods.Length-1);
            }
        }

        Points dalt = new Points();

        PointsInfo Togoinfo = dalt.GetModel(TogoID);

        string Tell = Togoinfo.Comm;//商家电话

        //商家：您好，【变量】，您的订单【变量】已经提交成功【变量】
        //用户：订单号：【变量】【变量】
        content = "订单号："+mylist[0].Orderid+"," + infos.Receiver + "用户在您这" + DateTime.Now.ToShortTimeString() + "订购了" + foods + ",共计" + p.ToString("0.0") + "元，电话" + infos.Phone + "，送餐地址" + infos.Address + "。";

        string usercontent = "您好，" + infos.Receiver + "，您的订单" + mylist[0].Orderid + "已经提交成功,共计" + p.ToString("0.0") + "元";

        if (Tell != "")
        {
            int i = SendMsg.send(Tell, content);

            i = SendMsg.send(infos.Mobilephone, usercontent);
        }
    }


    public static void SendOrderTogoByOrderId(string orderid)
    {
        Custorder dal = new Custorder();

        string content = "";
        string foods = "";
        decimal p = 0; ;

        CustorderInfo order = new CustorderInfo();

        order = dal.GetModel(orderid);

        int TogoID = order.TogoId;

        IList<FoodlistInfo> foodlist = new List<FoodlistInfo>();
        foodlist = new Foodlist().GetAllByOrderID(orderid);

        for (int j = 0; j < foodlist.Count; j++)
        {

            foods += foodlist[j].FoodName + "" + foodlist[j].FCounts + "份 " + foodlist[j].Remark + "，";
            foods = foods.Substring(0, foods.Length - 1);
        }


        Points dalt = new Points();
        PointsInfo Togoinfo = dalt.GetModel(TogoID);
        string Tell = Togoinfo.Comm;//商家电话

        content = "订单号：" + orderid + "," + order.OrderRcver + "用户在您这" + DateTime.Now.ToShortTimeString() + "订购了" + foods + ",共计" + p.ToString("0.0") + "元，电话" + order.OrderComm + "，送餐地址" + order.AddressText + "。";

        ////商家：您好，【变量】，您的订单【变量】已经提交成功【变量】
        ////用户：订单号：【变量】【变量】
        string usercontent = "您好，" + order.OrderRcver + "，您的订单" + orderid + "已经提交成功,共计" + order.OrderSums.ToString("0.0") + "元";

        if (Tell != "")
        {
            int i = SendMsg.send(Tell, content);

            i = SendMsg.send(order.OrderComm, usercontent);
        }
    }
}
