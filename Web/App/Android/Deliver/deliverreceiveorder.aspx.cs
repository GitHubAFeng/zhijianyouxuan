using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

/// <summary>
/// 配送员接收订单：
/// 订单变成配送中。添加记录，同后台调度到某人一样
/// </summary>
public partial class AndroidAPI_UpdateOrderdeliverreceiveorde : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string orderid = WebUtility.InputText(Request["orderid"]);
        string Interval = "30"; //WebUtility.InputText(Request["interval"]);
        int did = HjNetHelper.GetPostParam("did", 0);//配送员编号
        int ordertype = HjNetHelper.GetPostParam("ordertype", 1);//1表示外卖订单，2表示跑腿订单

        //先判断订单是否已经变成4，如果是提示已经处理了，否则OrderDeliver添加记录，订单变成4
        OrderDeliver ODdal = new OrderDeliver();

        string msg = "";

        if (ordertype == 1)
        {
            Custorder dal = new Custorder();
            CustorderInfo model = dal.GetModel(orderid);
            if (model != null)
            {
                if (model.sendstate != 0)
                {
                    msg = "{\"orderid\":\"" + orderid + "\",\"state\":\"0\",\"msg\":\"" + "此订单已经被其他配送员接收了" + "\"}";
                }
                else
                {
                    string oid = orderid;

                    DeliverInfo delivermodel = new Deliver().GetModel(did);

                    OrderDeliverInfo odmodel = ODdal.GetModel(oid);
                    if (odmodel == null)
                    {
                        odmodel = new OrderDeliverInfo();
                    }
                    odmodel.Orderid = oid;
                    odmodel.DeliverId = did;
                    odmodel.DeliverName = delivermodel.Name;
                    odmodel.Dispatcher = delivermodel.Name;
                    odmodel.DispatchTime = DateTime.Now;
                    odmodel.DeliveryTime = DateTime.Now;
                    odmodel.Inve1 = Convert.ToInt32(Interval);
                    odmodel.Inve2 = "";
                    odmodel.Section = "";
                    int id = 0;
                    if (odmodel.DataId > 0)
                    {
                        id = ODdal.Update(odmodel);
                    }
                    else
                    {
                        id = ODdal.Add(odmodel);
                    }

                    if (id > 0)
                    {
                        //记录
                        dal.AddOrderRecord(odmodel.Orderid, 7, delivermodel.Name, "配送员接收后修改订单状态");
                        string sql = "update custorder set deliverid='" + did + "',OrderStatus=7,sendstate=1, SetStateTime= getdate() where OrderID='" + orderid + "'";
                        WebUtility.excutesql(sql);

                        //给商家消息
                        {
                            NoticeHelper notice = new NoticeHelper(Context);
                            notice.sendNotice2Shop(model.TogoId, "订单" + oid + "配送员已经抢单", oid);
                        }

                        //new Hangjing.Weixin.SendMsg(Context).sendTemplateMsg(oid);


                        new OrderStep().Add(new OrderStepInfo()
                        {
                            stepcode = 40,
                            title = "配送员正在赶往商家",
                            subtitle = "骑士电话："+delivermodel.Phone,
                            deliverid = did,
                            addtime = DateTime.Now,
                            orderid = orderid,
                            revevar1 = delivermodel.Phone,
                            revevar2 = "0",
                            revevar3 = "0"

                        });

                        msg = "{\"orderid\":\"" + orderid + "\",\"state\":\"1\",\"msg\":\"\"}";
                    }
                    else
                    {
                        msg = "{\"orderid\":\"" + orderid + "\",\"state\":\"0\",\"msg\":\"配送员接收出错\"}";
                        HJlog.toLog("配送员接收出错");
                    }

                }
            }
            else
            {
                msg = "{\"orderid\":\"" + orderid + "\",\"state\":\"0\",\"msg\":\"" + "此订单不存在" + "\"}";
            }
        }
        else
        {
            ExpressOrder dal = new ExpressOrder();
            IList<ExpressOrderInfo> list = dal.GetList(1, 1, "OrderID='" + orderid + "'", "dataid", 1);
            if (list.Count > 0)
            {
                ExpressOrderInfo model = list[0];

                if (model != null)
                {
                    if (model.State != 1)
                    {
                        msg = "{\"orderid\":\"" + orderid + "\",\"state\":\"0\",\"msg\":\"" + "此订单已经被其他配送员接收了" + "\"}";
                    }
                    else
                    {
                        string oid = orderid;

                        DeliverInfo delivermodel = new Deliver().GetModel(did);

                        OrderDeliverInfo odmodel = ODdal.GetModel(oid);
                        if (odmodel == null)
                        {
                            odmodel = new OrderDeliverInfo();
                        }
                        odmodel.Orderid = oid;
                        odmodel.DeliverId = did;
                        odmodel.DeliverName = delivermodel.Name;
                        odmodel.Dispatcher = delivermodel.Name;
                        odmodel.DispatchTime = DateTime.Now;
                        odmodel.DeliveryTime = DateTime.Now;
                        odmodel.Inve1 = Convert.ToInt32(Interval);
                        odmodel.Inve2 = "";
                        odmodel.Section = "";
                        int id = 0;
                        if (odmodel.DataId > 0)
                        {
                            id = ODdal.Update(odmodel);
                        }
                        else
                        {
                            id = ODdal.Add(odmodel);
                        }

                        if (id > 0)
                        {
                            //记录

                            dal.AddOrderRecord(orderid, 2, delivermodel.Name, "配送员接收跑腿订单后修改订单状态");
                            string sql = "update ExpressOrder set state=2,Inve1 = " + did + " where OrderID='" + orderid + "'";//抢单成功-配送中
                            WebUtility.excutesql(sql);
                            msg = "{\"orderid\":\"" + orderid + "\",\"state\":\"1\",\"msg\":\"\"}";
                        }
                        else
                        {
                            msg = "{\"orderid\":\"" + orderid + "\",\"state\":\"0\",\"msg\":\"配送员接收出错\"}";
                            HJlog.toLog("配送员接收出错");
                        }
                    }
                }
                else
                {
                    msg = "{\"orderid\":\"" + orderid + "\",\"state\":\"0\",\"msg\":\"" + "此订单不存在" + "\"}";
                }
            }
            else
            {
                msg = "{\"orderid\":\"" + orderid + "\",\"state\":\"0\",\"msg\":\"" + "此订单不存在" + "\"}";
            }
        }

        Response.Write(msg);
        Response.End();
    }
}
