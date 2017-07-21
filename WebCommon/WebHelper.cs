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
    /// 此类主要用于多个站点都通用的方法
    /// </summary>
    public class WebHelper
    {
        /// <summary>
        /// 充值方式
        /// </summary>
        /// <param name="paytype"></param>
        /// <returns></returns>
        public static string Recharge(string paytype)
        {
            switch (paytype)
            {
                case "2":
                    return "支付宝";
                case "1":
                    return "网站";
                case "5":
                    return "微信";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 商家充值状态
        /// </summary>
        /// <param name="paytype"></param>
        /// <returns></returns>
        public static string shopRechargeState(string paytype)
        {
            switch (paytype)
            {
                case "0":
                    return "进行中";
                case "1":
                    return "完成";
                case "2":
                    return "拒绝";
                case "3":
                    return "商家取消";
            }

            return "异常";
        }

        /// <summary>
        ///商家充值方式
        /// </summary>
        /// <param name="paytype"></param>
        /// <returns></returns>
        public static string shopRecharge(string paytype)
        {
            switch (paytype)
            {
                case "0":
                    return "后台操作";
                case "1":
                    return "提现";
                case "7":
                    return "订单结算";
                default:
                    return "";
            }
        }



        /// <summary>
        /// 根据月份创建文件文件夹
        /// </summary>
        public static string CreateDirectoryByMonth(HttpContext context)
        {
            string strDay = System.DateTime.Now.ToString("yyyyMM");
            string DirUrl = "~/upload/" + strDay + "/";
            if (!System.IO.Directory.Exists(context.Server.MapPath(DirUrl)))//检测文件夹是否存在，不存在则创建
            {
                System.IO.Directory.CreateDirectory(context.Server.MapPath(DirUrl));
            }
            return DirUrl;

        }


        /// <summary>
        /// 查询此订单可以享受的优惠
        /// </summary>
        /// <param name="shoppromotions"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static IList<OrderPromotionInfo> getOrderPromotions(IList<webPromotionConfigInfo> shoppromotions, EAddressInfo address)
        {
            IList<OrderPromotionInfo> targetpromotions = new List<OrderPromotionInfo>();
            if (address.paymode == 4)
            {
                return targetpromotions;//只能在线支付享受优惠
            }
            if (shoppromotions.Count == 0)
            {
                return targetpromotions;
            }

            int[] ptypes = { 20, 30, 40 };

            //首次下单立减N元(不与其他优惠共享)
            webPromotionConfigInfo firstorderpromotion = shoppromotions.Where(a => a.ptype == 1).FirstOrDefault();
            if (firstorderpromotion != null && new Custorder().GetCount("OrderComm = '" + address.Mobilephone + "' and OrderStatus in (0,1,2,7,3)") == 0) //首单，取消的除外
            {
                OrderPromotionInfo item = new OrderPromotionInfo();
                item.revevar1 = firstorderpromotion.revevar1;
                item.freeSendFee = firstorderpromotion.minusmoney;
                item.reveint2 = firstorderpromotion.pId;
                item.shopid = firstorderpromotion.shopid;

                item.startdate = firstorderpromotion.startdate;
                item.enddate = firstorderpromotion.enddate;
                item.ptype = firstorderpromotion.ptype;

                targetpromotions.Add(item);
                return targetpromotions;
            }

            //满N元免配送费
            webPromotionConfigInfo orderpromotion20 = shoppromotions.Where(a => a.ptype == 20).FirstOrDefault();
            if (orderpromotion20 != null && orderpromotion20.overmoney <= address.foodprice)
            {
                OrderPromotionInfo item = new OrderPromotionInfo();
                item.revevar1 = orderpromotion20.revevar1;
                item.freeSendFee = address.senmoney; ;
                item.reveint2 = orderpromotion20.pId;
                item.shopid = orderpromotion20.shopid;

                item.startdate = orderpromotion20.startdate;
                item.enddate = orderpromotion20.enddate;
                item.ptype = orderpromotion20.ptype;

                targetpromotions.Add(item);
            }

            //满N元减少M元
            IList<webPromotionConfigInfo> orderpromotion30 = shoppromotions.Where(a => a.ptype == 30).ToList();
            orderpromotion30 = new IListSort<webPromotionConfigInfo>(orderpromotion30, "overmoney", false).Sort();
            foreach (var promotion in orderpromotion30)
            {
                if (address.foodprice >= promotion.overmoney)
                {
                    OrderPromotionInfo item = new OrderPromotionInfo();
                    item.revevar1 = promotion.revevar1;
                    item.freeSendFee = promotion.minusmoney; ;
                    item.reveint2 = promotion.pId;
                    item.shopid = promotion.shopid;

                    item.startdate = promotion.startdate;
                    item.enddate = promotion.enddate;
                    item.ptype = promotion.ptype;

                    targetpromotions.Add(item);
                    break;
                }

            }


            //提前N分钟下单减少M元
            IList<webPromotionConfigInfo> orderpromotion40 = shoppromotions.Where(a => a.ptype == 40).ToList();
            orderpromotion40 = new IListSort<webPromotionConfigInfo>(orderpromotion40, "overmoney", false).Sort();
            int aheadtime = Convert.ToInt32((Convert.ToDateTime(address.sendtime) - DateTime.Now).TotalMinutes);
            foreach (var promotion in orderpromotion40)
            {
                if (aheadtime >= promotion.overmoney)
                {
                    OrderPromotionInfo item = new OrderPromotionInfo();
                    item.revevar1 = promotion.revevar1;
                    item.freeSendFee = promotion.minusmoney; ;
                    item.reveint2 = promotion.pId;
                    item.shopid = promotion.shopid;
                    item.startdate = promotion.startdate;
                    item.enddate = promotion.enddate;
                    item.ptype = promotion.ptype;

                    targetpromotions.Add(item);
                    break;
                }

            }





            return targetpromotions;
        }



        /// <summary>
        /// 获取商家
        /// </summary>
        /// <param name="shopid"></param>
        /// <param name="PType">商家促销类型，-1表示没有，要在这里获取</param>
        /// <param name="PEnd">参与的平台促销项</param>
        /// <returns></returns>
        public static IList<webPromotionConfigInfo> getShopPromotions(int shopid, int PType, string PEnd)
        {
            IList<webPromotionConfigInfo> shoppromotions = new List<webPromotionConfigInfo>();
            IList<webPromotionConfigInfo> webPromotions = CacheHelper.GetWebPromotionConfig();

            if (PType < 0)
            {
                PointsInfo shop = new Points().GetModel(shopid);
                PType = shop.PType;
                PEnd = shop.PEnd;
            }

            switch (PType)
            {
                case 10:
                    {
                        shoppromotions = new webPromotionConfig().GetList(10, 1, "  startdate <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND enddate >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and isopen=1 and shopid = " + shopid, "pId", 1, 0);
                    }
                    break;
                case 20:
                    {
                        string[] promotionids = delBrackets(PEnd).Split(',');
                        foreach (var pid in promotionids)
                        {
                            foreach (var promotion in webPromotions)
                            {
                                if (pid.Trim() == promotion.pId.ToString())
                                {
                                    shoppromotions.Add(promotion);
                                }

                            }
                        }
                    }
                    break;
                default:
                    break;
            }


            return shoppromotions;
        }

        /// <summary>
        /// 传入{1},{2}..的字符串,过滤其中的括号
        /// </summary>
        /// <param name="mycbList"></param>
        /// <returns></returns>
        public static string delBrackets(string mycbList)
        {
            string tempcat = mycbList.Replace("{", "").Replace("}", "");
            return tempcat;
        }

        /// <summary>
        /// 去第一个的符号
        /// </summary>
        /// <param name="old"></param>
        /// <returns></returns>
        public static string delfirst(string old)
        {
            return System.Text.RegularExpressions.Regex.Replace(old, @"^,", "");
        }

        /// <summary>
        /// 订单接受状态（商家）
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public static string TurnOrderReceiveState(object State)
        {
            string ret = "";
            switch (Convert.ToInt32(State))
            {
                case 0:
                    ret = "未接收"; break;
                case 1:
                    ret = "已接收"; break;
                case 2:
                    ret = "拒绝"; break;
            }

            return ret;
        }

        /// <summary>
        /// 提现状态
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public static string TurnCashOutState(object State)
        {
            string ret = "";
            switch (Convert.ToInt32(State))
            {
                case 0:
                    ret = "未处理"; break;
                case 1:
                    ret = "已打款"; break;
                case 2:
                    ret = "管理员拒绝"; break;
                case 3:
                    ret = "商家取消"; break;
            }

            return ret;
        }

        /// <summary>
        /// 判断当前优惠券编号是否可用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static resultinfo checkCard(string cardckey, decimal curmoney)
        {
            resultinfo rs = new resultinfo();
            rs.status = 0;
            rs.data = "";

            ShopCard dal = new ShopCard();
            string sql = "  ckey = '" + cardckey + "'";
            IList<ShopCardInfo> cardlist = dal.GetList(1, 1, sql, "cid", 1);
            if (cardlist.Count == 0)
            {
                rs.message = "优惠券券号错误，请重新输入";
                rs.status = 20;
                return rs;
            }
            else
            {
                if (cardlist[0].Inve2 == "0")
                {
                    rs.message = "这张券未激活，不能使用";
                    rs.status = 21; 
                    return rs;

                }
                else
                {
                    if (cardlist[0].isused == 1)
                    {
                        rs.message = "这张券已经使用过了，不能重复使用";
                        rs.status = 22; 
                        return rs;
                    }
                    else
                    {
                        if (cardlist[0].moneyline > curmoney)
                        {
                            rs.message = "此券商品"+ cardlist[0].moneyline.ToString("0") + "元可用";
                            rs.status = 23; 
                            rs.moneyline = cardlist[0].moneyline.ToString("0");
                            return rs;
                        }
                        else
                        {
                            //可用，生成json
                            rs.status = 0;
                            decimal cmoney = 0;
                            switch (cardlist[0].ReveInt1)
                            {
                                case 1:
                                    cmoney = cardlist[0].Point;
                                    break;
                                case 2:
                                    cmoney = cardlist[0].Point;
                                    cmoney = curmoney * (100 - cmoney) / 100;
                                    break;
                                case 3:
                                    cmoney = Convert.ToInt32(cardlist[0].Point);
                                    break;
                                default:
                                    break;
                            }

                            rs.data = "[{'CID':'" + cardlist[0].CID + "','Point':'" + cmoney.ToString("0.0") + "','ReveInt1':'" + cardlist[0].ReveInt1 + "','ckey':'" + cardlist[0].ckey + "'}]";
                            return rs;
                        }

                    }
                }
            }

        }




        /// <summary>
        /// 得到随机数
        /// </summary>
        /// <param name="len">随机数长度</param>
        /// <returns></returns>
        public static string GetRandomOnlyNum(int len)
        {
            char[] s = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string str = String.Empty;
            Random random = new Random(GetRandomSeed());
            for (int i = 0; i < len; i++)
            {
                str += s[random.Next(0, s.Length)].ToString();
            }
            return str;
        }
        public static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }


        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string GetMd5(string src)
        {

            System.Security.Cryptography.MD5 MD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] t = MD5.ComputeHash(System.Text.Encoding.GetEncoding("GB2312").GetBytes(src));
            System.Text.StringBuilder sb = new System.Text.StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }



        /// <summary>
        /// 判断订单是否可以重新支付
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public static bool CanPayAgain(CustorderInfo order)
        {
            bool payagian = false;
            if ((order.OrderStatus <= 2 || order.OrderStatus == 7) && order.paystate == 0 && (order.paymode == 1 || order.paymode == 5))
            {
                payagian = true;
            }

            return payagian;
        }
        /// <summary>
        /// 返回跑腿订单状态的中文显示 订单状态： 0 新增;1 配送中;3 成功 ;6 取消,2：已经调度
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public static string TurnExpressOrderState(object State)
        {
            string ret = "";
            switch (Convert.ToInt32(State))
            {
                case 0:
                    ret = "新增"; break;
                case 2:
                    ret = "已经调度"; break;
                case 1:
                    ret = "配送中"; break;
                case 3:
                    ret = "成功"; break;
                case 6:
                    ret = "取消"; break;
            }

            return ret;
        }


    }

}
