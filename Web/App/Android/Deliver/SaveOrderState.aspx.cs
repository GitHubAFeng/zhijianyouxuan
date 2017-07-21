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
///  更新订单状态（当状态为3为配送完成时，订单状态也变成完成）
// 传入参数：订单编号 配送状态状态 
/// </summary>
public partial class App_Android_Deliver_SaveOrderState : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string orderid = WebUtility.InputText(Request["orderid"]);
        string state = WebUtility.InputText(Request["state"]);//配送状态
        int did = HjNetHelper.GetPostParam("did", 0);
        int ordertype = HjNetHelper.GetPostParam("ordertype", 1);//1表示外卖订单，2表示跑腿订单

        //后期添加 外卖订单配送状态改成1，跑腿订单状态改成2的时候推送消息 2015-12-14 

        if (ordertype == 1)//外卖订单 修改配送状态
        {
            Custorder dal = new Custorder();

            string sql = "update custorder set sendstate=" + state;
            sql += " where OrderID='" + orderid + "' and deliverid=" + did;

            if (WebUtility.excutesql(sql) > 0)
            {

                switch (Convert.ToInt32(state))
                {
                    case 1:
                        DeliverInfo delivermodel = new Deliver().GetModel(did);

                        new OrderStep().Add(new OrderStepInfo()
                        {
                            stepcode = 40,
                            title = "配送员正在赶往商家",
                            subtitle = "骑士电话：" + delivermodel.Phone,
                            deliverid = did,
                            addtime = DateTime.Now,
                            orderid = orderid,
                            revevar1 = delivermodel.Phone,
                            revevar2 = "0",
                            revevar3 = "0"

                        });

                        //if (state == "1")
                        //{
                        //    new Hangjing.Weixin.SendMsg(Context).sendTemplateMsg(orderid);
                        //}


                        NoticeHelper notice = new NoticeHelper(Context, did.ToString(), orderid);
                        notice.sendOrderByDeliveryidNoData();
                        break;

                    case 2:
                        GPS_RecordsInfo gps = new GPS_Records().GetModelByDid(did);
                        new OrderStep().Add(new OrderStepInfo()
                        {
                            stepcode = 50,
                            title = "配送员已经取货",
                            subtitle = "请等待配送",
                            deliverid = did,
                            addtime = DateTime.Now,
                            orderid = orderid,
                            revevar1 = "",
                            revevar2 = gps.JH2,
                            revevar3 = gps.JH3

                        });
                        string sql2 = "update custorder set picktime='" + DateTime.Now + "'";
                        sql2 += " where OrderID='" + orderid + "' and deliverid=" + did;
                        WebUtility.excutesql(sql2);

                        break;
                    case 3:
                        dal.deliverComplete(orderid, did);

                        new OrderStep().Add(new OrderStepInfo()
                        {
                            stepcode = 60,
                            title = "已送达",
                            subtitle = "",
                            deliverid = did,
                            addtime = DateTime.Now,
                            orderid = orderid,
                            revevar1 = "",
                            revevar2 = "0",
                            revevar3 = "0"

                        });
                        string sql3 = "update custorder set comtime='" + DateTime.Now + "'";
                        sql3 += " where OrderID='" + orderid + "'";
                        WebUtility.excutesql(sql3);


                        break;
                    default:
                        break;
                }

                Response.Write("{\"orderid\":\"" + orderid + "\",\"state\":\"1\"}");
            }
            else
            {
                Response.Write("{\"orderid\":\"" + orderid + "\",\"state\":\"0\",\"msg\":\"此订单已经被其他配送员接收了，您不能再操作了\"}");
            }
        }
        else//跑腿订单
        {
            ExpressOrder edal = new ExpressOrder();
            string sql = "update expressorder set State=" + state;
            sql += " where OrderID='" + orderid + "' and Inve1=" + did;

            if (WebUtility.excutesql(sql) > 0)
            {
                Response.Write("{\"orderid\":\"" + orderid + "\",\"state\":\"1\"}");

                //通知骑士客户端。
                if (Convert.ToInt32(state) == 2)
                {
                    try
                    {
                        NoticeHelper notic = new NoticeHelper(HttpContext.Current, did.ToString(), orderid);
                        notic.sendExpressOrderToDeliverNoLData();
                    }
                    catch (Exception ex)
                    {
                        HJlog.toLog("推送消息给骑士异常：" + ex.Message);
                    }
                }

                if (Convert.ToInt32(state) == 3)
                {
                    edal.deliverComplete(orderid, did);
                    string sql3 = "update expressorder set comtime='" + DateTime.Now + "'";
                    sql3 += " where OrderID='" + orderid + "' and Inve1=" + did;
                    WebUtility.excutesql(sql3);
                }

                if (Convert.ToInt32(state) == 4)
                {
                    string sql4 = "update expressorder set picktime='" + DateTime.Now + "'";
                    sql4 += " where OrderID='" + orderid + "' and Inve1=" + did;
                    HJlog.toLog("sql4=" + sql4);
                    WebUtility.excutesql(sql4);
                }

            }
            else
            {
                Response.Write("{\"orderid\":\"" + orderid + "\",\"state\":\"0\",\"msg\":\"此订单已经被其他配送员接收了，您不能再操作了\"}");
            }
        }

        Response.End();
    }
}
