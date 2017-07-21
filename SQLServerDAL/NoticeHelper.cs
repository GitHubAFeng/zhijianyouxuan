using Hangjing.Model;
using Hangjing.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Hangjing.DBUtility;

namespace Hangjing.SQLServerDAL
{

    /// <summary>
    /// 此类主要处理给app发消息
    /// </summary>
    public class NoticeHelper
    {
        HttpContext context;
        string shopid = "";
        string orderid = "";

        static bool ispush = Convert.ToInt32(CacheHelper.GetSetValue(38)) == 1 ? true : false;

        public NoticeHelper(HttpContext _context)
        {
            context = _context;
        }

        /// <summary>
        /// 推送消息给商家
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="shopid">可以表示商家编号、骑士编号、群组编号</param>
        public NoticeHelper(HttpContext _context, string _shopid)
        {
            context = _context;
            shopid = _shopid;
        }

        /// <summary>
        /// 推送消息给商家
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="shopid">可以表示商家编号、骑士编号、群组编号</param>
        public NoticeHelper(HttpContext _context, string _shopid, string _orderid)
        {
            context = _context;
            shopid = _shopid;
            orderid = _orderid;
        }


        /// <summary>
        /// 推送未处理订单个数给商家
        /// </summary>
        public void send2ShopByShopid()
        {
            int count = new Custorder().SendShopOrderCount(shopid);
            string msg = "{\"state\":\"1\",\"count\":\"" + count + "\"}";
            if (!ispush)
            {

            }
            else//supersocket
            {
                wcfnotice.UserNoticeServiceClient unsc = new wcfnotice.UserNoticeServiceClient();
                ///发订单
                unsc.AddMessage(Convert.ToInt32(shopid), 2, 0, msg);
            }

        }


        /// <summary>
        /// 推送申请配送的订单个数给骑士
        /// </summary>
        public void sendOrderByDeliveryid()
        {
            //订单表中也保存配送员编号，便于统计,修改订单状态:
            string sql = "update Custorder set deliverid='" + shopid + "',OrderStatus=7,sendstate=0  where OrderID='" + orderid + "'";//订单成功-已经调度
            SQLHelper.excutesql(sql);

            OrderCountInfo model = new Custorder().SendDeliverOrderCount(shopid);
            OrderCountInfo expressmodel = new ExpressOrder().SendDeliverOrderCount(shopid);

            string msg = "{\"state\":\"1\",\"newordercount\":\"" + model.CountIntValue + "\",\"ordercount\":\"" + 0 + "\",\"ordercountall\":\"" + 0 + "\",\"ExpressOrdercount\":\"" + expressmodel.CountIntValue + "\"}";

            if (!ispush)
            {

            }
            else//supersocket
            {
                wcfnotice.UserNoticeServiceClient unsc = new wcfnotice.UserNoticeServiceClient();
                ///发订单
                unsc.AddMessage(Convert.ToInt32(shopid), 1, 0, msg);
            }
        }

        /// <summary>
        /// 给骑士发跑腿订单消息
        /// </summary>
        public void sendExpressOrderToDeliver()
        {
            string sql = "update ExpressOrder set Inve1='" + shopid + "',state=1  where OrderID='" + orderid + "'";//订单成功-已经调度
            SQLHelper.excutesql(sql);

            OrderCountInfo model = new Custorder().SendDeliverOrderCount(shopid);
            OrderCountInfo expressmodel = new ExpressOrder().SendDeliverOrderCount(shopid);
            string msg = "{\"state\":\"1\",\"newordercount\":\"" + model.CountIntValue + "\",\"ordercount\":\"" + 0 + "\",\"ordercountall\":\"" + 0 + "\",\"ExpressOrdercount\":\"" + expressmodel.CountIntValue + "\"}";

            if (!ispush)
            {

            }
            else//supersocket
            {
                wcfnotice.UserNoticeServiceClient unsc = new wcfnotice.UserNoticeServiceClient();
                ///发订单
                unsc.AddMessage(Convert.ToInt32(shopid), 1, 0, msg);
            }
        }


        /// <summary>
        /// 推送申请配送的订单个数给骑士 不更新数据
        /// </summary>
        public void sendOrderByDeliveryidNoData()
        {
            OrderCountInfo model = new Custorder().SendDeliverOrderCount(shopid);
            OrderCountInfo expressmodel = new ExpressOrder().SendDeliverOrderCount(shopid);

            string msg = "{\"state\":\"1\",\"newordercount\":\"" + model.CountIntValue + "\",\"ordercount\":\"" + 0 + "\",\"ordercountall\":\"" + 0 + "\",\"ExpressOrdercount\":\"" + expressmodel.CountIntValue + "\"}";

            if (!ispush)
            {

            }
            else//supersocket
            {
                wcfnotice.UserNoticeServiceClient unsc = new wcfnotice.UserNoticeServiceClient();
                ///发订单
                unsc.AddMessage(Convert.ToInt32(shopid), 1, 0, msg);
            }
        }

