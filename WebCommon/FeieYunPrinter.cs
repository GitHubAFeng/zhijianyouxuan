using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;
using Newtonsoft.Json;

namespace Hangjing.WebCommon
{

    /// <summary>
    /// 飞鹅打印机相关
    /// </summary>
    public class FeieYunPrinter
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        private string orderid;

        public const string IP = "http://api.feieyun.cn/Api/Open/";
        public string user = CacheHelper.GetSetValue(77);  //飞鹅云后台注册用户名。
        public string ukey = CacheHelper.GetSetValue(78);    //
        public string sn = "";    //


        /// <summary>
        /// 根据订单编号实例
        /// </summary>
        /// <param name="orderid"></param>
        public FeieYunPrinter(string orderid)
        {
            this.orderid = orderid;

        }

        public apiResultInfo PrintOrder()
        {
            apiResultInfo rs = new apiResultInfo();
            CustorderInfo order = new Custorder().GetModel(orderid);

            TogoPrinterInfo printerconfig = new TogoPrinter().GetModel(order.TogoId);
            if (printerconfig == null)
            {
                rs.state = 0;
                rs.msg = "此商家无打印机：订单：" + orderid;
                return rs;
            }
            else
            {
                sn = printerconfig.PrinterSn;
            }

            //标签说明："<BR>"为换行符,"<CB></CB>"为居中放大,"<B></B>"为放大,"<C></C>"为居中,<L></L>字体变高
            //<W></W>字体变宽",<QR></QR>"为二维码,"<CODE>+12位数字"为一维条码

            //拼凑订单内容时可参考如下格式
            string orderInfo = getOrderPrintFormat(order);
            //orderInfo = Uri.EscapeDataString(orderInfo);

            string stime = Hangjing.Common.Utils.ConvertToUnixTimestamp(DateTime.Now).ToString();  // 	当前UNIX时间戳，10位，精确到秒。

            string sig = Hangjing.Common.DataHelper.SHA1(user + ukey + stime).ToLower();

            string postData = "user=" + user;
            postData += ("&stime=" + stime);
            postData += ("&sig=" + sig);
            postData += ("&apiname=Open_printMsg");

            postData += ("&sn=" + sn);
            postData += ("&content=" + orderInfo);
            postData += ("&times=1");

            HttpHelper objhttp = new HttpHelper();
            objhttp.isToLower = false;
            HttpItem objHttpItem = new HttpItem()
            {
                URL = IP,
                Encoding = "utf-8",
                Method = "POST",
                Postdata = postData,
            };
            HJlog.toLog("IP=" + IP + " Postdata=" + postData);

            string returnmsg = objhttp.GetHtml(objHttpItem);

            if (returnmsg != "String Error")
            {
                printresult pr = JsonConvert.DeserializeObject<printresult>(returnmsg);
                if (pr.responseCode == 0)
                {
                    rs.state = 1;
                    rs.msg = "";
                }
                else
                {
                    rs.state = 0;
                    rs.msg = pr.msg;
                }

            }
            else
            {
                rs.state = 0;
                rs.msg = "服务器繁忙，请稍后再试";
            }

            Hangjing.AppLog.AppLog.Info("PrintOrder=" + returnmsg + "\r\nurl=" + objHttpItem.URL);

            return rs;
        }

        /// <summary>
        /// 订单格式
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public string getOrderPrintFormat(CustorderInfo order)
        {
            //标签说明："<BR>"为换行符,"<CB></CB>"为居中放大,"<B></B>"为放大,"<C></C>"为居中,<L></L>字体变高
            //<W></W>字体变宽",<QR></QR>"为二维码,"<CODE>+12位数字"为一维条码

            //拼凑订单内容时可参考如下格式
            string orderInfo;
            orderInfo = "<CB>" + order.TogoName + "</CB><BR>";//标题字体如需居中放大,就需要用标签套上
            orderInfo += "名称　　　　　 单价  数量 金额<BR>";
            orderInfo += "--------------------------------<BR>";

            IList<FoodlistInfo> foods = new Foodlist().GetAllByOrderID(order.orderid);

            foreach (var item in foods)
            {
                string name = item.FoodName;
                int lo = System.Text.Encoding.GetEncoding("GB2312").GetByteCount(name);
                //lo = name.Length;
                while (lo < 14)
                {
                    name += " ";
                    lo++;
                }

                orderInfo += name + " " + item.FoodPrice + "   " + item.FCounts + "   " + (item.FCounts * item.FoodPrice) + "<BR>";
            }
            orderInfo += "备注：" + order.OrderAttach + "<BR>";
            orderInfo += "--------------------------------<BR>";
            orderInfo += "合计：" + order.OrderSums + "元[" + (order.paystate == 1 ? "已付" : "未付") + "]<BR>";

            orderInfo += " 配送费：" + order.SendFee + "元<BR>";

            if (order.cardpay > 0)
            {
                orderInfo += "优惠券支付：" + order.cardpay + "元<BR>";
            }

            if (order.paymode == 4)
            {
                orderInfo += " 实际支付：" + (order.OrderSums - order.cardpay) + "元<BR>";
            }
            else
            {
                orderInfo += " 实际支付：" + order.paymoney + "元<BR>";
            }


            if (order.promotionmoney > 0 && order.paymode != 4)
            {
                orderInfo += "促销优惠：" + order.promotionmoney + "元<BR>";
            }

            orderInfo += "送餐地址：" + order.AddressText + "<BR>";
            //orderInfo += "送餐时间：" + order.SendTime + "<BR>";

            orderInfo += "订单号：" + order.orderid + "<BR>";
            orderInfo += "联系人：" + order.OrderRcver + "<BR>";
            orderInfo += "联系电话：" + order.OrderComm + "<BR>";
            orderInfo += "订餐时间：" + order.OrderDateTime + "<BR>";

            orderInfo += "客服电话：" + CacheHelper.GetSetValue(6) + "<BR>";

            orderInfo += "<BR>";


            return orderInfo;
        }


    }
}