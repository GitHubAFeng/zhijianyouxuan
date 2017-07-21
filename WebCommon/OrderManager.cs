using Hangjing.Model;
using Hangjing.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Hangjing.WebCommon
{
    /// <summary>
    /// 订单处理逻辑（pc,微信等客户端提交订单都调用这个类，）
    /// </summary>
    public class OrderManager
    {
        Custorder dal = new Custorder();
        public IList<ROrderinfo> submitOrder(IList<Hangjing.Model.ETogoShoppingCart> list, EAddressInfo address, HttpContext context)
        {
            IList<ROrderinfo> mylist = dal.SubmitOrder(list, address);
            foreach (var item in mylist)
            {
                //2015.3.25 货到付款和余额支付的直接通知商家,支付宝支付 微信支付 银联支付需要支付成功后再通知
                if (address.paymode == 3 || address.paymode == 4)
                {
                    FeieYunPrinter p = new FeieYunPrinter(item.Orderid);
                    apiResultInfo rs = p.PrintOrder();

                    NoticeHelper notice = new NoticeHelper(context, item.togoid.ToString());
                    if (item.isAutoReceiveOrder == 0)
                    {
                        notice.send2ShopByShopid();
                    }
                    else//自动接单发普通消息
                    {
                        notice.sendNotice2Shop(item.togoid, "您有新订单，已经自动接单",item.Orderid);
                    }

                    if (item.WeiXxinOpenID != "" && CacheHelper.GetSetValue(58) == "1")
                    {
                        new Hangjing.Weixin.SendMsg(context).sendText(item.WeiXxinOpenID, "您有新订单：" + item.Orderid + "，请注意查收");
                    }
                }
            }


            return mylist;
        }
    }
}