        /// <summary>
        /// 给骑士发跑腿订单消息 不更新数据
        /// </summary>
        public void sendExpressOrderToDeliverNoLData()
        {
            OrderCountInfo model = new Custorder().SendDeliverOrderCount(shopid);
            OrderCountInfo expressmodel = new ExpressOrder().SendDeliverOrderCount(shopid);
            string msg = "{\"state\":\"1\",\"newordercount\":\"" + model.CountIntValue + "\",\"ordercount\":\"" + 0 + "\",\"ordercountall\":\"" + 0 + "\",\"ExpressOrdercount\":\"" + expressmodel.CountIntValue + "\"}";

            if (!ispush)
            {
                wcfnotice.UserNoticeServiceClient unsc = new wcfnotice.UserNoticeServiceClient();
                ///发订单
                unsc.AddMessage(Convert.ToInt32(shopid), 1, 0, msg);
            }
        }



        /// <summary>
        /// 发群主订单
        /// </summary>
        public void send2Group()
        {
            IList<DeliverInfo> deliverlist = CacheHelper.GetDeliverList().Where(a => a.GpsIMEI == shopid && a.IsApproved == 0).ToList();//并且为审核通过
            foreach (var item in deliverlist)
            {
                int id = item.DataId;
                int type = 1;//消息类型：0表示订单，1表示纯消息。
                int se = 1;//1表示骑士，2表示商家

                /**********************************************
                  msg（通知内容）
                       新订单通知 {\"state\":"\"1\,\"count\":\"1\"}  count 表示订单数量
                       消息通知   {\"state\":"\"1\,\"msg\":\"XX订单取消配送\"} msg 表示消息内容
                  id  商家或者骑士编号
                  se  1表示骑士，2表示商家
                  type  消息类型：0表示订单，1表示纯消息。
                ************************************************/
                String msg = "{\"state\":\"1\",\"msg\":\"群组订单\"}";//消息内容：json格式

                if (!ispush)
                {

                }
                else//supersocket
                {
                    wcfnotice.UserNoticeServiceClient unsc = null;
                    if (unsc == null)
                    {
                        unsc = new wcfnotice.UserNoticeServiceClient();
                    }
                    ///发订单
                    unsc.AddMessage(id, se, type, msg);
                }
            }
        }

        /// <summary>
        /// 发所有人群订单(本城市)
        /// </summary>
        public void send2All(int cityid)
        {
            IList<DeliverInfo> deliverlist = CacheHelper.GetDeliverList().Where(a => a.Inve1 == cityid && a.IsApproved == 0).ToList();//并且审核状态为审核通过
            foreach (var item in deliverlist)
            {
                int id = item.DataId;
                int type = 1;//消息类型：0表示订单，1表示纯消息。
                int se = 1;//1表示骑士，2表示商家

                /**********************************************
                  msg（通知内容）
                       新订单通知 {\"state\":"\"1\,\"count\":\"1\"}  count 表示订单数量
                       消息通知   {\"state\":"\"1\,\"msg\":\"XX订单取消配送\"} msg 表示消息内容
                  id  商家或者骑士编号
                  se  1表示骑士，2表示商家
                  type  消息类型：0表示订单，1表示纯消息。
                ************************************************/
                String msg = "{\"state\":\"1\",\"msg\":\"群组订单\"}";//消息内容：json格式

                if (!ispush)
                {

                }
                else//supersocket
                {
                    wcfnotice.UserNoticeServiceClient unsc = null;
                    if (unsc == null)
                    {
                        unsc = new wcfnotice.UserNoticeServiceClient();
                    }
                    ///发订单
                    unsc.AddMessage(id, se, type, msg);
                }

            }
        }

        /// <summary>
        /// 发指定编号的骑士
        /// 参数 ids为骑士编号，多个用逗号分
        /// </summary>
        public void send2IDs(string ids)
        {
            string[] deliverlist = ids.Split(',');

            foreach (var item in deliverlist)
            {
                int id = Convert.ToInt32(item);
                int type = 1;//消息类型：0表示订单，1表示纯消息。
                int se = 1;//1表示骑士，2表示商家

                /**********************************************
                  msg（通知内容）
                       新订单通知 {\"state\":"\"1\,\"count\":\"1\"}  count 表示订单数量
                       消息通知   {\"state\":"\"1\,\"msg\":\"XX订单取消配送\"} msg 表示消息内容
                  id  商家或者骑士编号
                  se  1表示骑士，2表示商家
                  type  消息类型：0表示订单，1表示纯消息。
                ************************************************/
                String msg = "{\"state\":\"1\",\"msg\":\"群组订单\"}";//消息内容：json格式

                if (!ispush)
                {

                }
                else//supersocket
                {
                    wcfnotice.UserNoticeServiceClient unsc = null;
                    if (unsc == null)
                    {
                        unsc = new wcfnotice.UserNoticeServiceClient();
                    }
                    ///发订单
                    unsc.AddMessage(id, se, type, msg);
                }

            }
        }

        /// <summary>
        /// 给商家普通消息（如果订单处理后）
        /// </summary>
        public void sendNotice2Shop(int did, string notice, string orderid)
        {
            int count = new Custorder().SendShopOrderCount(did.ToString());
            wcfnotice.UserNoticeServiceClient unsc = new wcfnotice.UserNoticeServiceClient();
            string msg = "{\"state\":\"1\",\"count\":\"" + count + "\",\"msg\":\"" + notice + "\",\"msgtype\":\"1\",\"orderid\":\"" + orderid + "\"}";

            if (!ispush)
            {

            }
            else//supersocket
            {
                unsc.AddMessage(did, 2, 1, msg);
                unsc.Close();
            }

        }
    }
}
