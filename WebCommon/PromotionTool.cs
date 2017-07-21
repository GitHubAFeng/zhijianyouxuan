using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Newtonsoft.Json;

namespace Hangjing.WebCommon
{
    /// <summary>
    /// 促销相关工具类
    /// </summary>
    public class PromotionTool
    {
        /// <summary>
        /// 返回订单参与的促销json 
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public static string getOrderPromotionsStr(string orderid)
        {
            IList<OrderPromotionInfo> promotions = new OrderPromotion().GetList(10, 1, "revevar2 = '" + orderid + "'", "pid", 1);
            IList<simplePromotion> simplePromotions = new List<simplePromotion>();
            foreach (var item in promotions)
            {
                simplePromotions.Add(new simplePromotion(item.revevar1, item.freeSendFee));
            }
            StringBuilder sb = new StringBuilder("\"Promotions\":");
          
            sb.Append(JsonConvert.SerializeObject(simplePromotions));

            return sb.ToString();
        }

        /// <summary>
        /// 商家促销转化成 IList<ShopFoodPictureInfo>
        /// </summary>
        /// <param name="shopid"></param>
        /// <param name="PType">商家促销类型，-1表示没有，要在这里获取</param>
        /// <param name="PEnd">参与的平台促销项</param>
        /// <returns></returns>
        public static IList<ShopFoodPictureInfo> getPromotionsFormPicTagList(int shopid, int PType, string PEnd)
        {
            IList<ShopFoodPictureInfo> promotions = new List<ShopFoodPictureInfo>();
            IList<webPromotionConfigInfo> shoppromotions = WebHelper.getShopPromotions(shopid, PType, PEnd);
            foreach (var item in shoppromotions)
            {
                ShopFoodPictureInfo tag = new ShopFoodPictureInfo();
                tag.Title = item.revevar1;
                tag.Picture = CacheHelper.GetSetValue(1) + "/images/jian_02.png";
                tag.togoname = item.overmoney.ToString(); 
                tag.Inve2 = item.minusmoney.ToString();

                promotions.Add(tag);

            }

            return promotions;
        }


    }

    /// <summary>
    /// 简单的促销实体
    /// </summary>
    class simplePromotion
    {      
        public simplePromotion(string title, decimal freeSendFee)
        {
            this.title = title;
            this.freeSendFee = freeSendFee;
        }

        /// <summary>
        /// 促销标题
        /// </summary>
        public string title
        {
            get;
            set;
        }


        /// <summary>
        /// 优惠的金额
        /// </summary>
        public decimal freeSendFee
        {
            get;
            set;
        }
    }
}
