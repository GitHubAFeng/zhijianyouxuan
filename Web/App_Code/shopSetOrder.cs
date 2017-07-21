using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

/// <summary>
/// 商家处理订单类
/// </summary>
public class shopSetOrder
{
    HttpContext context;
    int shopid = 0;
    int ishopset = 0;
    string orderid = "";
    string resign = "";//拒绝，取消理由


    /// <summary>
    /// 商家处理订单
    /// </summary>
    /// <param name="_context"></param>
    /// <param name="shopid">可以表示商家编号、骑士编号、群组编号</param>
    public shopSetOrder(HttpContext _context, int _shopid, string _orderid, int _ishopset, string _resign)
    {
        context = _context;
        shopid = _shopid;
        orderid = _orderid;
        ishopset = _ishopset;
        resign = _resign;
    }

    /// <summary>
    /// 处理订单,返回0表示这个已经处理了过了，1表示处理成功
    /// </summary>
    public int HandleOrder()
    {
        int rs = 1;
        hurryorder hal = new hurryorder();
        Custorder dal = new Custorder();
        CustorderInfo model = dal.GetModel(orderid);
        NoticeHelper notice = new NoticeHelper(context, shopid.ToString());


        string sql = "";
        switch (ishopset)
        {
            case 0:
                int back = hal.UpdateValue("ReveInt", 1, " where oid = " + model.orderid);
                break;
            case 1:
                if (model.OrderStatus == 5)
                {
                    rs = -1;
                }
                else
                {

                    dal.AddOrderRecord(model.orderid, model.OrderStatus, "商家", "商家接收订单:订单详情");
                    sql = " UPDATE dbo.Custorder SET  ReveDate1 = getdate(),IsShopSet=1  where unid = " + model.Unid;
                    WebUtility.excutesql(sql);

                    new OrderStep().Add(new OrderStepInfo()
                    {
                        stepcode = 20,
                        title = "商家已经接单",
                        subtitle = "",
                        addtime = DateTime.Now,
                        orderid = orderid,
                        revevar1 = "",
                        revevar2 = "0",
                        revevar3 = "0"

                    });

                    //自动调度订单 通知骑士
                    if (model.ReveVar1 == "0")
                    {
                        IList<ROrderinfo> my_list = new List<ROrderinfo>();
                        ROrderinfo mm = new ROrderinfo();
                        mm.latlng = model.ReveVar2;
                        mm.Orderid = model.orderid;
                        mm.cityid = model.cityid;
                        mm.sentorg = model.ReveVar1;
                        mm.paystate = 1;
                        mm.paymode = 1;
                        my_list.Add(mm);
                        dal.AutoDispatch(my_list);
                    }
                }
                notice.send2ShopByShopid();
                break;
            case 2:
                //if (model.deliverid > 0 )
                //{
                //    rs = 0;
                //    return rs;
                //}

                dal.AddOrderRecord(model.orderid, 4, "商家", "商家拒绝订单:订单详情");
                sql = " UPDATE dbo.Custorder SET  ReveDate1 = getdate(),IsShopSet=2,OrderStatus=4,OrderAddrEx ='" + resign + "' where unid = " + model.Unid;
                WebUtility.excutesql(sql);
                dal.OrderCancelRefund(model.orderid); //取消，退款

                new OrderStep().Add(new OrderStepInfo()
                {
                    stepcode = 80,
                    title = "商家拒绝订单",
                    subtitle = "",
                    addtime = DateTime.Now,
                    orderid = orderid,
                    revevar1 = "",
                    revevar2 = "0",
                    revevar3 = "0"

                });
                notice.send2ShopByShopid();
                break;
        }
        return rs;

    }

    /// <summary>
    /// 商家处理取消订单申请
    /// </summary>
    public int CancelOrder()
    {
        int rs = 1;
        Custorder dal = new Custorder();
        CustorderInfo model = dal.GetModel(orderid);
        if (model.OrderStatus != 8)
        {
            rs = -1;
        }
        else
        {
            string sql = "";
            switch (ishopset)
            {
                case 1:
                    dal.AddOrderRecord(model.orderid, model.OrderStatus, "商家", "商家同意订单取消申请");
                    sql = " UPDATE dbo.Custorder SET shopCancel=1  where unid = " + model.Unid;
                    WebUtility.excutesql(sql);

                    new OrderStep().Add(new OrderStepInfo()
                    {
                        stepcode = 105,
                        title = "商家同意订单取消申请",
                        subtitle = "",
                        addtime = DateTime.Now,
                        orderid = orderid,
                        revevar1 = "",
                        revevar2 = "0",
                        revevar3 = "0"
                    });
                    break;
                case 2:
                    dal.AddOrderRecord(model.orderid, model.OrderStatus, "商家", "商家拒绝订单取消申请");

                    sql = " UPDATE dbo.Custorder SET shopCancel=2,Cancelreason='" + resign + "' where unid = " + model.Unid;
                    WebUtility.excutesql(sql);

                    new OrderStep().Add(new OrderStepInfo()
                    {
                        stepcode = 108,
                        title = "商家拒绝订单取消申请",
                        subtitle = resign,
                        addtime = DateTime.Now,
                        orderid = orderid,
                        revevar1 = "",
                        revevar2 = "0",
                        revevar3 = "0"

                    });

                    break;
            }

            //NoticeHelper notice = new NoticeHelper(context, shopid.ToString());
            //notice.send2ShopByShopid();
        }
        return rs;

    }

    /// <summary>
    /// 完成订单
    /// </summary>
    /// <returns></returns>
    public int CompleteOrder()
    {
        int rs = 0;
        Custorder dal = new Custorder();
        CustorderInfo model = dal.GetModel(orderid);

        dal.AddOrderRecord(model.orderid, 3, "商家", "订单详细界面");
        if (dal.UpdataState(model.Unid, 3) > 0)
        {
            dal.AddPoint(model.orderid.ToString());
            rs = 1;

            new OrderStep().Add(new OrderStepInfo()
            {
                stepcode = 70,
                title = "订单完成",
                subtitle = "",
                addtime = DateTime.Now,
                orderid = orderid,
                revevar1 = "",
                revevar2 = "0",
                revevar3 = "0"

            });

        }
        else
        {
            rs = 0;
        }

        return rs;
    }


}